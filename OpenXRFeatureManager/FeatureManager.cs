// <copyright file="FeatureManager.cs" company="nicoco007">
// This file is part of OpenXRFeatureManager.
//
// OpenXRFeatureManager is free software: you can redistribute it and/or modify it under the terms
// of the GNU General Public License as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
//
// OpenXRFeatureManager is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with OpenXRFeatureManager.
// If not, see https://www.gnu.org/licenses/.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Management;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
using Logger = IPA.Logging.Logger;

namespace OpenXRFeatureManager
{
    /// <summary>
    /// Manages adding extra features to <see cref="OpenXRSettings"/> and restarting OpenXR so the changes apply.
    /// </summary>
    public class FeatureManager
    {
        private readonly Logger _logger;
        private readonly List<OpenXRFeature> _registeredFeatures = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureManager"/> class.
        /// </summary>
        /// <param name="logger">The logger that this instance will use.</param>
        internal FeatureManager(Logger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Invoked after OpenXR is unloaded and before new features are added during the restart procedure.
        /// </summary>
        public event Action? afterOpenXRUnloaded;

        /// <summary>
        /// Invoked after new features are added and before OpenXR is reloaded during the restart procedure.
        /// </summary>
        public event Action? beforeOpenXRReloaded;

        /// <summary>
        /// Gets the current instance of <see cref="FeatureManager"/>.
        /// </summary>
        public static FeatureManager instance { get; internal set; } = null!;

        /// <summary>
        /// Gets a value indicating whether or not this instance of <see cref="FeatureManager"/> has been initialized.
        /// </summary>
        internal bool initialized { get; private set; }

        /// <summary>
        /// Instantiates and configures an <see cref="OpenXRFeature"/> of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the feature to create.</typeparam>
        /// <param name="featureId">A well known string ID for the feature. It is recommended that this ID be in reverse DNS naming format (com.foo.bar.feature).</param>
        /// <param name="name">Name of the feature.</param>
        /// <param name="version">Version of the feature.</param>
        /// <param name="company">Name of the company/developer(s) that created the feature.</param>
        /// <param name="openXRExtensionStrings">A comma-separated list of OpenXR extensions that are necessary for the feature to work.</param>
        /// <param name="priority">
        /// Determines the order in which the feature will be called in both the GetInstanceProcAddr hook list and
        /// when events such as OnInstanceCreate are called. Higher priority features will hook after lower priority features and
        /// be called first in the event list.
        /// </param>
        /// <param name="required">True if this feature is required, false otherwise. Required features will cause the loader to fail to initialize if they fail to initialize or start.</param>
        /// <returns>The newly created feature.</returns>
        public static T CreateOpenXRFeature<T>(string featureId, string? name = null, string version = "0.0.1", string? company = null, string? openXRExtensionStrings = null, int priority = 0, bool required = false)
            where T : OpenXRFeature
        {
            T feature = ScriptableObject.CreateInstance<T>();

            name ??= $"{typeof(T).Name} Standalone";

            feature.name = name;
            feature.m_enabled = true;
            feature.featureIdInternal = featureId;
            feature.nameUi = name;
            feature.version = version;
            feature.company = company;
            feature.openxrExtensionStrings = openXRExtensionStrings;
            feature.priority = priority;
            feature.required = required;

            return feature;
        }

        /// <summary>
        /// Registers an <see cref="OpenXRFeature"/>. This operation is idempotent.
        /// </summary>
        /// <param name="openXRFeature">The <see cref="OpenXRFeature"/> to register.</param>
        public void RegisterFeature(OpenXRFeature openXRFeature)
        {
            if (initialized)
            {
                throw new InvalidOperationException("Tried registering an OpenXR feature too late! Features should be registered in a mod's [OnStart] or [OnEnable] method.");
            }

            if (_registeredFeatures.Contains(openXRFeature))
            {
                return;
            }

            _logger.Info($"Registering feature {openXRFeature.GetType().Name}");

            _registeredFeatures.Add(openXRFeature);
        }

        /// <summary>
        /// Adds the features defined by <see cref="_registeredFeatures"/> to <see cref="OpenXRSettings.features"/> and restarts the OpenXR loader.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        internal async Task AddFeaturesAndRestartOpenXRAsync()
        {
            if (initialized)
            {
                throw new InvalidOperationException("Already initialized");
            }

            if (_registeredFeatures.Count == 0)
            {
                _logger.Trace("No features registered; nothing to do");
                initialized = true;
                return;
            }

            XRManagerSettings manager = XRGeneralSettings.Instance.Manager;
            bool openXRWasLoaded = manager.activeLoader is OpenXRLoader;

            if (openXRWasLoaded)
            {
                _logger.Trace($"Deinitializing {manager.activeLoader.name}");
                manager.DeinitializeLoader();
            }

            afterOpenXRUnloaded?.Invoke();

            _logger.Trace("Adding additional OpenXR features");
            OpenXRFeature[] existingFeatures = OpenXRSettings.Instance.features;

            var allFeatures = new OpenXRFeature[existingFeatures.Length + _registeredFeatures.Count];
            Array.Copy(existingFeatures, allFeatures, existingFeatures.Length);

            for (int i = 0; i < _registeredFeatures.Count; i++)
            {
                allFeatures[existingFeatures.Length + i] = _registeredFeatures[i];
            }

            OpenXRSettings.Instance.features = allFeatures;

            // Important: this prevents HMD data through XR Input going missing when the game window
            // loses focus if the window isn't focused while starting up (potentially among other things).
            await Task.Yield();

            beforeOpenXRReloaded?.Invoke();

            if (openXRWasLoaded)
            {
                _logger.Trace("Initializing loader");
                manager.InitializeLoaderSync();
                manager.StartSubsystems();
            }

            initialized = true;
        }
    }
}

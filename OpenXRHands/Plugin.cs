// <copyright file="Plugin.cs" company="nicoco007">
// This file is part of OpenXRHands.
//
// OpenXRHands is free software: you can redistribute it and/or modify it under the terms
// of the GNU General Public License as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
//
// OpenXRHands is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with OpenXRHands.
// If not, see https://www.gnu.org/licenses/.
// </copyright>

using IPA;
using IPA.Loader;
using OpenXRFeatureManager;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Hands.OpenXR;

namespace OpenXRHands;

[Plugin(RuntimeOptions.SingleStartInit)]
public class Plugin
{
    [Init]
    public Plugin()
    {
    }

    [OnStart]
    public void OnStart()
    {
        FeatureManager featureManager = FeatureManager.instance;

        featureManager.beforeOpenXRReloaded += OnBeforeOpenXRReloaded;

        string xrHandsVersion = PluginManager.GetPluginFromId("Unity.XR.Hands").HVersion.ToString();
        featureManager.RegisterFeature(FeatureManager.CreateOpenXRFeature<HandTracking>(HandTracking.featureId, null, xrHandsVersion, "Unity", HandTracking.extensionString, -100));
        featureManager.RegisterFeature(FeatureManager.CreateOpenXRFeature<MetaHandTrackingAim>(MetaHandTrackingAim.featureId, null, xrHandsVersion, "Unity", MetaHandTrackingAim.extensionString));
    }

    [OnExit]
    public void OnExit()
    {
        FeatureManager.instance.beforeOpenXRReloaded -= OnBeforeOpenXRReloaded;
    }

    private void OnBeforeOpenXRReloaded()
    {
        // These have RuntimeInitializeOnLoadMethod attributes so they'd usually be loaded by Unity directly,
        // but since the game wasn't built with these we need to call them manually. Some of them rely on
        // the XR features being registered in OpenXRSettings so we do it in OnBeforeOpenXRReloaded.
        MetaAimHand.RegisterLayout();
        XRHandDevice.Initialize();
        OpenXRHandProvider.Register();
    }
}

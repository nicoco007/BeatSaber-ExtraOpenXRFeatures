// <copyright file="Plugin.cs" company="nicoco007">
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

using HarmonyLib;
using IPA;
using IPA.Logging;
using OpenXRFeatureManager.Patches;
using UnityEngine.SceneManagement;

namespace OpenXRFeatureManager;

[Plugin(RuntimeOptions.SingleStartInit)]
public class Plugin
{
    private readonly Harmony _harmony = new("com.nicoco007.openxr-feature-manager");

    [Init]
    public Plugin(Logger logger)
    {
        FeatureManager.instance = new FeatureManager(logger.GetChildLogger(nameof(FeatureManager)));
    }

    [OnStart]
    public void OnEnable()
    {
        SceneContext_Awake.sceneEarlyLoad += OnSceneEarlyLoad;
        _harmony.PatchAll();
    }

    [OnExit]
    public void OnDisable()
    {
        _harmony.UnpatchSelf();
        SceneContext_Awake.sceneEarlyLoad -= OnSceneEarlyLoad;
    }

    private void OnSceneEarlyLoad(Scene scene)
    {
        FeatureManager.instance.AddFeaturesAndRestartOpenXR();
    }
}

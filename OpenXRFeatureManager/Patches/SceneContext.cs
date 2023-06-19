// <copyright file="SceneContext.cs" company="nicoco007">
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
using HarmonyLib;
using UnityEngine.SceneManagement;
using Zenject;

namespace OpenXRFeatureManager.Patches;

/// <summary>
/// Patch targeting <see cref="SceneContext.Awake"/> so code can be run before basically anything else in a loaded scene.
/// </summary>
/// <remarks>
/// We need to run the XR restart logic after the splash screen scene unloads but before anything else.
/// Awake will run before before OnSceneLoaded gets invoked so we can make changes before the first SceneContext initializes.
/// </remarks>
[HarmonyPatch(typeof(SceneContext), nameof(SceneContext.Awake))]
internal class SceneContext_Awake
{
    /// <summary>
    /// Invoked before a <see cref="SceneContext"/>'s Awake function runs.
    /// </summary>
    internal static event Action<Scene>? sceneEarlyLoad;

    private static void Prefix(SceneContext __instance)
    {
        sceneEarlyLoad?.Invoke(__instance.gameObject.scene);
    }
}

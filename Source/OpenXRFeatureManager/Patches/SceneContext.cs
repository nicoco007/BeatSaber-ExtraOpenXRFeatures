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
using System.Threading.Tasks;
using HarmonyLib;
using Zenject;

namespace OpenXRFeatureManager.Patches;

/// <summary>
/// Patch targeting <see cref="SceneContext.Awake"/> so code can be run before basically anything else in a loaded scene.
/// </summary>
/// <remarks>
/// We need to run the OpenXR restart logic after the splash screen scene but before anything else.
/// Awake will run before before OnSceneLoaded gets invoked so we can make changes before the first SceneContext (PCInit) initializes.
/// </remarks>
[HarmonyPatch(typeof(SceneContext), nameof(SceneContext.Awake))]
internal class SceneContext_Awake
{
    [HarmonyPriority(Priority.Last)]
    private static bool Prefix(SceneContext __instance)
    {
        if (FeatureManager.instance.initialized)
        {
            return true;
        }

        FeatureManager.instance.AddFeaturesAndRestartOpenXRAsync().ContinueWith(
            (task) =>
            {
                if (task.Exception != null)
                {
                    Plugin.log.Error($"Failed to set up OpenXR features\n{task.Exception}");
                }

                Awake(__instance);
            },
            TaskScheduler.FromCurrentSynchronizationContext());

        // This will break any postfix patches from other mods. We could transpile the method instead to allow
        // them to run, but since postfixes would likely rely on the original Awake running they'd break anyway.
        return false;
    }

    [HarmonyReversePatch]
    private static void Awake(object instance) => throw new NotImplementedException();
}

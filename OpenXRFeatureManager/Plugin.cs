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

namespace OpenXRFeatureManager;

[Plugin(RuntimeOptions.SingleStartInit)]
public class Plugin
{
    private readonly Harmony _harmony = new("com.nicoco007.openxr-feature-manager");

    [Init]
    public Plugin(Logger logger)
    {
        log = logger;

        FeatureManager.instance = new FeatureManager(logger.GetChildLogger(nameof(FeatureManager)));
    }

    internal static Logger log { get; private set; } = null!;

    [OnStart]
    public void OnEnable()
    {
        _harmony.PatchAll();
    }

    [OnExit]
    public void OnDisable()
    {
        _harmony.UnpatchSelf();
    }
}

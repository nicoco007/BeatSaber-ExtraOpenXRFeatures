// <copyright file="Plugin.cs" company="nicoco007">
// This file is part of OpenXRTrackerProfiles.
//
// OpenXRTrackerProfiles is free software: you can redistribute it and/or modify it under the terms
// of the GNU General Public License as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
//
// OpenXRTrackerProfiles is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with OpenXRTrackerProfiles.
// If not, see https://www.gnu.org/licenses/.
// </copyright>

using IPA;
using OpenXRFeatureManager;
using OpenXRTrackerProfiles.Features;
using Logger = IPA.Logging.Logger;

namespace OpenXRTrackerProfiles;

[Plugin(RuntimeOptions.SingleStartInit)]
public class Plugin
{
    [Init]
    public Plugin(Logger logger)
    {
        log = logger;
    }

    internal static Logger log { get; private set; } = null!;

    [OnStart]
    public void OnStart()
    {
        FeatureManager.instance.RegisterFeature(FeatureManager.CreateOpenXRFeature<HTCViveTrackerProfile>(HTCViveTrackerProfile.featureId, name: HTCViveTrackerProfile.uiName, company: "nicoco007", openXRExtensionStrings: HTCViveTrackerProfile.extensionString));
    }

    [OnExit]
    public void OnDisable()
    {
    }
}

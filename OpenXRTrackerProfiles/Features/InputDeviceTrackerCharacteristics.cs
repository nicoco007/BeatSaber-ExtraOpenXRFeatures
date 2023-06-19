// <copyright file="InputDeviceTrackerCharacteristics.cs" company="nicoco007">
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

using System;

namespace OpenXRTrackerProfiles.Features;

/// <summary>
/// A set of bit flags describing potential <see cref="XRTracker" /> characteristics.
/// </summary>
[Flags]
public enum InputDeviceTrackerCharacteristics : uint
{
    /// <summary>
    /// The user's left foot.
    /// </summary>
    TrackerLeftFoot = 0x1000u,

    /// <summary>
    /// The user's right foot.
    /// </summary>
    TrackerRightFoot = 0x2000u,

    /// <summary>
    /// The user's left shoulder.
    /// </summary>
    TrackerLeftShoulder = 0x4000u,

    /// <summary>
    /// The user's right shoulder.
    /// </summary>
    TrackerRightShoulder = 0x8000u,

    /// <summary>
    /// The user's left elbow.
    /// </summary>
    TrackerLeftElbow = 0x10000u,

    /// <summary>
    /// The user's right elbow.
    /// </summary>
    TrackerRightElbow = 0x20000u,

    /// <summary>
    /// The user's left knee.
    /// </summary>
    TrackerLeftKnee = 0x40000u,

    /// <summary>
    /// The user's right knee.
    /// </summary>
    TrackerRightKnee = 0x80000u,

    /// <summary>
    /// The user's waist.
    /// </summary>
    TrackerWaist = 0x100000u,

    /// <summary>
    /// The user's chest.
    /// </summary>
    TrackerChest = 0x200000u,

    /// <summary>
    /// A camera.
    /// </summary>
    TrackerCamera = 0x400000u,

    /// <summary>
    /// A keyboard.
    /// </summary>
    TrackerKeyboard = 0x800000u,
}

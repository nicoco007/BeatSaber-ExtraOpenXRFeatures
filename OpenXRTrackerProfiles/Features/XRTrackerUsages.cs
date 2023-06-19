// <copyright file="XRTrackerUsages.cs" company="nicoco007">
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

using UnityEngine.InputSystem.Utilities;

namespace OpenXRTrackerProfiles.Features;

/// <summary>
/// All potential device usages for <see cref="XRTracker" /> devices.
/// </summary>
public class XRTrackerUsages
{
    /// <summary>
    /// Device on left foot.
    /// </summary>
    public static readonly InternedString LeftFoot = new("LeftFoot");

    /// <summary>
    /// Device on right foot.
    /// </summary>
    public static readonly InternedString RightFoot = new("RightFoot");

    /// <summary>
    /// Device on left shoulder.
    /// </summary>
    public static readonly InternedString LeftShoulder = new("LeftShoulder");

    /// <summary>
    /// Device on right shoulder.
    /// </summary>
    public static readonly InternedString RightShoulder = new("RightShoulder");

    /// <summary>
    /// Device on left elbow.
    /// </summary>
    public static readonly InternedString LeftElbow = new("LeftElbow");

    /// <summary>
    /// Device on right elbow.
    /// </summary>
    public static readonly InternedString RightElbow = new("RightElbow");

    /// <summary>
    /// Device on left knee.
    /// </summary>
    public static readonly InternedString LeftKnee = new("LeftKnee");

    /// <summary>
    /// Device on right knee.
    /// </summary>
    public static readonly InternedString RightKnee = new("RightKnee");

    /// <summary>
    /// Device on waist.
    /// </summary>
    public static readonly InternedString Waist = new("Waist");

    /// <summary>
    /// Device on chest.
    /// </summary>
    public static readonly InternedString Chest = new("Chest");

    /// <summary>
    /// Device on a camera.
    /// </summary>
    public static readonly InternedString Camera = new("Camera");

    /// <summary>
    /// Device on a keyboard.
    /// </summary>
    public static readonly InternedString Keyboard = new("Keyboard");
}

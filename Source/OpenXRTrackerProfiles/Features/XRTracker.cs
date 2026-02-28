// <copyright file="XRTracker.cs" company="nicoco007">
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

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

namespace OpenXRTrackerProfiles.Features;

/// <summary>
/// A base Input System <see cref="TrackedDevice"/> for XR trackers.
/// </summary>
[InputControlLayout(isGenericTypeOfDevice = true, displayName = "XR Tracker")]
public class XRTracker : TrackedDevice
{
}

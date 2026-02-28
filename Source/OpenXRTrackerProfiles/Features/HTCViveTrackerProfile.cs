// <copyright file="HTCViveTrackerProfile.cs" company="nicoco007">
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

using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;
using UnityEngine.Scripting;
using UnityEngine.XR;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;

namespace OpenXRTrackerProfiles.Features;

/// <summary>
/// This <see cref="OpenXRInteractionFeature" /> enables the use of HTC Vive Trackers interaction profiles in OpenXR.
/// Heavily inspired by <a href="https://forum.unity.com/threads/openxr-and-openvr-together.1113136/#post-7803057">this Unity Forum post</a>.
/// </summary>
public class HTCViveTrackerProfile : OpenXRInteractionFeature
{
    /// <summary>
    /// The display name used for this profile.
    /// </summary>
    public const string uiName = "HTC Vive Tracker Profile";

    /// <summary>
    /// The feature id string. This is used to give the feature a well known id for reference.
    /// </summary>
    public const string featureId = "com.massive.openxr.feature.input.htcvivetracker";

    /// <summary>
    /// The interaction profile string used to reference the <a href="https://www.khronos.org/registry/OpenXR/specs/1.0/html/xrspec.html#:~:text=in%20this%20case.-,VIVE%20Tracker%20interaction%20profile,-Interaction%20profile%20path">HTC Vive Tracker</a>.
    /// </summary>
    public const string interactionProfileName = "/interaction_profiles/htc/vive_tracker_htcx";

    /// <summary>
    /// The name of the OpenXR extension that supports the Vive Tracker.
    /// </summary>
    public const string extensionString = "XR_HTCX_vive_tracker_interaction";

    private const string deviceLocalizedName = "HTC Vive Tracker OpenXR";

    /// <summary>
    /// Registers the <see cref="XRViveTracker" /> layout with the Input System.
    /// </summary>
    protected override void RegisterDeviceLayout()
    {
        InputSystem.RegisterLayout<XRTracker>();

        InputSystem.RegisterLayout(
            typeof(XRViveTracker),
            matches: default(InputDeviceMatcher)
                .WithInterface(XRUtilities.InterfaceMatchAnyVersion)
                .WithProduct(deviceLocalizedName));
    }

    /// <summary>
    /// Removes the <see cref="XRViveTracker" /> layout from the Input System.
    /// </summary>
    protected override void UnregisterDeviceLayout()
    {
        InputSystem.RemoveLayout(nameof(XRViveTracker));
        InputSystem.RemoveLayout(nameof(XRTracker));
    }

    /// <inheritdoc />
    protected override void RegisterActionMapsWithRuntime()
    {
        ActionMapConfig actionMap = new()
        {
            name = "htcvivetracker",
            localizedName = deviceLocalizedName,
            desiredInteractionProfile = interactionProfileName,
            manufacturer = "HTC",
            serialNumber = string.Empty,
            deviceInfos = new List<DeviceConfig>()
            {
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerLeftFoot,
                    userPath = TrackerUserPaths.leftFoot,
                },
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerRightFoot,
                    userPath = TrackerUserPaths.rightFoot,
                },
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerLeftShoulder,
                    userPath = TrackerUserPaths.leftShoulder,
                },
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerRightShoulder,
                    userPath = TrackerUserPaths.rightShoulder,
                },
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerLeftElbow,
                    userPath = TrackerUserPaths.leftElbow,
                },
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerRightElbow,
                    userPath = TrackerUserPaths.rightElbow,
                },
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerLeftKnee,
                    userPath = TrackerUserPaths.leftKnee,
                },
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerRightKnee,
                    userPath = TrackerUserPaths.rightKnee,
                },
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerWaist,
                    userPath = TrackerUserPaths.waist,
                },
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerChest,
                    userPath = TrackerUserPaths.chest,
                },
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerCamera,
                    userPath = TrackerUserPaths.camera,
                },
                new DeviceConfig()
                {
                    characteristics = InputDeviceCharacteristics.TrackedDevice | (InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerKeyboard,
                    userPath = TrackerUserPaths.keyboard,
                },
            },
            actions = new List<ActionConfig>()
            {
                new ActionConfig()
                {
                    name = "devicePose",
                    localizedName = "Device Pose",
                    type = ActionType.Pose,
                    usages = new List<string>()
                    {
                        "Device",
                    },
                    bindings = new List<ActionBinding>()
                    {
                        new ActionBinding()
                        {
                            interactionPath = TrackerComponentPaths.grip,
                            interactionProfileName = interactionProfileName,
                        },
                    },
                },
            },
        };

        AddActionMap(actionMap);
    }

    /// <inheritdoc />
    protected override bool OnInstanceCreate(ulong xrInstance)
    {
        bool result = base.OnInstanceCreate(xrInstance) && OpenXRRuntime.IsExtensionEnabled(extensionString);

        if (result)
        {
            Plugin.log.Info("HTC Vive Tracker extension enabled");
        }
        else
        {
            Plugin.log.Error("HTC Vive Tracker extension not enabled");
        }

        return result;
    }

    /// <summary>
    /// OpenXR user path definitions for the tracker.
    /// </summary>
    public static class TrackerUserPaths
    {
        /// <summary>
        /// Path for user left foot.
        /// </summary>
        public const string leftFoot = "/user/vive_tracker_htcx/role/left_foot";

        /// <summary>
        /// Path for user right foot.
        /// </summary>
        public const string rightFoot = "/user/vive_tracker_htcx/role/right_foot";

        /// <summary>
        /// Path for user left shoulder.
        /// </summary>
        public const string leftShoulder = "/user/vive_tracker_htcx/role/left_shoulder";

        /// <summary>
        /// Path for user right shoulder.
        /// </summary>
        public const string rightShoulder = "/user/vive_tracker_htcx/role/right_shoulder";

        /// <summary>
        /// Path for user left elbow.
        /// </summary>
        public const string leftElbow = "/user/vive_tracker_htcx/role/left_elbow";

        /// <summary>
        /// Path for user right elbow.
        /// </summary>
        public const string rightElbow = "/user/vive_tracker_htcx/role/right_elbow";

        /// <summary>
        /// Path for user left knee.
        /// </summary>
        public const string leftKnee = "/user/vive_tracker_htcx/role/left_knee";

        /// <summary>
        /// Path for user right knee.
        /// </summary>
        public const string rightKnee = "/user/vive_tracker_htcx/role/right_knee";

        /// <summary>
        /// Path for user waist.
        /// </summary>
        public const string waist = "/user/vive_tracker_htcx/role/waist";

        /// <summary>
        /// Path for user chest.
        /// </summary>
        public const string chest = "/user/vive_tracker_htcx/role/chest";

        /// <summary>
        /// Path for user custom camera.
        /// </summary>
        public const string camera = "/user/vive_tracker_htcx/role/camera";

        /// <summary>
        /// Path for user keyboard.
        /// </summary>
        public const string keyboard = "/user/vive_tracker_htcx/role/keyboard";
    }

    /// <summary>
    /// OpenXR component path definitions for the tracker.
    /// </summary>
    public static class TrackerComponentPaths
    {
        /// <summary>
        /// Constant for a pose interaction binding '.../input/grip/pose' OpenXR Input Binding. Used by input subsystem to bind actions to physical inputs.
        /// </summary>
        public const string grip = "/input/grip/pose";
    }

    /// <summary>
    /// An Input System device based off the <a href="https://www.khronos.org/registry/OpenXR/specs/1.0/html/xrspec.html#_htc_vive_controller_profile">HTC Vive Tracker</a>.
    /// </summary>
    [Preserve]
    [InputControlLayout(displayName = "HTC Vive Tracker (OpenXR)", commonUsages = new[] { "LeftFoot", "RightFoot", "LeftShoulder", "RightShoulder", "LeftElbow", "RightElbow", "LeftKnee", "RightKnee", "Waist", "Chest", "Camera", "Keyboard" })]
    public class XRViveTracker : XRTracker
    {
        /// <summary>
        /// Gets the <see cref="PoseControl" /> that represents information from the <see cref="TrackerComponentPaths.grip" /> OpenXR binding.
        /// </summary>
        [Preserve]
        [InputControl(offset = 0, aliases = new[] { "device", "gripPose" }, usage = "Device", noisy = true)]
        public PoseControl? devicePose { get; private set; }

        /// <summary>
        /// Gets the <see cref="Vector3Control" /> required for back compatibility with the XRSDK layouts. This value is equivalent to mapping devicePose/position.
        /// </summary>
        [Preserve]
        [InputControl(offset = 8, alias = "gripPosition", noisy = true)]
        public new Vector3Control? devicePosition { get; private set; }

        /// <summary>
        /// Gets the <see cref="QuaternionControl" /> required for backwards compatibility with the XRSDK layouts. This value is equivalent to mapping devicePose/rotation.
        /// </summary>
        [Preserve]
        [InputControl(offset = 20, alias = "gripOrientation", noisy = true)]
        public new QuaternionControl? deviceRotation { get; private set; }

        /// <summary>
        /// Gets the <see cref="ButtonControl" /> required for backwards compatibility with the XRSDK layouts. This value is equivalent to mapping devicePose/isTracked.
        /// </summary>
        [Preserve]
        [InputControl(offset = 60)]
        public new ButtonControl? isTracked { get; private set; }

        /// <summary>
        /// Gets the <see cref="ButtonControl" /> required for backwards compatibility with the XRSDK layouts. This value is equivalent to mapping devicePose/trackingState.
        /// </summary>
        [Preserve]
        [InputControl(offset = 64)]
        public new IntegerControl? trackingState { get; private set; }

        /// <inheritdoc />
        protected override void FinishSetup()
        {
            base.FinishSetup();
            devicePose = GetChildControl<PoseControl>("devicePose");
            devicePosition = GetChildControl<Vector3Control>("devicePosition");
            deviceRotation = GetChildControl<QuaternionControl>("deviceRotation");
            isTracked = GetChildControl<ButtonControl>("isTracked");
            trackingState = GetChildControl<IntegerControl>("trackingState");

            string capabilities = description.capabilities;
            var deviceDescriptor = XRDeviceDescriptor.FromJson(capabilities);

            if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerLeftFoot))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.LeftFoot);
            }
            else if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerRightFoot))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.RightFoot);
            }
            else if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerLeftShoulder))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.LeftShoulder);
            }
            else if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerRightShoulder))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.RightShoulder);
            }
            else if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerLeftElbow))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.LeftElbow);
            }
            else if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerRightElbow))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.RightElbow);
            }
            else if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerLeftKnee))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.LeftKnee);
            }
            else if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerRightKnee))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.RightKnee);
            }
            else if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerWaist))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.Waist);
            }
            else if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerChest))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.Chest);
            }
            else if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerCamera))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.Camera);
            }
            else if (deviceDescriptor.characteristics.HasFlag((InputDeviceCharacteristics)InputDeviceTrackerCharacteristics.TrackerKeyboard))
            {
                InputSystem.SetDeviceUsage(this, XRTrackerUsages.Keyboard);
            }
        }
    }
}

# OpenXR Tracker Profiles
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/nicoco007/BeatSaber-ExtraOpenXRFeatures/openxr-tracker-profiles.yml?style=flat-square)](https://github.com/nicoco007/BeatSaber-ExtraOpenXRFeatures/actions/workflows/openxr-tracker-profiles.yml)
[![Latest Release](https://img.shields.io/github/v/release/nicoco007/BeatSaber-ExtraOpenXRFeatures?style=flat-square&filter=openxr-tracker-profiles/*)](https://github.com/nicoco007/BeatSaber-ExtraOpenXRFeatures/releases)

A Beat Saber mod that adds profiles to OpenXR to support more devices such as Vive Trackers.

> [!NOTE]
> There is currently a limitation in Unity's OpenXR implementation that prevents trackers from being recognized if turned on **after** the game is started. If you do this, you will need to restart the game before trackers are recognized in-game. [This Unity Forum thread](https://forum.unity.com/threads/openxr-detecting-vive-tracker-connection-after-launching-the-app.1418659/) is the latest information I could find regarding the situation. There is unfortunately nothing I can do about this since from what I've gathered this is an issue on the native side.

## Installation
Simply put `OpenXRTrackerProfiles.dll` in your game's `Plugins` folder. There is no additional configuration necessary on the game's side.

### Assigning SteamVR Tracker Roles
For trackers to show up in-game, you will need to assign roles to your trackers through SteamVR.

To do so, right-click on any tracker and press "Manage Trackers." You can also open the SteamVR context menu, press "Settings", and press "Controllers."

![SteamVR Manage Trackers](Documentation/steamvr_manage_trackers.png)

Then, press "Manage Trackers" again.

![SteamVR Settings Manage Trackers](Documentation/steamvr_settings_manage_trackers.png)

You will be presented with a list containing your trackers.

![SteamVR Vive Trackers (Disabled)](Documentation/steamvr_vive_trackers_disabled.png)

Simply select a value in the dropdown list to assign the tracker to that role. **Note that due to how OpenXR works, only one tracker per role will be recognized by the game at a time**. Press "Close" to save your settings.

![SteamVR Vive Trackers (Disabled)](Documentation/steamvr_vive_trackers_list.png)

For Vive 2.0 trackers (and potentially others), the serial number presented by SteamVR unfortunately is not written down on the device itself. To identify the trackers, I recommend turning them on one at a time so the circle next to the serial number can be used to identify the tracker that is turned on.

![SteamVR Vive Trackers (Disabled)](Documentation/steamvr_vive_trackers_connected.png)

That's it! Your trackers should now be usable in-game. If you are using VMT, follow the additional instructions below to finish setting up.

### VirtualMotionTracker (VMT) Additional Setup

> [!IMPORTANT]
> If you haven't already, set up your tracker roles as explained above.

1.  Start up SteamVR without starting Beat Saber. If you started Beat Saber during your current SteamVR session, restart SteamVR. Then, open SteamVR's controller settings.

    ![](Documentation/VMT/OpenControllerSettings.png)

2.  In the bottom left corner, press the "Advanced Settings" toggle.

    ![](Documentation/VMT/ShowAdvancedSettings.png)

3.  Press the now revealed "Show Binding UI" button.

    ![](Documentation/VMT/ShowBindingUI.png)

4.  In the new window that opens, press the "Show More Applications" button.

    ![](Documentation/VMT/ShowMoreApplications.png)

5.  Select "Beat Saber" in the list. If it isn't your most recently played game, you may need to scroll down a bit.

    ![](Documentation/VMT/ShowMoreApplicationsBeatSaber.png)

6.  Wait for the binding under "Current Binding" to show up, then press the "Back" button in the top right corner.

    ![](Documentation/VMT/LegacyBindingsBack.png)

7.  Start Beat Saber normally and **wait for it to fully load a scene (i.e. the health warning or main menu is visible)**. Once it has loaded, press the "Beat Saber" button that should now be at the top of the applications list.

    ![](Documentation/VMT/ApplicationsBeatSaber.png)

8.  Wait once more for the bindings to load, then press the controller's name under "Current Controller" (in this case, "Index Controller").

    ![](Documentation/VMT/SelectController.png)

9.  In the list that pops up, you should see all the VMT trackers you set up previously. Depending on which trackers you set up, you should see some or all of "vmt_leftfoot", "vmt_rightfoot", and "vmt_waist". Select the first tracker (ignore the entry called simply "vmt"). Here, we are choosing "vmt_leftfoot".

    ![](Documentation/VMT/SelectTracker.png)

10. Under "Current Binding," press the "Edit" button. If you do not see a binding under "Current Binding," close SteamVR and restart from step 1. The binding will not show up if you do not open the Binding UI before starting Beat Saber.

    ![](Documentation/VMT/VmtLeftFootEdit.png)

11. Select "htcvivetracker" at the top. If you do not see "htcvivetracker" you either did not wait long enough after starting the game or haven't installed OpenXRTrackerProfiles properly. Press the "Back" button in the top left corner of the window, make sure it is installed correctly, restart the game, and press the "Edit" button again.

    ![](Documentation/VMT/HtcViveTracker.png)

12. Press the "Poses" button in the middle of the screen.

    ![](Documentation/VMT/Poses.png)

13. In the top left of the pop up that opens, under "Poses", press the "unused" button next to `/user/foot/left/pose/raw` (`left` will be replaced by `right` or `waist` depending on the tracker you selected earlier).

    ![](Documentation/VMT/Unused.png)

14. In the pop up that opens, select "devicepose".

    ![](Documentation/VMT/DevicePose.png)

15. Press the "Close" button at the bottom of the popup.

    ![](Documentation/VMT/ClosePoses.png)

16. Wait for the "Saving..." box near the top of the window to disappear.

    ![](Documentation/VMT/WaitSaving.png)

17. Press the "Save Personal Binding" button in the bottom center of the window.

    ![](Documentation/VMT/SavePersonalBinding.png)

18. Change the name and description if you want, then press "Save".

    ![](Documentation/VMT/PersonalBindingSaveButton.png)

19. Once the binding has saved, press the "Back" button in the top left corner.

    ![](Documentation/VMT/BindingBack.png)

20. You should now see your binding under "Current Binding". If it doesn't show up, press the "Back" button in the top left corner and press "Beat Saber" in the list again.

    ![](Documentation/VMT/CustomBindingVisible.png)

21. Repeat steps 8 to 20 for any other trackers that were in the list in step 9.

You should now be able to use VMT in-game!

## Usage (for developers)
I highly recommend using the new Unity Input System to access tracked devices with OpenXR. You can technically still use the legacy `UnityEngine.XR.InputDevices` system, but since all controllers and trackers in OpenXR use unique well-defined paths, there is no advantage to using `InputDevices` for trackers specifically anymore. Only a single tracker is available for each role so if, for example, you're trying to set up full body tracking, automatically assuming tracker roles based on position as was previously done is no longer really possible nor necessary.

Accessing trackers using the Input System is fairly straightforward. First, create an `InputAction` for the role you want to track:

```csharp
// Create an InputAction directly
var inputAction = new InputAction("Waist Pose");

// Create an InputAction on an InputActionMap (better if you will be using multiple InputActions):
var inputActionMap = new InputActionMap("My Input Actions");
var inputAction = inputActionMap.AddAction("Waist Pose");
```

Then, add a binding for that action:
```csharp
inputAction.AddBinding("<XRTracker>{Waist}/devicePose");
```

The syntax for the binding path is as follows:
- `<XRTracker>` is the type of `TrackedDevice` to use. It must be enclosed in angle brackets `<>`. For trackers, this should always be `XRTracker`.
- `{Waist}` is the device usage. It must be enclosed in curly brackets `{}`. You can either put a string directly or reference a constant from `OpenXRTrackerProfiles.XRTrackerUsages`. For trackers, this currently includes following values:
  - `LeftFoot`
  - `RightFoot`
  - `LeftShoulder`
  - `RightShoulder`
  - `LeftElbow`
  - `RightElbow`
  - `LeftKnee`
  - `RightKnee`
  - `Waist`
  - `Chest`
  - `Camera`
  - `Keyboard`
- `/devicePose` is the path to the control to use. It must begin with a slash `/`. This path can be simply the root input control or can specify a child control, e.g. `/devicePose/position`. You can check all the avaiable options by looking at the available `InputControl`s on the input device (e.g. `XRViveTracker`). For trackers, these are the options:
  - `devicePose` (`UnityEngine.InputSystem.XR.PoseState`): Provides tracking state, position, rotation, velocity, and angular velocity. It has the following child controls:
    - `isTracked` (`float`): Whether or not the device is currently tracked. The value will be 1 if the device is being tracked and 0 if not.
    - `trackingState` (`int`): Which values (position, rotation, velocity, etc.) are currently being tracked. Should be cast to `UnityEngine.XR.InputTrackingState`.
    - `position` (`Vector3`): The current position of the device.
    - `rotation` (`Quaternion`): The current rotation of the device.
    - `velocity` (`Vector3`): The current velocity of the device.
    - `angularVelocity` (`Vector3`): The current angular velocity of the device.
  - `isTracked` (`float`): Whether or not the device is currently tracked. Same as `/devicePose/isTracked`.
  - `trackingState` (`int`): Which values are currently being tracked. Same as `/devicePose/trackingState`.
  - `devicePosition` (`Vector3`): The current position of the device. Same as `/devicePose/position`.
  - `deviceRotation` (`Quaternion`): The current rotation of the device. Same as `/devicePose/rotation`.

You can then enable the action by simply calling `Enable()`.
```csharp
// Single action
inputAction.Enable();

// Action map
inputActionMap.Enable();
```

To access the input's value, simply use `ReadValue`. Note that if the headset has presence detection, a value will only be provided if presence is detected (i.e. if the headset is on the user's head).
```csharp
var pose = inputAction.ReadValue<Pose>();
```

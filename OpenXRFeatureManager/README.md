# OpenXR Feature Manager
A Beat Saber mod that manages adding OpenXR features at runtime.

This mod consolidates logic to add features to `OpenXRSettings`. Since OpenXR needs to restart for feature changes to apply, this avoids the need for each mod that adds features to restart OpenXR and therefore reduces startup delay.

## Usage (for developers)
You can take a look at how [OpenXR Hands](../OpenXRHands) and [OpenXR Tracker Profiles](../OpenXRTrackerProfiles) use the mod.

The public API basically consists of:
- `FeatureManager.CreateOpenXRFeature<T>(...)` to create an `OpenXRFeature` scriptable object where `T` is the type of the feature (must inherit `OpenXRFeature`).
- `FeatureManager.instance.RegisterFeature(...)` to register a feature (usually created with the method above).

You can use the `beforeOpenXRReloaded` and `afterOpenXRUnloaded` events to do things while this mod applies new features and restarts OpenXR. For example, OpenXR Hands uses it to register/initialize the features that are being added.

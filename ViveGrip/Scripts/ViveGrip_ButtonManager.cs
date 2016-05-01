﻿using UnityEngine;
using System.Collections;

public class ViveGrip_ButtonManager : MonoBehaviour {
  public enum ViveInput {
    Grip,
    Trigger
  }; // TODO: add more buttons
  public SteamVR_TrackedObject trackedObject;
  public ViveInput grab = ViveInput.Grip;
  public ViveInput interact = ViveInput.Trigger;

  void Start() {}

  public bool Pressed(string action) {
    ulong rawInput = ConvertString(action);
    return Device().GetTouchDown(rawInput);
  }

  public bool Released(string action) {
    ulong rawInput = ConvertString(action);
    return Device().GetTouchUp(rawInput);
  }

  public bool Holding(string action) {
    ulong rawInput = ConvertString(action);
    return Device().GetTouch(rawInput);
  }

  ulong ConvertString(string action) {
    ViveInput input = GetInputFor(action);
    return Decode(input);
  }

  SteamVR_Controller.Device Device() {
    return SteamVR_Controller.Input((int)trackedObject.index);
  }

  ViveInput GetInputFor(string action) {
    switch (action.ToLower()) {
      default:
      case "grab":
        return grab;
      case "interact":
        return interact;
    }
  }

  ulong Decode(ViveInput input) {
    switch ((int)input) {
      default:
      case 0:
        return SteamVR_Controller.ButtonMask.Grip;
      case 1:
        return SteamVR_Controller.ButtonMask.Trigger;
    }
  }
}

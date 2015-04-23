using System;
using System.Collections;
using UnityEngine;
using InControl;


// This custom profile is enabled by adding it to the Custom Profiles list
// on the InControlManager component, or you can attach it yourself like so:
// InputManager.AttachDevice( new UnityInputDevice( "KeyboardAndMouseProfile" ) );
//
public class KeyboardAndMouseProfile : UnityInputDeviceProfile
{
  public KeyboardAndMouseProfile()
  {
    Name = "Keyboard/Mouse";
    Meta = "Keyboard and mouse profile.";

    // This profile only works on desktops.
    SupportedPlatforms = new[]
    {
      "Windows",
      "Mac",
      "Linux"
    };

    Sensitivity = 1.0f;
    LowerDeadZone = 0.0f;
    UpperDeadZone = 1.0f;

    ButtonMappings = new[]
    {
      // new InputControlMapping
      // {
      //   Handle = "Fire - Mouse",
      //   Target = InputControlType.Action1,
      //   Source = MouseButton0
      // },
      new InputControlMapping
      {
        Handle = "Fire - Keyboard",
        Target = InputControlType.Action1,
        // KeyCodeButton fires when any of the provided KeyCode params are down.
        Source = KeyCodeButton( KeyCode.Space, KeyCode.Return )
      }
    };

    AnalogMappings = new[]
    {
      new InputControlMapping
      {
        Handle = "Move X",
        Target = InputControlType.LeftStickX,
        // KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
        Source = KeyCodeAxis( KeyCode.LeftArrow, KeyCode.RightArrow )
      },
      new InputControlMapping
      {
        Handle = "Move Y",
        Target = InputControlType.LeftStickY,
        // Notes that up is positive in Unity, therefore the order of KeyCodes is down, up.
        Source = KeyCodeAxis( KeyCode.DownArrow, KeyCode.UpArrow )
      }
    };
  }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    // -- Axis

    public static float LeftHorizontal(int playerNumber)
    {
        float r = 0.0f;
        r += Input.GetAxis("J" + playerNumber + "LeftHorizontal");
        r += Input.GetAxis("K" + playerNumber + "MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float LeftVertical(int playerNumber)
    {
        float r = 0.0f;
        r += Input.GetAxis("J" + playerNumber + "LeftVertical");
        r += Input.GetAxis("K" + playerNumber + "MainVertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 LeftJoystick(int playerNumber)
    {
        return new Vector3(LeftHorizontal(playerNumber), 0, LeftVertical(playerNumber));
    }

    // -- Buttons

    public static bool AButton(int playerNumber)
    {
        return Input.GetButtonDown("J" + playerNumber + "AButton");
    }

    public static bool BButton(int playerNumber)
    {
        return Input.GetButtonDown("J" + playerNumber + "BButton");
    }

    public static bool XButton(int playerNumber)
    {
        return Input.GetButtonDown("J" + playerNumber + "XButton");
    }

    public static bool YButton(int playerNumber)
    {
        return Input.GetButtonDown("J" + playerNumber + "YButton");
    }

}

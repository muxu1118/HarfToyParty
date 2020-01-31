using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick 1 button 0"))
        {
            Debug.Log("JoyStick0");
        }
        if (Input.GetKeyDown("joystick 1 button 1"))
        {
            Debug.Log("JoyStick1");
        }
        if (Input.GetKeyDown("joystick 1 button 2"))
        {
            Debug.Log("JoyStick2");
        }
        if (Input.GetKeyDown("joystick 1 button 3"))
        {
            Debug.Log("JoyStick3");
        }

    }
}

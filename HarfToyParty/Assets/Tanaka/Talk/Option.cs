using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject menu;

    private bool activeFlag = false;

    private void Start()
    {
        menu.SetActive(true);
    }

    public void Menumenu()
    {
        if (Input.GetKeyDown("joystick 1 button 2"))
        {
            Panel.SetActive(true);
            Debug.Log("押されてるよ");
        }
    }

    public void Numenume()
    {
        if (Input.GetKeyDown("joystick 1 button 2"))
            Panel.SetActive(false);
    }
}

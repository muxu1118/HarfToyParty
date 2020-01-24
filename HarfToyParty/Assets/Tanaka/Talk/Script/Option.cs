using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject option;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject Verification;

    private bool activeFlag = true;

    private void Start()
    {
        option.SetActive(false);
        Verification.SetActive(false);
    }

    public void Update()
    {
        Skip();
        SkipVerification();
        End();

        if (Input.GetKeyDown("joystick 1 button 5"))    // RB
        {
            if (activeFlag)
            {
                Menumenu();
                Debug.Log("aaaa");
            }
            else
            {
                Numenume();
                Debug.Log("bbbb");
            }
            activeFlag = !activeFlag;
        }
    }

    public void Menumenu()
    {
        option.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Numenume()
    {
        option.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Skip()
    {
        if (Input.GetKeyDown("joystick 1 button 2"))
        {
            if (option.activeSelf == false)
            {
                Verification.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void SkipVerification()
    {
        if (Verification.activeSelf == true)
        {
            if (Input.GetKeyDown("joystick 1 button 1"))
            {
                Verification.SetActive(false);
                Time.timeScale = 1f;
            }

            else if (Input.GetKeyDown("joystick 1 button 3"))
                SceneManager.LoadScene("Choice");
        }
    }

    public void End()
    {
        if (Input.GetKeyDown("joystick 1 button 3"))
        {
            if (option.activeSelf == true)
                SceneManager.LoadScene("Title");
        }
    }
}

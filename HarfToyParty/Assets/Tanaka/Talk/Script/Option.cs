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
        End();
        Move();

        if (Input.GetKeyDown("joystick 1 button 5"))    // RB
        {
            if (Verification.activeSelf == false)
            {
                if (activeFlag)
                    Menumenu(); 
                else
                    Numenume();
                    
                activeFlag = !activeFlag;
            }
        }

        else if (Input.GetKeyDown("joystick 1 button 1"))    //A
        {
            if (activeFlag)
                Skip();
            else
                SkipVerification();
            
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
        if (option.activeSelf == false)
        {
            Verification.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void SkipVerification()
    {
        if (Verification.activeSelf == true)
        {
            Verification.SetActive(false);
            Time.timeScale = 1f;
        }

    }

    public void Move()
    {
        if (Input.GetKeyDown("joystick 1 button 3"))   // Y
            if (Verification.activeSelf == true)
                SceneManager.LoadScene("Choice");
    }

    public void End()
    {
        if (Input.GetKeyDown("joystick 1 button 3"))    // Y
            if (option.activeSelf == true)
                SceneManager.LoadScene("Title");
    }
}

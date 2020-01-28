using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject option;
    [SerializeField] GameObject menu;
    //[SerializeField] GameObject SkipMenu;

    private bool activeFlag = true;

    private void Start()
    {
        option.SetActive(false);
        //SkipMenu.SetActive(false);
    }

    public void Update()
    {
        End();
        //Move();
        // RB で activeFlagの入れ替え
        if (Input.GetKeyDown("joystick 1 button 5"))    // RB
        {
            //if (SkipMenu.activeSelf == false)
            //{
                if (activeFlag)
                    Menumenu();
                else
                    Numenume();

                activeFlag = !activeFlag;
            //}
        }

        // A で activeFlag の入れ替え
        //else if (Input.GetKeyDown("joystick 1 button 1"))    //A
        //{
        //    if (activeFlag)
        //        Skip();
        //    else
        //        SkipMenumenu();
            
        //    activeFlag = !activeFlag;
        //}
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

    //public void Skip()
    //{
    //    if (option.activeSelf == false)
    //    {
    //        SkipMenu.SetActive(true);
    //        Time.timeScale = 0f;
    //    }
    //}

    //public void SkipMenumenu()
    //{
    //    if (SkipMenu.activeSelf == true)
    //    {
    //        SkipMenu.SetActive(false);
    //        Time.timeScale = 1f;
    //    }
    //}

    //public void Move()
    //{
    //    if (Input.GetKeyDown("joystick 1 button 3"))   // Y
    //        if (SkipMenu.activeSelf == true)
    //            SceneManager.LoadScene("Choice");
    //}

    public void End()
    {
        if (Input.GetKeyDown("joystick 1 button 3"))    // Y
            if (option.activeSelf == true)
                SceneManager.LoadScene("Choice");
    }
}

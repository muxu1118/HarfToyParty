using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkPanel : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    
    private bool activeFlag = false;
    
    private void Start()
    {
        Panel.SetActive(false);
    }

    private void Update()
    {
        //if (Input.GetKeyDown("joystick 1 button 2"))
        //{
        //    // activeFlagがtrueの時呼び出される
        //    if (activeFlag)
        //    {
        //        SetFalse();
        //    }
        //    // activeFlagがfalseの時呼び出される
        //    else
        //    {
        //        SetTrue();
        //    }
        //    // On / Off切り替え
        //    activeFlag = !activeFlag;
        //}
    }

    public void Menu()
    {
        if(Input.GetMouseButtonDown(0))
        //if (Input.GetKeyDown("joystick 1 button 2"))
        {
            // activeFlagがtrueの時呼び出される
            if (activeFlag)
            {
                SetFalse();
            }
            // activeFlagがfalseの時呼び出される
            else
            {
                SetTrue();
            }
            // On / Off切り替え
            activeFlag = !activeFlag;
        }
    }

    public void SetTrue()
    {
        Panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SetFalse()
    {
        Panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Skip()
    {
        SceneManager.LoadScene("Choice");
    }

    public void End()
    {
        SceneManager.LoadScene("Title");
    }
}

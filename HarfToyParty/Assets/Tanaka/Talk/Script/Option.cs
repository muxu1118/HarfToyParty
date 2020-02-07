using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// TalkScene 用の Script
public class Option : MonoBehaviour
{
    [SerializeField] GameObject option;
    [SerializeField] GameObject optionShadow;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject Skip;
    [SerializeField] GameObject Frame1;
    [SerializeField] GameObject Frame2;
    [SerializeField] GameObject Choice1;
    [SerializeField] GameObject Choice2;

    private bool activeFlag = true;

    private void Start()
    {
        FadeManager.FadeIn();
        if (SceneManager.GetActiveScene().name == "TalkScene")
        {
            option.SetActive(false);
            optionShadow.SetActive(false);
            Frame1.SetActive(false);
            Frame2.SetActive(false);
            Choice1.SetActive(false);
            Choice2.SetActive(false);
            Skip.SetActive(true);
        }
    }

    public void Update()
    {
        Move();
        FramemMove();
        SelectFrame();     

        if (SceneManager.GetActiveScene().name == "TalkScene")
        {
            // RB で activeFlagの入れ替え
            if (Input.GetKeyDown("joystick button 5"))    // RB
            {
                if (activeFlag)
                    SystemCall();
                else
                    MenuOut();

                activeFlag = !activeFlag;
            }
            else if (Input.GetKeyDown("joystick button 1"))  // A
                MenuOut();
        }
    }

    void Move()
    {
        if (option.activeSelf == true)
            Time.timeScale = 0f;
        else if (option.activeSelf == false)
            Time.timeScale = 1f;
    }

    public void SystemCall()
    {
        option.SetActive(true);
        optionShadow.SetActive(true);
        Frame1.SetActive(false);

        if (Frame2.activeSelf == true)
            Frame1.SetActive(false);
    }

    public void MenuOut()
    {
        option.SetActive(false);
        optionShadow.SetActive(false);
        Frame1.SetActive(false);
        Frame2.SetActive(false);
        Choice1.SetActive(false);
        Choice2.SetActive(false);
    }

    public void Frame01()
    {
        Frame1.SetActive(true);
        Frame2.SetActive(false);
    }

    public void Frame02()
    {
        Frame1.SetActive(false);
        Frame2.SetActive(true);
    }

    public void Choice01()
    {
        Choice1.SetActive(true);
        Choice2.SetActive(false);
    }

    public void Choice02()
    {
        Choice1.SetActive(false);
        Choice2.SetActive(true);
    }

    public void SelectFrame()
    {
        if (option.activeSelf == true && optionShadow.activeSelf == true)
            if (Input.GetKeyDown("joystick button 3"))  // Y
            {
                if (activeFlag)
                    Choice02();
                else
                    Choice01();

                activeFlag = !activeFlag;
            }
            else if (Frame1.activeSelf == true && Choice1.activeSelf == true)
            {
                if (Input.GetKeyDown("joystick button 2"))  // B
                {
                    FadeManager.FadeOut(0);
                    GameManager.instance.StateChange();
                }
            }
            else if (Frame2.activeSelf == true && Choice1.activeSelf == true)
                if (Input.GetKeyDown("joystick button 2"))
                    MenuOut();
    }

    public void FramemMove()
    {
        if (option.activeSelf == true && optionShadow.activeSelf == true)
            if (Input.GetKeyDown("joystick button 0"))
            {
                if (activeFlag)
                    Frame01();
                else
                    Frame02();

                activeFlag = !activeFlag;
            }
            else if (Frame1.activeSelf == true)
            {
                if (Input.GetKeyDown("joystick button 2"))  // B
                {
                    Time.timeScale = 1f;
                    FadeManager.FadeOut(2);
                    GameManager.instance.StateChange();
                }
            }
            else if (Frame2.activeSelf == true)
                if (Input.GetKeyDown("joystick button 2"))
                    MenuOut();
    }
}

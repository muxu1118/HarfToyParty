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
    private bool moveFlag = true;

    private void Start()
    {
        FadeManager.FadeIn();

        option.SetActive(false);
        optionShadow.SetActive(false);
        Frame1.SetActive(false);
        Frame2.SetActive(false);
        Choice1.SetActive(false);
        Choice2.SetActive(false);
        Skip.SetActive(true);
    }

    public void Update()
    {
        GameMove();
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

    void GameMove()
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
            if (Input.GetAxisRaw("Joysticks 1 Vertical") > 0.1 && activeFlag)    // 上
            {
                Choice02();
                activeFlag = false;
            }
            else if (Input.GetAxisRaw("Joysticks 2 Vertical") > 0.1 && activeFlag)    // 上
            {
                Choice02(); 
                activeFlag = false;
            }
            else if (Input.GetAxisRaw("Joysticks 1 Vertical") < -0.1 && !activeFlag)    // 下
            {
                Choice01();
                activeFlag = true;
            }
            else if (Input.GetAxisRaw("Joysticks 2 Vertical") < -0.1 && !activeFlag)    // 下
            {
                Choice01();
                activeFlag = true;
            }
            else if (Frame1.activeSelf == true && Choice1.activeSelf == true)
            {
                if (Input.GetKeyDown("joystick button 2"))  // B
                {
                    MenuOut();
                    Time.timeScale = 1f;
                    FadeManager.FadeOut(0);
                }
            }
            else if (Frame2.activeSelf == true && Choice1.activeSelf == true)
                if (Input.GetKeyDown("joystick button 2"))
                    MenuOut();
    }

    public void FramemMove()
    {
        if (option.activeSelf == true && optionShadow.activeSelf == true)
            if (Choice1.activeSelf == true || Choice2.activeSelf == true)
                if (Input.GetAxisRaw("Joysticks 1 Horizontal") > 0.1f && moveFlag)  // 右
                {
                    Frame02();
                    moveFlag = false;
                }
                else if (Input.GetAxisRaw("Joysticks 2 Horizontal") > 0.1 && moveFlag)    // 右
                {
                    Frame02();
                    moveFlag = false;
                }
                else if (Input.GetAxisRaw("Joysticks 1 Horizontal") < -0.1 && !moveFlag)    // 左
                {
                    Frame01();
                    moveFlag = true;
                }
                else if (Input.GetAxisRaw("Joysticks 2 Horizontal") < -0.1 && !moveFlag)    // 左
                {
                    Frame01();
                    moveFlag = true;
                }
                else if (Frame1.activeSelf == true && Choice2.activeSelf == true)
                {
                    if (Input.GetKeyDown("joystick button 2"))  // B
                    {
                        MenuOut();
                        Time.timeScale = 1f;
                        GameManager.instance.StateChange();
                        FadeManager.FadeOut(2);
                    }
                }
                else if (Frame2.activeSelf == true)
                    if (Input.GetKeyDown("joystick button 2"))
                        MenuOut();
    }
}

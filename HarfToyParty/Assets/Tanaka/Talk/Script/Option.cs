using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        option.SetActive(false);
        optionShadow.SetActive(false);
        Frame1.SetActive(false);
        Frame2.SetActive(false);
        Choice1.SetActive(false);
        Choice2.SetActive(false);

        if (SceneManager.GetActiveScene().name == "TalkScene")
            Skip.SetActive(true);
    }

    public void Update()
    {
        End();
        FramemMove();
        MoveFrame();

        // RB で activeFlagの入れ替え
        //if (Input.GetKeyDown("joystick button 5"))    // RB
        if (Input.GetMouseButtonDown(1))
        {
            if (activeFlag)
                Menumenu();
            else
                Numenume();

            activeFlag = !activeFlag;
        }

        else if (SceneManager.GetActiveScene().name == "MainGeme")
            Skip.SetActive(false);
    }

    public void Menumenu()
    {
        option.SetActive(true);
        optionShadow.SetActive(true);
        Frame1.SetActive(false);

        if (Frame2.activeSelf == true)
            Frame1.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Numenume()
    {
        option.SetActive(false);
        optionShadow.SetActive(false);
        Frame1.SetActive(false);
        Frame2.SetActive(false);
        Time.timeScale = 1f;
    }

    public void End()
    {
        if (SceneManager.GetActiveScene().name == "TalkScene")
        {
            if (Input.GetKeyDown("joystick button 2"))    // B
                if (option.activeSelf == true && optionShadow.activeSelf == true)
                {
                    Time.timeScale = 1f;
                    GameManager.instance.StateChange();
                    SceneController.instance.sceneSwitching("MainGame");
                }
        }

        else if (SceneManager.GetActiveScene().name == "MainGame")
        {
            if (Input.GetKeyDown("joystick button 2"))    // B
                if (option.activeSelf == true && optionShadow.activeSelf == true)
                {
                    Time.timeScale = 1f;
                    GameManager.instance.StateChange();
                    SceneController.instance.sceneSwitching("Title");
                }
        }
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

    public void MoveFrame()
    {
        if (option.activeSelf == true && optionShadow.activeSelf == true)
            if (Input.GetKeyDown("joystick Analog5"))
            {
                if (activeFlag)
                    Choice02();
                else
                    Choice01();

                activeFlag = !activeFlag;
            }
    }

    public void FramemMove()
    {
        if (option.activeSelf == true && optionShadow.activeSelf == true)
            if (Input.GetKeyDown("joystick Analog4"))
            {
                if (activeFlag)
                    Frame01();
                else
                    Frame02();

                activeFlag = !activeFlag;
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// MainGame 用の Script
public class MainGameOption : MonoBehaviour
{
    [SerializeField] GameObject Optionmenu;
    [SerializeField] GameObject Shadow;
    [SerializeField] GameObject Menuop;
    [SerializeField] GameObject Choose01;
    [SerializeField] GameObject Choose02;

    private bool flagActive = true;

    void Start()
    {
        FadeManager.FadeIn();

        Optionmenu.SetActive(false);
        Shadow.SetActive(false);
        Choose01.SetActive(false);
        Choose02.SetActive(false);
    }

    void Update()
    {
        Select();
        Finish();
        MoveGame();
        // RBでオプション操作
        if (Input.GetKeyDown("joystick button 5"))    // RB
        {
            if (flagActive)
                MenuCall();
            else
                MenuDelet();
            
            flagActive = !flagActive;
        }
        else if (Input.GetKeyDown("joystick button 1"))
            MenuDelet();
    }
    // オプションパネルが出た時背景が止まる、消えた時動き出す
    void MoveGame()
    {
        if (Optionmenu.activeSelf == true)
            Time.timeScale = 0f;
        else if (Optionmenu.activeSelf == false)
            Time.timeScale = 1f;
    }
    // オプションパネルを呼び出す
    public void MenuCall()
    {
        Optionmenu.SetActive(true);
        Shadow.SetActive(true);
        Choose01.SetActive(true);

        if (Choose02.activeSelf == true)
            Choose01.SetActive(false);
    }

    public void MenuDelet()
    {
        Optionmenu.SetActive(false);
        Shadow.SetActive(false);
        Choose01.SetActive(false);
        Choose02.SetActive(false);
    }

    public void SelectChoice()
    {
        Choose01.SetActive(true);
        Choose02.SetActive(false);
    }

    public void ChoiceSelect()
    {
        Choose01.SetActive(false);
        Choose02.SetActive(true);
    }

    public void Select()
    {
        if (Optionmenu.activeSelf == true && Shadow.activeSelf == true)
            if (Input.GetAxisRaw("Joysticks 1 Horizontal") > 0.1f && !flagActive)  // 右
            {
                ChoiceSelect();
                flagActive = true;
            }
            else if (Input.GetAxisRaw("Joysticks 2 Horizontal") > 0.1 && !flagActive)    // 右
            {
                ChoiceSelect();
                flagActive = true;
            }
            else if (Input.GetAxisRaw("Joysticks 1 Horizontal") < -0.1 && flagActive)    // 下
            {
                SelectChoice();
                flagActive = false;
            }
            else if (Input.GetAxisRaw("Joysticks 2 Horizontal") < -0.1 && flagActive)    // 下
            {
                SelectChoice();
                flagActive = false;
            }
            else if (Input.GetKeyDown("joystick button 2"))  // B
                if (Choose01.activeSelf == true)
                {
                    MenuDelet();
                    Time.timeScale = 1f;
                    GameManager.instance.StateChange();
                    FadeManager.FadeOut(0);
                }
                else if (Choose02.activeSelf == true)
                    MenuDelet();
    }

    public void Finish()
    {
        if (Input.GetKeyDown("joystick button 2"))    // B
            if (Optionmenu.activeSelf == true && Shadow.activeSelf == true)
            {
                MenuDelet();
                Time.timeScale = 1f;
                GameManager.instance.StateChange();
                FadeManager.FadeOut(0);
            }
    }
}

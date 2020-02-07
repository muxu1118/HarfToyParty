using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// MainGame 用の Script
public class MainGameOption : MonoBehaviour
{
    // SerializeField -------------------------
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

        if (Input.GetKeyDown("joystick button 5"))    // RB
        {
            if (flagActive)
                MenuCall();
            else
                MenuDelet();
            // Active / 非Active の入れ替え
            flagActive = !flagActive;
        }
        else if (Input.GetKeyDown("joystick button 1"))
            MenuDelet();

    }

    void MoveGame()
    {
        if (Optionmenu.activeSelf == true)
            Time.timeScale = 0f;
        else if (Optionmenu.activeSelf == false)
            Time.timeScale = 1f;
    }

    // オプション呼び出しのためのクラス
    public void MenuCall()
    {
        Optionmenu.SetActive(true);
        Shadow.SetActive(true);
        Choose01.SetActive(true);

        if (Choose02.activeSelf == true)
            Choose01.SetActive(false);
    }
    // オプションを 非Active 状態にするためのクラス
    public void MenuDelet()
    {
        Optionmenu.SetActive(false);
        Shadow.SetActive(false);
        Choose01.SetActive(false);
        Choose02.SetActive(false);
    }

    public void SelectChice()
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
            if (Input.GetKeyDown("joystick button 0"))
            {
                if (flagActive)
                    SelectChice();
                else
                    ChoiceSelect();

                flagActive = !flagActive;
            }
            else if (Input.GetKeyDown("joystick button 2"))  // B
                if (Choose01.activeSelf == true)
                {
                    Time.timeScale = 1f;
                    GameManager.instance.StateChange();
                    SceneController.instance.sceneSwitching("Title");
                }
                else if (Choose02.activeSelf == true)
                    MenuDelet();
    }

    public void Finish()
    {
        if (Input.GetKeyDown("joystick button 2"))    // B
            if (Optionmenu.activeSelf == true && Shadow.activeSelf == true)
            {
                Time.timeScale = 1f;
                GameManager.instance.StateChange();
                SceneController.instance.sceneSwitching("Title");
            }
    }
}

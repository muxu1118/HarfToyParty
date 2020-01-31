using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject option;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject Skip;

    private bool activeFlag = true;

    private void Start()
    {
        option.SetActive(false);
        if (SceneManager.GetActiveScene().name == "TalkScene")
            Skip.SetActive(true);
    }

    public void Update()
    {
        End();
       
        // RB で activeFlagの入れ替え
        if (Input.GetKeyDown("joystick button 5"))    // RB
        {
            if (activeFlag)
                Menumenu();
            else
                Numenume();

            activeFlag = !activeFlag;
        }

        else if(SceneManager.GetActiveScene().name == "MainGeme")
        {
            Skip.SetActive(false);
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

    public void End()
    {
        if (Input.GetKeyDown("joystick button 3"))    // Y
            if (option.activeSelf == true)
            {
                GameManager.instance.StateChange();
                //SceneController.instance.sceneSwitching("Choice");
                SceneController.instance.sceneSwitching("MainGame");
            }
    }
}

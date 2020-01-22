using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject option;
    [SerializeField] GameObject menu;

    private void Start()
    {
        //menu.SetActive(true);
        option.SetActive(false);
    }

    public void Menumenu()
    {
        //if (Input.GetKeyDown("joystick 1 button 2"))
        option.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("押されてるよ");
    }

    public void Numenume()
    {
        //if (Input.GetKeyDown("joystick 1 button 2"))
        option.SetActive(false);
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

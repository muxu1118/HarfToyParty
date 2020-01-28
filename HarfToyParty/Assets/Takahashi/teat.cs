using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class teat : MonoBehaviour
{
    [SerializeField]
    //Image[] image;
    //Sprite[] image;
    SpriteRenderer[] spriteRenderers;
    int i = 0;

    [SerializeField]
    GameObject background;

    [SerializeField]
    float x, y;

    private void Start()
    {
        //background.transform.position = new Vector3(970, 520, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PartGet();
            background.transform.position = new Vector3(x, y, 0);
        }
    }

    public void PartGet()
    {
        Debug.Log("hazime");
        i++;
        Debug.Log(i);
        switch (i)
        {
            case 1:
                spriteRenderers[0].sprite = Resources.Load<Sprite>("Sprites/old/Part/gimmick_body3_B");
                //Debug.LogError("来た");
                break;
            case 2:
                spriteRenderers[1].sprite = Resources.Load<Sprite>("Sprites/old/Part/gimmick_body01_B");
                break;
            case 3:
                spriteRenderers[2].sprite = Resources.Load<Sprite>("Sprites/old/Part/gimmick_body2_B");
                break;
        }
    }

    private void sceneChange()
    {
        if (Input.GetKeyDown("joystick 1 button 3"))
        {
            if (SceneManager.GetActiveScene().name == "Title")
            {
                //GameState = State.PlayerChoise;
                SceneController.instance.sceneSwitching("TalkScene");
            }
            else if (SceneManager.GetActiveScene().name == "TalkScene")
            {
                //GameState = State.Main;
                SceneController.instance.sceneSwitching("MainGame");
            }
        }
    }
}

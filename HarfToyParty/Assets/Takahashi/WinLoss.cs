using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoss : MonoBehaviour
{         
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Image image;

    GameManager manager;

    Sprite sprites;
    
    void Start()
    {
        //画像の取得
            }

    public void WinOrLoss(int winner)
    {
        int winnerDesplay = winner;
        switch (winnerDesplay)
        {
            case 1:
                //赤の勝利
                panel.SetActive(true);
                sprites = Resources.Load<Sprite>("Sprites/");
                image.sprite = sprites;
                break;
            case 2:
                //青の勝利
                panel.SetActive(true);
                sprites = Resources.Load<Sprite>("Sprites/");
                image.sprite = sprites;
                break;
        }        
    }

    private void title()
    {
        Resources.Load("Title");
        
    }
}

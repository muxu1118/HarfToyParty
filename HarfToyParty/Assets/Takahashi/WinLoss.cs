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

    Sprite[] sprites;
    Sprite redWin;
    Sprite blueWin;

    void Start()
    {
        //画像の取得
        sprites = Resources.LoadAll<Sprite>("Sprites/");                                
    }

    public void WinOrLoss(int winner)
    {
        int winnerDesplay = winner;
        switch (winnerDesplay)
        {
            case 1:
                //赤の勝利
                image.sprite = redWin;
                redWin = sprites[1];
                break;
            case 2:
                //青の勝利
                image.sprite = blueWin;
                blueWin = sprites[2];
                break;
        }        
    }

    private void title()
    {
        Resources.Load("Title");
        
    }
}

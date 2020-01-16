using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoss : MonoBehaviour
{         
    [SerializeField]
    GameObject panel; 
    [SerializeField]
    Image red;
    [SerializeField]
    Image blue;

    Sprite Win;
    Sprite Lose;

    [SerializeField]
    GameObject[] crownTear;

    void Start()
    {
        //1P勝利の画像を取得
        Win = Resources.Load<Sprite>("Sprites/WinLose/w");
        //2P勝利の画像を取得
        Lose = Resources.Load<Sprite>("Sprites/WinLose/l");
    }

    /// <summary>
    /// 試合が終了したときにどちらが勝ったかを表示させる
    /// </summary>
    /// <param name="winner">勝ったプレイヤー</param>
    public void WinOrLoss(int winner)
    {
        //リザルトUIを表示
        panel.SetActive(true);
        int winnerDesplay = winner;
        switch (winnerDesplay)
        {
            case 1:
                //赤の勝利                
                red.sprite = Win;
                blue.sprite = Lose;
                crownTear[0].SetActive(true);
                crownTear[1].SetActive(false);
                break;
            case 2:
                //青の勝利                
                red.sprite = Lose;
                blue.sprite = Win;
                crownTear[0].SetActive(false);
                crownTear[1].SetActive(true);
                break;
        }        
    }
}

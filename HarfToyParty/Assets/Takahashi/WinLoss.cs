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

    void Start()
    {
        //1P勝利の画像を取得
        Win = Resources.Load<Sprite>("Sprites/w");
        //2P勝利の画像を取得
        Lose = Resources.Load<Sprite>("Sprites/l");
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
                break;
            case 2:
                //青の勝利                
                red.sprite = Lose;
                blue.sprite = Win;
                break;
        }        
    }
}

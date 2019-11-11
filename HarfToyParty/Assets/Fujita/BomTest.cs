using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomTest : MonoBehaviour
{
    private GameObject Bomb;
    public Text BomText; //画面タイマー表示用テキスト
    [SerializeField]
    private float timeExplosion;//爆発までの時間
    private float timeEpTrigger = 0;

    public Vector2 MyPosi { get; set; } = new Vector2();
    void Update()
    {
        WaitTime();
    }

    private void WaitTime()
    {
        // 爆発までの時間
        if(timeExplosion >= timeEpTrigger)
        {
            timeExplosion -= Time.deltaTime;
            if(timeExplosion <= timeEpTrigger)
            {
                Destroy(gameObject);
            }
        BomText.text = "爆発まで" + timeExplosion.ToString("f0") + "秒";
        }else
        {
            //爆発したいよ
            Explosion();
        }
    }

    /// <summary>
    /// 爆発させる関数
    /// </summary>
    private void Explosion()
    {
        int x = (int)MyPosi.x, y = (int)MyPosi.y;
        for(int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (y - j > 6 && y - j < 0 && x + i > 6 && x + i < 0) 
                {
                    continue;
                }
                if (Map.instance.mapInt[y - j, x + i] == (int)MapKind.Player1 || Map.instance.mapInt[y - j, x + i] == (int)MapKind.Player2) 
                {
                    // プレイヤーにダメージを与えたい

                }
            }
        }
    }
}

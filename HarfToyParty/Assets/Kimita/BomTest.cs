﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomTest : MonoBehaviour
{
    [SerializeField]
    private GameObject BombRange;

    public Text BomText; //画面タイマー表示用テキスト
    [SerializeField]
    private float timeExplosion;//爆発までの時間
    private float timeEpTrigger = 0;
    bool isSE;

    public Vector2 MyPosi { get; set; } = new Vector2();
    private void Start()
    {
        BombRange.SetActive(false);
        isSE = false;
    }
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
           
        //BomText.text = "爆発まで" + timeExplosion.ToString("f0") + "秒";
        }else if (!isSE)
        {
            isSE = true;
            SEController.instance.PlaySE(SEController.SEType.Bomb,0.8f, false);
            StartCoroutine(DestroyObject());
        }
        else
        {
            //爆発したいよ
            transform.GetChild(0).gameObject.SetActive(false);
            BombRange.SetActive(true);
        }
    }

    /// <summary>
    /// 爆発させる関数
    /// </summary>
    private void Explosion()
    {
        int x = (int)MyPosi.x, y = (int)MyPosi.y;
        // 自分周辺の探索
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                // マップ外だったらもう一回
                if (y - j > 6 || y - j < 0 || x + i > 6 || x + i < 0) 
                {
                    continue;
                }
                // 探索した範囲にプレイヤーがいたら
                if (Map.instance.mapInt[y - j, x + i] == (int)MapKind.Player1 || Map.instance.mapInt[y - j, x + i] == (int)MapKind.Player2) 
                {
                    // プレイヤーにダメージを与えたい
                    Map.instance.PlayerBomDown((MapKind)Map.instance.mapInt[y - j, x + i]);
                }
                // 探索した範囲に壊れる壁があったら
                if (Map.instance.mapInt[y - j, x + i] >= (int)MapKind.BreakWall1 && Map.instance.mapInt[y - j, x + i] <= (int)MapKind.BreakWall10)
                {
                    // GameObjectから壊れる壁を取得
                    BreakWall[] breakObjects = FindObjectsOfType<BreakWall>();
                    foreach (BreakWall BW in breakObjects)
                    {
                        // 探索した壁と同じ壁を破壊
                        if((int)BW.MyWallP == Map.instance.mapInt[y - j, x + i])
                        {
                            Destroy(BW.transform.gameObject);
                            Map.instance.mapInt[y - j, x + i] = (int)MapKind.YUKA;
                        }
                    }
                }
            }
        }
    }

    IEnumerator DestroyObject()
    {

        float time = 0.2f;
        while(time > 0)
        {
            Explosion();
            time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        time = 0.3f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }
}

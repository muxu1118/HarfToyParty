﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoss : MonoBehaviour
{         
    //リザルトUI
    [SerializeField]
    GameObject panel;
    //リザルト画面の背景
    [SerializeField]
    GameObject background;
    //Win,Loseを表示させるための場所に配置したオブジェクト
    [SerializeField]
    Image red,blue;
    //Win,Loseのスプライト画像を取得するためのもの
    Sprite Win,Lose; 

    //[SerializeField]
    //float b,x,y;    
        
    GameObject crownPrefab;
    GameObject tearPrefab;

    GameObject crown;
    GameObject tear;

    //王冠と涙の位置を勝者に合わせるためのもの 
    bool slore = true;

    int i = 1;
    bool b = true;

    void Start()
    {
        //panel.SetActive(false);
        //1P勝利の画像を取得
        Win = Resources.Load<Sprite>("Sprites/WinLose/w");
        //2P勝利の画像を取得
        Lose = Resources.Load<Sprite>("Sprites/WinLose/l");
        //王冠のプレハブを取得
        crownPrefab = (GameObject)Resources.Load("Prefabs/ResultUI/Crown");
        //涙のプレハブを取得
        tearPrefab = (GameObject)Resources.Load("Prefabs/ResultUI/Tear");
        background.transform.position = new Vector3(970, 520, 0);
        //GameManager.instance.winLoseLood();
    }
    
    private void Update()
    {        
        //位置確認用
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Quaternion rote = new Quaternion(0.0f, 0.0f, 0.2f, 1.0f);

            //GameObject crown = Instantiate(crownPrefab, new Vector3(x, y, 0.0f), rote);
            //crown.transform.parent = panel.transform;
            //crown.transform.SetParent(panel.transform, false);
            //Debug.Log("wwww");
            if (!b)
            {
                Destroy(crown);
                Destroy(tear);
                panel.SetActive(false);
                b = !b;
            }
            else
            {
                if(i == 1)
                {
                    WinOrLoss(1);
                    
                    i = 2;
                }
                else if(i == 2)
                {
                    WinOrLoss(2);
                   
                    i = 1;
                }
                b = !b;
            }
        }
        titleSceneLoad();
    }    

    /// <summary>
    /// 王冠を生成する
    /// </summary>
    private void crownGenerate()
    {
        //王冠の角度b = 0.2が弟　-0.2が兄
        float crownAngle = 0.2f;
        //王冠の位置
        //float crown_x = -232, crown_y = 206;
        float crown_x = -242, crown_y = 166;

        if (!slore)
        {
            //青が勝利した場合角度と場所を反転
            crownAngle *= -1f; 
            crown_x = 400;
        }

        //王冠の角度を設定
        Quaternion rote = new Quaternion(0.0f, 0.0f, crownAngle, 1.0f);
        //王冠を指定した位置に生成
        crown = Instantiate(crownPrefab, new Vector3(crown_x,crown_y, 0.0f), rote);        
        //指定した親に子として生成
        crown.transform.SetParent(panel.transform, false);
        //Debug.Log("kita");
    }

    /// <summary>
    /// 涙を生成する
    /// </summary>
    private void tearGenerate()
    {
        //涙の位置
        //float tear_x = -208, tear_y = 144;
        float tear_x = -218, tear_y = 124;

        if (slore)
        {
            //赤が勝利した場合場所を反転
            tear_x = 388;
        }
        tear = Instantiate(tearPrefab, new Vector3(tear_x, tear_y, 0.0f), Quaternion.identity);        
        tear.transform.SetParent(panel.transform, false);
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
                slore = true;
                //赤が勝利の場合
                blue.sprite = Lose;
                red.sprite = Win;

                crownGenerate();
                tearGenerate(); 
                
                break;
            case 2:
                //青の勝利  
                slore = false;
                red.sprite = Lose;
                blue.sprite = Win;

                crownGenerate();
                tearGenerate();

                break;
        } 
    }

    private void titleSceneLoad()
    {
        if (Input.GetKeyDown("joystick 1 button 4"))
        {
            GameManager.instance.StateChange();
            SceneController.instance.sceneSwitching("Title");
        }
    }
}

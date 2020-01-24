using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoss : MonoBehaviour
{         
    //リザルトUI
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Image red;
    [SerializeField]
    Image blue;
   
    Sprite Win; //Winのスプライト画像を取得するためのもの
    Sprite Lose; //Loseのスプライト画像を取得するためのもの
    
    [SerializeField]
    GameObject[] Red_resultUI;
    [SerializeField]
    GameObject[] Blue_resultUI;

    [SerializeField]
    float b;
    //王冠の場合b = 0.2が弟　-0.2が兄

    GameObject crownPrefab;
    GameObject tearPrefab;

    bool slore = true;       

    private void Update()
    {
        //位置確認用
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Quaternion rote = new Quaternion(0.0f, 0.0f, b, 1.0f);
            
            GameObject crown = Instantiate(Red_resultUI[0], new Vector3(1.0f, 2.0f, 0.0f), rote);
            crown.transform.parent = panel.transform;            
        }
    }

    void Start()
    {        
        //1P勝利の画像を取得
        Win = Resources.Load<Sprite>("Sprites/WinLose/w");
        //2P勝利の画像を取得
        Lose = Resources.Load<Sprite>("Sprites/WinLose/l");
        crownPrefab = (GameObject)Resources.Load("Prefabs/crown");
        tearPrefab = (GameObject)Resources.Load("Prefabs/tear");
    }

    /// <summary>
    /// 王冠を生成する
    /// </summary>
    private void crownGenerate()
    {
        //赤が勝利の場合
        if (slore)
        {             
            b *= -0.1f;
        }        
        Quaternion rote = new Quaternion(0.0f, 0.0f, b, 1.0f);
        //
        GameObject crowCroaw = Instantiate(crownPrefab, new Vector3(1.0f, 2.0f, 0.0f), rote);
        crowCroaw.transform.parent = panel.transform;
    }

    /// <summary>
    /// 涙を生成する
    /// </summary>
    private void tearGenerate()
    {
        //赤が敗北の場合        
        GameObject crowTear = Instantiate(tearPrefab, new Vector3(1.0f, 2.0f, 0.0f), Quaternion.identity);
        crowTear.transform.parent = panel.transform;
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
                
                blue.sprite = Lose;

                Blue_resultUI[0].SetActive(false);
                Blue_resultUI[1].SetActive(false);

                Red_resultUI[2].SetActive(false);
                break;
            case 2:
                //青の勝利  
                slore = false;
                red.sprite = Lose;

                Red_resultUI[0].SetActive(false);
                Red_resultUI[1].SetActive(false);

                Blue_resultUI[2].SetActive(false);
                break;
        }        
    }
}

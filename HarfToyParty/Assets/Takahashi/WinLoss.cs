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
    GameObject background;
    [SerializeField]
    Image red;
    [SerializeField]
    Image blue;

    Sprite Win; //Winのスプライト画像を取得するためのもの
    Sprite Lose; //Loseのスプライト画像を取得するためのもの
    
    //[SerializeField]
    //GameObject[] Red_resultUI;
    //[SerializeField]
    //GameObject[] Blue_resultUI;

    [SerializeField]
    float b,x,y;
    //王冠の場合b = 0.2が弟　-0.2が兄
    float crownAngle = 0.2f;
    //王冠の位置
    float crown_x, crown_y;
    //涙の位置
    float tear_x, tear_y;

    GameObject crownPrefab;
    GameObject tearPrefab;

    //王冠と涙の位置を勝者に合わせるためのもの
    bool slore = true;

    void Start()
    {
        //1P勝利の画像を取得
        Win = Resources.Load<Sprite>("Sprites/WinLose/w");
        //2P勝利の画像を取得
        Lose = Resources.Load<Sprite>("Sprites/WinLose/l");
        //王冠のプレハブを取得
        crownPrefab = (GameObject)Resources.Load("Prefabs/ResultUI/Crown");
        //涙のプレハブを取得
        tearPrefab = (GameObject)Resources.Load("Prefabs/ResultUI/Tear");
        
    }

    private void Update()
    {        
        //位置確認用
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Quaternion rote = new Quaternion(0.0f, 0.0f, b, 1.0f);

            GameObject crown = Instantiate(crownPrefab, new Vector3(x, y, 0.0f), rote);
            //crown.transform.parent = panel.transform;
            crown.transform.SetParent(panel.transform, false);
            Debug.Log("wwww");
        }        
    }    

    /// <summary>
    /// 王冠を生成する
    /// </summary>
    private void crownGenerate()
    {        
        if (slore)
        {
            //赤が勝利した場合角度と場所を反転
            crownAngle *= -1f; 
            crown_x *= -1f;
            crown_y *= -1f;
        }
        //王冠の角度を設定
        Quaternion rote = new Quaternion(0.0f, 0.0f, crownAngle, 1.0f);
        //王冠を指定した位置に生成
        GameObject crown = Instantiate(crownPrefab, new Vector3(crown_x,crown_y, 0.0f), rote);        
        //指定した親に子として生成
        crown.transform.SetParent(panel.transform, false);        
    }

    /// <summary>
    /// 涙を生成する
    /// </summary>
    private void tearGenerate()
    {
        if (slore)
        {
            //赤が勝利した場合場所を反転
            tear_x *= -1f;
            tear_y *= -1f;
        }
        GameObject crowTear = Instantiate(tearPrefab, new Vector3(tear_x, tear_y, 0.0f), Quaternion.identity);
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
        background.transform.position = new Vector3(85, 96, 0);
        int winnerDesplay = winner;
        switch (winnerDesplay)
        {
            case 1:
                //赤が勝利の場合

                crownGenerate();
                tearGenerate();

                blue.sprite = Lose;

                //Blue_resultUI[0].SetActive(false);
                //Blue_resultUI[1].SetActive(false);

                //Red_resultUI[2].SetActive(false);
                break;
            case 2:
                //青の勝利  
                slore = false;
                red.sprite = Lose;

                crownGenerate();
                tearGenerate();

                //Red_resultUI[0].SetActive(false);
                //Red_resultUI[1].SetActive(false);

                //Blue_resultUI[2].SetActive(false);
                break;
        }        
    }
}

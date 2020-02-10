using System.Collections;
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
    Image red,blue,drow;
    //Win,Loseのスプライト画像を取得するためのもの
    Sprite Win,Lose,Drow; 

    //[SerializeField]
    //float b,x,y;    
        
    GameObject crownPrefab;
    GameObject tearPrefab;

    GameObject crown;
    GameObject tear;
    GameObject draw;

    //王冠の角度
    float crownAngle = 0.2f;
    //王冠の位置
    [SerializeField]
    float crown_x = -242, crown_y = 166;
    //涙の位置
    [SerializeField]
    float tear_x = -218, tear_y = 124;

    //王冠と涙の位置を勝者に合わせるためのもの 
    bool slore = true;

    //Vector2 result_UI;
    int i = 1;
    bool b = true;

    bool stage = false;
    int stageCount = 1;

    void Start()
    {
        //panel.SetActive(false);
        //1P勝利の画像を取得
        Win = Resources.Load<Sprite>("Sprites/WinLose/w");
        //2P勝利の画像を取得
        Lose = Resources.Load<Sprite>("Sprites/WinLose/l");
        Drow = Resources.Load<Sprite>("Sprites/WinLose/d");
        //王冠のプレハブを取得
        crownPrefab = (GameObject)Resources.Load("Prefabs/ResultUI/Crown");
        //涙のプレハブを取得
        tearPrefab = (GameObject)Resources.Load("Prefabs/ResultUI/Tear");
        background.transform.position = new Vector3(970, 520, 0);
        //GameManager.instance.winLoseLood();
    }
    
    private void Update()
    {
        //ステージの切り替え
        if (stage)
        {
            if(Input.GetKeyDown("joystick button 5"))
            {
                Stage.instance.StageSelect(stageCount);
                Stage.instance.StageReset();
                panel.SetActive(false);
                stageCount++;
                stage = false;                
            }
        }

        //位置確認用
        if (Input.GetKeyDown(KeyCode.Space))
        {
        //    
        //    //if (!b)
        //    //{
        //    //    Destroy(crown);
        //    //    Destroy(tear);
        //    //    panel.SetActive(false);
        //    //    b = !b;
        //    //}
        //    //else
        //    {
        //        if(i == 1)
        //        {
        //            WinOrLoss(1);
                    
        //            i = 2;
        //        }
        //        else if(i == 2)
        //        {
        //            WinOrLoss(2);
                   
        //            i = 1;
        //        }
        //        b = !b;
        //    }
        }
    }    

    /// <summary>
    /// 引き分けを表示
    /// </summary>
    private void resultGenerate()
    {

        //draw = Instantiate(red.gameObject, new Vector3(86, 99, 0.0f), Quaternion.identity);
        //draw.transform.localScale = new Vector2(1.5f, 1.5f);
        //draw.transform.SetParent(panel.transform, false);
        drow.gameObject.SetActive(true);
        drow.sprite = Drow;
        red.gameObject.SetActive(false);
        blue.gameObject.SetActive(false);
    }

    /// <summary>
    /// 王冠を生成する
    /// </summary>
    private void crownGenerate()
    {
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
    }

    /// <summary>
    /// 涙を生成する
    /// </summary>
    private void tearGenerate()
    {
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
    public void GameEnd(int winner)
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

                //GameManager.instance.StateChange();                
                break;
            case 2:
                //青の勝利  
                slore = false;
                red.sprite = Lose;
                blue.sprite = Win;

                crownGenerate();
                tearGenerate();

                //GameManager.instance.StateChange();
                break;            
        }        
    }

    /// <summary>
    /// どちらかがパーツを取得した際に呼ばれる
    /// </summary>
    /// <param name="winner"></param>
    public void WinOrLose(int winner)
    {
        panel.SetActive(true);

        //ステージを切り替えるためのスイッチ
        stage = true;
        Debug.LogWarning(stage);

        int winnerDesplay = winner;
        switch (winnerDesplay)
        {
            case 1:
                //赤が勝利の場合
                red.sprite = Win;
                blue.sprite = Lose;                             

                break;
            case 2:
                //青の勝利  
                red.sprite = Lose;
                blue.sprite = Win;

                break;
            case 3:
                resultGenerate();
                break;
        }
        
    }

    private void titleSceneLoad()
    {
        if (Input.GetKeyDown("joystick button 5"))
        {
            DestroyUI();
            SceneController.instance.sceneSwitching("Title");
        }
    }

    /// <summary>
    /// ゲームが終わるときに呼ばれる
    /// </summary>
    private void DestroyUI()
    {
        stageCount = 1;
        Destroy(tear);
        Destroy(crown);
    }
}

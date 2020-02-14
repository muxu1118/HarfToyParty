using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoss : MonoBehaviour
{
    Timer timer;
    //リザルトUI
    [SerializeField]
    GameObject panel;
    //リザルト画面の背景
    //[SerializeField]
    //GameObject background;
    //Win,Loseを表示させるための場所に配置したオブジェクト
    [SerializeField]
    Image red,blue,drow;
    //Win,Loseのスプライト画像を取得するためのもの
    Sprite Win,Lose; 

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
    float crown_x = -215, crown_y = 222;
    //涙の位置
    [SerializeField]
    float tear_x = -208, tear_y = 148;

    //王冠と涙の位置を勝者に合わせるためのもの 
    bool slore = true;

    //Vector2 result_UI;
    int i = 1;
    bool b = true;

    bool stageTrigger = false;　//ステージを切り替えるトリガー
    public bool drowtTrigger = false;   //引き分けかどうかを判断するトリガー
    int stageCount = 0;         

    bool gameEndTrrger = false;
    [SerializeField]
    characterPrehub characterPrehub;
    PartThrow _partThrow;

    Animator red_animator;
    Animator blue_animator;

    [SerializeField]
    GameObject[] chara;

    void Start()
    {
        red_animator = chara[0].GetComponent<Animator>();
        blue_animator = chara[1].GetComponent<Animator>();

        //panel.SetActive(false);
        //1P勝利の画像を取得
        Win = Resources.Load<Sprite>("Sprites/WinLose/w");
        //2P勝利の画像を取得
        Lose = Resources.Load<Sprite>("Sprites/WinLose/l");
        //Drow = Resources.Load<Sprite>("Sprites/WinLose/d");
        //王冠のプレハブを取得
        crownPrefab = (GameObject)Resources.Load("Prefabs/ResultUI/Crown");
        //涙のプレハブを取得
        tearPrefab = (GameObject)Resources.Load("Prefabs/ResultUI/Tear");
        //background.transform.position = new Vector3(970, 520, 0);
        //GameManager.instance.winLoseLood();

        _partThrow = GameObject.Find("throwObject").GetComponent<PartThrow>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        Stage.instance.StageSelect(stageCount);
        
    }
    
    private void Update()
    {
        
        //ステージの切り替え
        if (stageTrigger)
        {
            if(Input.GetKeyDown("joystick button 5"))
            {
                stageCount++;
                //ステージを破棄
                Stage.instance.StageReset();
                //ステージを変更
                Stage.instance.StageSelect(stageCount);
                //リザルトUIを隠す
                panel.SetActive(false);
                //次のステージを選べるようにする
                //ボタンを押しても反応しないようにする
                stageTrigger = false;
                red.gameObject.SetActive(true);
                blue.gameObject.SetActive(true);
                drow.gameObject.SetActive(false);
                //タイマーのカウントをリセット
                timer.TimeReset();
                red_animator.SetBool("WinTriggle", false);
                red_animator.SetBool("LoseTriggle", false);
                blue_animator.SetBool("LoseTriggle", false);                
                blue_animator.SetBool("WinTriggle", false);

                Debug.Log("ステージが切り替わった");
            }             
        }

        //パーツを規定数取得し終わったらタイトルに戻るもの
        if (gameEndTrrger)
        {
            if (Input.GetKeyDown("joystick button 5"))
            {
                DestroyUI();
                SceneController.instance.sceneSwitching("Title");
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
    /// 王冠を生成する
    /// </summary>
    private void crownGenerate()
    {
        if (!slore)
        {
            //青が勝利した場合角度と場所を反転
            crownAngle *= -1f; 
            crown_x = 390;
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
            tear_x = 377;
        }
        tear = Instantiate(tearPrefab, new Vector3(tear_x, tear_y, 0.0f), Quaternion.identity);        
        tear.transform.SetParent(panel.transform, false);
    }    

    /// <summary>
    /// どちらかがパーツを取得した際に呼ばれる
    /// </summary>
    /// <param name="winner"></param>
    public void WinOrLose(int winner)
    {
        panel.SetActive(true);

        //ステージを切り替えるためのスイッチ
        stageTrigger = true;
        //Debug.LogWarning(stageTrigger);

        int winnerDesplay = winner;
        switch (winnerDesplay)
        {
            case 1:
                //赤が勝利

                red_animator.SetBool("WinTriggle", true);
                blue_animator.SetBool("LoseTriggle", true);

                red.sprite = Win;
                blue.sprite = Lose;
                //drow.gameObject.SetActive(false);
                Debug.Log("赤の勝利");
                //characterPrehub.GetComponent<characterPrehub>().RedWinChange();
                break;
            case 2:
                //青の勝利  
                blue_animator.SetBool("WinTriggle", true);
                red_animator.SetBool("LoseTriggle", true);

                red.sprite = Lose;
                blue.sprite = Win;
                //drow.gameObject.SetActive(false);
                Debug.Log("青の勝利");
                //characterPrehub.GetComponent<characterPrehub>().BlueWinChange();
                break;
            case 3:
                red_animator.SetBool("WinTriggle", true);
                blue_animator.SetBool("WinTriggle", true);

                resultGenerate();

                //partDesplay.GetComponent<PartDesplay>().PartGet()
                //drowtTrigger = true;
                break;
        }
        
    }

    /// <summary>
    /// 試合が終了したときにどちらが勝ったかを表示させる
    /// </summary>
    /// <param name="winner">勝ったプレイヤー</param>
    public void GameEnd(int winner)
    {
        
        //リザルトUIを表示
        //panel.SetActive(true);
        gameEndTrrger = true;
        Debug.LogWarning("ゲームが終了したよ");

        int winnerDesplay = winner;
        switch (winnerDesplay)
        {
            case 1:
                slore = true;
                //赤が勝利の場合
                //blue.sprite = Lose;
                //red.sprite = Win;

                //赤のキャラをすべて色付きにして勝ちアニメーションを再生
                characterPrehub.GetComponent<characterPrehub>().RedWinChange();

                //王冠を作成
                crownGenerate();
                //涙を作成
                tearGenerate();                 
                break;
            case 2:
                //青の勝利  
                slore = false;
                //red.sprite = Lose;
                //blue.sprite = Win;

                //赤のキャラをすべて色付きにして勝ちアニメーションを再生
                characterPrehub.GetComponent<characterPrehub>().BlueWinChange();

                //王冠を作成
                crownGenerate();
                //涙を作成
                tearGenerate();
                break;
        }        
    }

    /// <summary>
    /// 引き分けを表示
    /// </summary>
    private void resultGenerate()
    {
        drow.gameObject.SetActive(true);
        red.gameObject.SetActive(false);
        blue.gameObject.SetActive(false);
    }

    IEnumerator drowDesplay()
    {
        _partThrow.drowCall();
        yield return new WaitForSeconds(1f);
        resultGenerate();
    }

    /// <summary>
    /// ゲームが終わるときに呼ばれる
    /// </summary>
    private void DestroyUI()
    {
        stageCount = 0;
        Destroy(tear);
        Destroy(crown);
    }
}

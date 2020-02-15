using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartDesplay : MonoBehaviour
{
    PartThrow _partThrow;
    WinLoss _winLoss;

    //プレイヤーの変更する画像を選択
    [SerializeField]
    SpriteRenderer[] character_changePart;

    SpriteRenderer[] StoragePart;

    [SerializeField]
    SpriteRenderer[] henkougo;

    [SerializeField]
    SpriteRenderer[] loseFace; 

    [SerializeField]
    GameObject[] character;

    [SerializeField]
    GameObject effect;
    [SerializeField]
    GameObject Blackout;

    [SerializeField]
    Image throwPart, drow_throwPart;

    //中央のパーツの初期位置を保存
    Vector2 centerPartVector;

    int winnerNum;

    string getPart;
    string DesplayPart;
    string Desplay = "Desplay";
    int partNum;
    int redPartCount = 0;
    int bluePartCount = 0;

    float rotaTime = 0;
    void Start()
    {
        //character_changePart[0].sprite = henkougo[0].sprite;
        _partThrow = GameObject.Find("throwObject").GetComponent<PartThrow>();
        _winLoss = GameObject.Find("ResultUI").GetComponent<WinLoss>();
        //PartStorage();
    }
   
    /// <summary>
    /// パーツを取得したときに呼び出される
    /// </summary>
    public void PartGet(string part, int winner)
    {
        getPart = part;
        winnerNum = winner;
        switch (getPart)
        {
            case "R_Face":
                partNum = 0;
                StartCoroutine("switching");
                break;
            case "R_Hand":
                partNum = 1;
                StartCoroutine("switching");
                break;
            case "R_Leg":
                partNum = 2;
                StartCoroutine("switching");
                break;
            case "B_Face":
                partNum = 3;
                StartCoroutine("switching");
                break;
            case "B_Hand":
                partNum = 4;
                StartCoroutine("switching");
                break;
            case "B_Leg":
                partNum = 5;
                StartCoroutine("switching");
                break;
            default:
                //既定のパーツが取得されなかった場合
                Debug.LogError(getPart);
                break;
        }
    }    

    //パーツ取得後の動き
    IEnumerator switching()
    {
        //中央に取ったパーツのデータを出力
        throwPart.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/" + getPart);
        //パーツを表示
        throwPart.gameObject.SetActive(true);
        //透明な背景を入れる
        Blackout.SetActive(true);
        //エフェクトを再生
        effect.SetActive(true);
        Debug.Log("中央に表示");

        if (_winLoss.drowtTrigger == true)
        {
            drow_throwPart.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1f);
        
        PartSearch();

        yield return new WaitForSeconds(1.5f);

        //エフェクトを止める        
        effect.SetActive(false);                
       
        //キャラクターのパーツを変更
        character_changePart[partNum].sprite = henkougo[partNum].sprite;
        
        yield return new WaitForSeconds(1.5f);

        //透明の背景を消す
        Blackout.SetActive(false);

        //勝者が兄なら
        if (partNum <= 2)
        {
            Debug.Log(partNum + "で兄の勝利");
            redPartCount++;
            _winLoss.WinOrLose(1);            
        }
        //勝者が弟なら
        else if(partNum >= 3)
        {
            Debug.Log(partNum + "で弟の勝利");
            bluePartCount++;
            _winLoss.WinOrLose(2);
        }

        //yield return new WaitForSeconds(0.5f);

        //ゲーム終了時に呼ばれる
        if (redPartCount == 2)
        {
            character_changePart[3].sprite = loseFace[1].sprite;
            _winLoss.GameEnd(1);
        }
        if(bluePartCount == 2)
        {
            character_changePart[0].sprite = loseFace[0].sprite;
            _winLoss.GameEnd(2);
        }
    }

    /// <summary>
    /// パーツをキャラクターの方に飛ばす
    /// </summary>
    private void PartSearch()
    {
        //パーツを取得したキャラクターが兄だった場合
        if (winnerNum == 1)
        {
            _partThrow.x *= -1;            
        }
        //else if(winnerNum == 2)
        //{
        //    _partThrow.drow_x *= -1;
        //}                
        //パーツをキャラクターの方に飛ばす
        _partThrow.partThrow();
    }
   
    /// <summary>
    /// 引き分け時に呼び出される
    /// </summary>
    public void drowdesplay()
    {
        Debug.Log("パーツ呼ばれた");
        StartCoroutine("drow_switching");
    }

    //パーツ未取得時の動き
    IEnumerator drow_switching()
    {
        throwPart.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/R_Face");
        drow_throwPart.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/B_Face");
        
        //パーツを表示
        throwPart.gameObject.SetActive(true);
        drow_throwPart.gameObject.SetActive(true);
        //透明な背景を入れる
        Blackout.SetActive(true);
        //エフェクトを再生
        effect.SetActive(true);
        Debug.Log("中央に表示");

        //if (_winLoss.drowtTrigger == true)
        //{
        //    drow_throwPart.gameObject.SetActive(true);
        //}

        yield return new WaitForSeconds(1f);
        _partThrow.drowCall();
        //PartSearch();

        yield return new WaitForSeconds(1f);

        //エフェクトを止める        
        effect.SetActive(false);

        //キャラクターのパーツを変更
        character_changePart[0].sprite = henkougo[0].sprite;
        character_changePart[3].sprite = henkougo[3].sprite;

        yield return new WaitForSeconds(1.5f);

        //透明の背景を消す
        Blackout.SetActive(false);

        //_winLoss.WinOrLose(3);
        redPartCount++;
        bluePartCount++;

        //勝者が兄なら
        //if (partNum <= 2)
        //{
        //    Debug.Log(partNum + "で兄の勝利");
        //    redPartCount++;
        //    _winLoss.WinOrLose(1);
        //}
        ////勝者が弟なら
        //else if (partNum >= 3)
        //{
        //    Debug.Log(partNum + "で弟の勝利");
        //    bluePartCount++;
        //    _winLoss.WinOrLose(2);
        //}

        //yield return new WaitForSeconds(0.5f);

        ////ゲーム終了時に呼ばれる
        //if (redPartCount == 2)
        //{
        //    _winLoss.GameEnd(1);
        //}
        //if (bluePartCount == 2)
        //{
        //    _winLoss.GameEnd(2);
        //}
    }

    /// <summary>
    /// ゲーム終了後にパーツを返納
    /// </summary>
    private void PartReturn()
    {
        for(int part = 0; part > character_changePart.Length-1; part++)
        {
            character_changePart[part].sprite = StoragePart[part].sprite;
        }        
    }

    /// <summary>
    /// 最初にパーツを格納
    /// </summary>
    private void PartStorage()
    {
        for (int strge = 0; strge > character_changePart.Length-1; strge++)
        {
            StoragePart[strge].sprite = character_changePart[strge].sprite;
        }
    }
}

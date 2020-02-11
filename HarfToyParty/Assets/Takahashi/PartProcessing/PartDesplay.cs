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
    GameObject effect;

    [SerializeField]
    Image throwPart;

    //中央のパーツの初期位置を保存
    Vector2 centerPartVector;

    int winnerNum;

    string getPart;
    string DesplayPart;
    string Desplay = "Desplay";
    int partNum;

    private void Start()
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

    /// <summary>
    /// エフェクトを再生しパーツをキャラクターの方に飛ばす
    /// </summary>
    private void PartSearch()
    {
        //キャラクターが兄だった場合
        if (winnerNum == 1)
        {
            _partThrow.x *= -1;
        }
        Debug.Log("中央に表示");
        
        //エフェクトを再生
        effect.SetActive(true);
        //パーツをキャラクターの方に飛ばす
        _partThrow.partThrow();
    }       

    //
    IEnumerator switching()
    {
        //中央に取ったパーツのデータを出力
        throwPart.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/" + getPart);
        //パーツを表示
        throwPart.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        
        PartSearch();
        //エフェクトを止める
        effect.SetActive(false);

        yield return new WaitForSeconds(2f);  

        //DesplayPart = "Sprites/ChangePart/" + Desplay + getPart;
        //character_changePart[partNum].sprite = Resources.Load<Sprite>(DesplayPart);
        //キャラクターのパーツを変更
        character_changePart[partNum].sprite = henkougo[partNum].sprite;
        
        yield return new WaitForSeconds(2f);

        //勝者が兄なら
        if(partNum <= 2)
        {
            _winLoss.WinOrLose(1);
        }
        //勝者が弟なら
        else if(partNum >= 3)
        {
            _winLoss.WinOrLose(2);
        }        
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

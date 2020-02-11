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
        _partThrow = GameObject.Find("throwObject").GetComponent<PartThrow>();
        _winLoss = GameObject.Find("ResultUI").GetComponent<WinLoss>();
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

    private void PartSearch()
    {
        if (winnerNum == 1)
        {
            _partThrow.x *= -1;
        }
        Debug.Log("中央に表示");
        
        effect.SetActive(true);
        _partThrow.partThrow();
    }       

    //
    IEnumerator switching()
    {
        throwPart.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/" + getPart);
        throwPart.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        PartSearch();
        effect.SetActive(false);

        yield return new WaitForSeconds(2f);  

        DesplayPart = "Sprites/ChangePart/" + Desplay + getPart;
        character_changePart[partNum].sprite = Resources.Load<Sprite>(DesplayPart);
        
        yield return new WaitForSeconds(2f);

        if(partNum <= 2)
        {
            _winLoss.WinOrLose(1);
        }
        else if(partNum >= 3)
        {
            _winLoss.WinOrLose(2);
        }        
    }
}

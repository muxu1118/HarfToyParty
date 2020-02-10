using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartDesplay : MonoBehaviour
{            
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
    PartThrow _partThrow;
    string getPart;
    string DesplayPart;
    string Desplay = "Desplay";
    int partNum;

    private void Start()
    {
        _partThrow = GameObject.Find("throwobj").GetComponent<PartThrow>();
    }

    private void Update()
    {
        
    }        

    /// <summary>
    /// パーツを取得したときに呼び出される
    /// </summary>
    public void PartGet(string part, int winner)
    {
        getPart = part;
        winnerNum = winner;
        switch (getPart) {
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
        throwPart.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/" + getPart);
        throwPart.gameObject.SetActive(true);
        effect.SetActive(true);
        _partThrow.partThrow();
    }       

    //
    IEnumerator switching()
    {
        PartSearch();
        
        yield return new WaitForSeconds(3f);  

        Debug.Log("表示");
        DesplayPart = "Sprites/ChangePart/" + Desplay + getPart;
        character_changePart[partNum].sprite = Resources.Load<Sprite>(DesplayPart);
    }
}

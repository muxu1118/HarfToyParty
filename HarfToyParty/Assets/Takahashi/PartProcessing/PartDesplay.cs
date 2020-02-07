using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartDesplay : MonoBehaviour
{    
    string getPart;

    //プレイヤーの変更する画像を選択
    [SerializeField]
    SpriteRenderer[] character_changePart;
       
    [SerializeField]
    GameObject effect;

    [SerializeField]
    Image throwPart;

    //中央のパーツの初期位置を保存
    Vector2 centerPartVector;

    private void Start()
    {
        
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if(i == 1)
        //    {
        //        PartGet("R_Hand");
        //        i++;
        //    }            
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    partThrow();
        //}       
    }

    /// <summary>
    /// プレイヤーが取得したパーツの名前を取得
    /// </summary>
    /// <param name="partName"></param>
    public void partNameGet(string partName)
    {
        getPart = partName;
    }

    /// <summary>
    /// 中央にパーツを表示
    /// </summary>
    private void centerPartDisplay(string part)
    {
        //CenterPart.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/"+part);
    }

    /// <summary>
    /// パーツを取得したときに呼び出される
    /// </summary>
    public void PartGet(string part)
    {
        getPart = part;
        switch (getPart) {
            case "R_Face":
                PartSearch();
                character_changePart[0].sprite = Resources.Load<Sprite>("Sprites/ChangePart/RightFace");
                //part.Split = Resources.Load<Sprite>("Sprite/Part/R_Leg");
                //choicePart[0] = Resources.Load<Sprite>("Sprite/Part/R_Leg");
                //PartSearch(0);
                break;
            case "R_Hand":
                PartSearch();
                character_changePart[1].sprite = Resources.Load<Sprite>("Sprites/ChangePart/RightArm");
                break;
            case "R_Leg":
                PartSearch();
                character_changePart[2].sprite = Resources.Load<Sprite>("Sprites/ChangePart/RightLeg");
                break;
            case "B_Face":
                PartSearch();
                character_changePart[3].sprite = Resources.Load<Sprite>("Sprites/ChangePart/LeftFace");
                break;
            case "B_Hand":
                PartSearch();
                character_changePart[4].sprite = Resources.Load<Sprite>("Sprites/ChangePart/LeftArm");
                break;
            case "B_Leg":
                PartSearch();
                character_changePart[5].sprite = Resources.Load<Sprite>("Sprites/ChangePart/LeftLeg");
                break;
            default:
                Debug.LogError(getPart);
                break;
        }
    }

    private void PartSearch()
    {
        Debug.Log("中央に表示");
        throwPart.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/" + getPart);
        throwPart.gameObject.SetActive(true);
        effect.SetActive(true);
    }       

    //力を加えたオブジェクトに重力をかける
    IEnumerator switching()
    {
        yield return new WaitForSeconds(1);        
    }
}

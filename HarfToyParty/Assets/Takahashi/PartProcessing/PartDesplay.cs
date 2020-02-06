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
    //投げるパーツ
    [SerializeField]
    Image CenterPart;
    //移動するパーツの速さ調整用
    [SerializeField]
    int x, y;
    [SerializeField]
    float gravity;
    [SerializeField]
    GameObject effect;

    [SerializeField]
    Rigidbody2D rb2;
    Vector2 pos;
    //パーツを徐々に透明にするためのもの
    float colorDown = 6;
    bool colorflag = false;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            partThrow();
        }

        //透明度を下げる
        if (colorflag)
        {
            colorDown -= Time.deltaTime;
            CenterPart.color = new Color(255, 255, 255, colorDown);
        }
        //下がりきったらおしまい
        else if (colorDown < 0)
        {
            colorflag = !colorflag;
            colorDown = 1;
            PartGet();
        }
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
        CenterPart.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/gimmick_body1_B");
    }

    /// <summary>
    /// パーツを取得したときに呼び出される
    /// </summary>
    private void PartGet()
    {
        switch (getPart) {
            case "R_Face":
                character_changePart[0].sprite = Resources.Load<Sprite>("Sprites/ChangePart/RightFace");
                //part.Split = Resources.Load<Sprite>("Sprite/Part/R_Leg");
                //choicePart[0] = Resources.Load<Sprite>("Sprite/Part/R_Leg");
                //PartSearch(0);
                break;
            case "R_Hand":
                character_changePart[1].sprite = Resources.Load<Sprite>("Sprites/ChangePart/RightArm");
                break;
            case "R_Leg":
                character_changePart[2].sprite = Resources.Load<Sprite>("Sprites/ChangePart/RightLeg");
                break;
            case "B_Face":
                character_changePart[3].sprite = Resources.Load<Sprite>("Sprites/ChangePart/LeftFace");
                break;
            case "B_Hand":
                character_changePart[4].sprite = Resources.Load<Sprite>("Sprites/ChangePart/LeftArm");
                break;
            case "B_Leg":
                character_changePart[5].sprite = Resources.Load<Sprite>("Sprites/Part/LeftLeg");
                break;
            default:
                Debug.LogError(getPart);
                break;
        }
    }

    private void PartSearch(int arrayNumber)
    {
        character_changePart[arrayNumber].sprite = Resources.Load<Sprite>("Sprite/Part" + getPart);
    }
     
    /// <summary>
    /// パーツを放物線を描いて飛ばす
    /// </summary>
    public void partThrow()
    {
        //パーツのカラーを徐々に薄くするトリガー
        colorflag = !colorflag;
        //放物線上を求める
        pos = new Vector2(x * 10 + 2 / (9.8f * 10 * 10), y * 10 + 2 / (9.8f * 10 * 10));
        //一度だけ力を加える
        rb2.AddForce(pos, ForceMode2D.Impulse);

        StartCoroutine(switching(1.5f));

        rb2.gravityScale = gravity;
    }

    //力を加えたオブジェクトに重力をかける
    IEnumerator switching(float stopTime)
    {
        yield return new WaitForSeconds(stopTime);        
    }
}

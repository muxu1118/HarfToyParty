using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDesplay : MonoBehaviour
{    
    string getPart;

    //プレイヤーの変更する画像を選択
    [SerializeField]
    SpriteRenderer[] character_changePart;
    [SerializeField]
    GameObject blackOut;

    int i = 1;

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
    }

    /// <summary>
    /// パーツを取得したときに呼び出される
    /// </summary>
    /// <param name="GetPart"></param>
    public void PartGet(string part)
    {
        string getPart = part;
        switch (getPart) {
            case "R_Face":
                character_changePart[0].sprite = Resources.Load<Sprite>("Sprites/ChangePart/RightFace");
                //part.Split = Resources.Load<Sprite>("Sprite/Part/R_Leg");
                //choicePart[0] = Resources.Load<Sprite>("Sprite/Part/R_Leg");
                //PartSearch(0);
                break;
            case "R_Hand":
                character_changePart[1].sprite = Resources.Load<Sprite>("Sprite/ChangePart/RightArm");
                break;
            case "R_Leg":
                character_changePart[2].sprite = Resources.Load<Sprite>("Sprite/ChangePart/RightLeg");
                break;
            case "B_Face":
                character_changePart[3].sprite = Resources.Load<Sprite>("Sprite/ChangePart/LeftFace");
                break;
            case "B_Hand":
                character_changePart[4].sprite = Resources.Load<Sprite>("Sprite/ChangePart/LeftArm");
                break;
            case "B_Leg":
                character_changePart[5].sprite = Resources.Load<Sprite>("Sprite/Part/LeftLeg");
                break;
        }
    }

    private void PartSearch(int arrayNumber)
    {
        character_changePart[arrayNumber].sprite = Resources.Load<Sprite>("Sprite/Part" + getPart);
    }

    private void partDisplay()
    {
        

    }

    IEnumerator Direct()
    {
        //blackOut.SetActive(true);

        yield return new WaitForSeconds(3f);

        //blackOut.SetActive(false);
    }

    public void conclusion(int win)
    {
        int winNumber = win;
        switch (winNumber)
        {
            case 1:
                character_changePart[0].color = new Color(255 / 2, 255 / 2, 255 / 2, 255);
                break;
            case 2:
                break;
        }
    }
}

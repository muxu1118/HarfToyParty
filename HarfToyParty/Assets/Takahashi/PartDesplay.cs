using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDesplay : MonoBehaviour
{
    //[SerializeField]
    //Sprite[] choicePart;
    string getPart;

    //[NamedArrayAttribute(new string[] {"R_Leg", "R_Face", "R_Hand", "B_Leg", "B_Face", "B_Hand" })]
    [SerializeField]
    Sprite[] choicePart;

    /// <summary>
    /// パーツを取得したときに呼び出される
    /// </summary>
    /// <param name="GetPart"></param>
    public void PartGet(string part)
    {
        getPart = part;
        switch (getPart) {
            case "R_Leg":
                choicePart[0] = Resources.Load<Sprite>("Sprite/Part/R_Leg");
                //PartSearch(0);
                break;
            case "R_Face":
                choicePart[1] = Resources.Load<Sprite>("Sprite/Part/R_Face");
                break;
            case "R_Hand":
                choicePart[2] = Resources.Load<Sprite>("Sprite/Part/R_Hand");
                break;
            case "B_Leg":
                choicePart[3] = Resources.Load<Sprite>("Sprite/Part/B_Leg");
                break;
            case "B_Face":
                choicePart[4] = Resources.Load<Sprite>("Sprite/Part/B_Face");
                break;
            case "B_Hand":
                choicePart[5] = Resources.Load<Sprite>("Sprite/Part/B_Hand");
                break;
        }
    }

    private void PartSearch(int arrayNumber)
    {
        choicePart[arrayNumber] = Resources.Load<Sprite>("Sprite/Part" + getPart);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterPrehub : MonoBehaviour
{   
    GameObject redCaracter;
    GameObject blueCaracter;
    GameObject redColorCaracter;
    GameObject blueColorCaracter;

    [SerializeField]
    float redCharacter_x, redCharacter_y;
    [SerializeField]
    float blueCharacter_x, blueCharacter_y;

    Vector2 scale;
    void Start()
    {
        //scale.x = 0.75f;
        //scale.y = 0.75f;
        redCaracter = (GameObject)Resources.Load("Prefabs/NewPrefab/Ani_red");
        blueCaracter = (GameObject)Resources.Load("Prefabs/NewPrefab/otouto_blue");
        redColorCaracter = (GameObject)Resources.Load("Prefabs/NewPrefab/Ani_red_h");
        blueColorCaracter = (GameObject)Resources.Load("Prefabs/NewPrefab/otouto_blue_h");
        //redCaracter.transform.localScale = scale;
    }

    /// <summary>
    /// 兄が勝った時に呼べれる
    /// </summary>
    public void RedWinChange()
    {
        //DestroyCaracter();
        //Instantiate(redColorCaracter, new Vector3(redCharacter_x, redCharacter_y, 0.0f), Quaternion.identity);
        //Instantiate(blueCaracter, new Vector3(blueCharacter_x, blueCharacter_y, 0.0f), Quaternion.identity);
        //tear.transform.SetParent(panel.transform, false);
    }

    /// <summary>
    /// 弟が勝った時に呼ばれる
    /// </summary>
    public void BlueWinChange()
    {
        DestroyCaracter();
        Instantiate(redCaracter, new Vector3(redCharacter_x, redCharacter_y, 0.0f), Quaternion.identity);
        Instantiate(blueColorCaracter, new Vector3(blueCharacter_x, blueCharacter_y, 0.0f), Quaternion.identity);
    }

    private void DestroyCaracter()
    {
        Destroy(GameObject.Find("red_half"));
        Destroy(GameObject.Find("blue_half"));
    }
}

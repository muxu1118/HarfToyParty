using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterPrehub : MonoBehaviour
{   
    GameObject redCaracter;
    GameObject blueCaracter;
    GameObject redColorCaracter;
    GameObject blueColorCaracter;
    GameObject redPrehub;
    GameObject bluePrehub;

    //[SerializeField]
    float redCharacter_x = 9, Character_y = -5;
    //[SerializeField]
    float blueCharacter_x = 22;

    Vector2 scale;
    [SerializeField]
    Animator _animeotrRed;
    [SerializeField]
    Animator _animeotrBlue;
    bool win = false, lose = false;

    void Start()
    {
        _animeotrRed = gameObject.GetComponent<Animator>();
        _animeotrBlue = gameObject.GetComponent<Animator>();
        //scale.x = 0.75f;
        //scale.y = 0.75f;
        redCaracter = (GameObject)Resources.Load("Prefabs/NewPrefab/red_half");
        blueCaracter = (GameObject)Resources.Load("Prefabs/NewPrefab/blue_half");
        redColorCaracter = (GameObject)Resources.Load("Prefabs/NewPrefab/red_Full");
        blueColorCaracter = (GameObject)Resources.Load("Prefabs/NewPrefab/blue_Full");
        //redCaracter.transform.localScale = scale;

        Debug.Log(redCaracter);
    }

    /// <summary>
    /// 兄が勝った時に呼べれる
    /// </summary>
    public void RedWinChange()
    {
        //DestroyCaracter();
        //redPrehub = Instantiate(redColorCaracter, new Vector3(redCharacter_x, Character_y, 0.0f), Quaternion.identity);
        //redPrehub.transform.SetParent(gameObject.transform, false);
        //bluePrehub = Instantiate(blueCaracter, new Vector3(blueCharacter_x, Character_y, 0.0f), Quaternion.identity);
        //bluePrehub.transform.SetParent(gameObject.transform, false);
        //tear.transform.SetParent(panel.transform, false);
        //_animeotrRed.SetBool("WinTriggle", true);
        //_animeotrBlue.SetBool("LoseTriggle", true);
        Debug.Log("キャラクターを入れ替えあるはずだったよ");
    }

    /// <summary>
    /// 弟が勝った時に呼ばれる
    /// </summary>
    public void BlueWinChange()
    {
        //DestroyCaracter();
        //redPrehub = Instantiate(redCaracter, new Vector3(redCharacter_x, Character_y, 0.0f), Quaternion.identity);
        //redPrehub.transform.SetParent(gameObject.transform, false);
        //bluePrehub = Instantiate(blueColorCaracter, new Vector3(blueCharacter_x, Character_y, 0.0f), Quaternion.identity);
        //bluePrehub.transform.SetParent(gameObject.transform, false);
        //_animeotrBlue.SetBool("WinTriggle", true);
        //_animeotrRed.SetBool("LoseTriggle", true);
        Debug.Log("キャラクターを入れ替えあるはずだったよ");
    }

    private void DestroyCaracter()
    {
        Destroy(GameObject.Find("red_half"));
        Destroy(GameObject.Find("blue_half"));
    }
}

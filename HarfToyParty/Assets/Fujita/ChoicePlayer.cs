using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoicePlayer : MonoBehaviour{

    public GameObject Player1, Player2;
    public static int Pl_1score = 0;
    public static int Pl_2score = 0;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void choice()
    {
        if (Application.isEditor)
        {
        //デバック用
            if (Player1)
            {
                Debug.Log("1を選択した");
                //背景をキャラ1に変える＋Pl_1scoreを持たせる

            }
            else if (Player2)
            {
                Debug.Log("2を選択した");
            }
            //タブレット用
            else if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Debug.Log("タップした");
                }
            }
        }
    }
}

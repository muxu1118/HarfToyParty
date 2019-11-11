using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoicePlayer : MonoBehaviour{

    public GameObject Player1, Player2;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void choice()
    {
        if (Application.isEditor)
        {
            //デバック用
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("プレイヤーを選択した");
                if (Player1)
                {
                    Debug.Log("1を選択した");
                    //背景を1に変える
                }
                if (Player2)
                {
                    Debug.Log("2を選択した");
                    //背景を2に変える
                }
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
    void Update()
    {
        
    }
}

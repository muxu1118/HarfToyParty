using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoicePlayer : MonoBehaviour{

    public GameObject Player1, Player2;
    public static int Pl_1score = 0;
    public static int Pl_2score = 0;

    public MapKind playerID;//Player1か2

    public int[] player = new int[2];
    

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
                playerID = MapKind.Player1;
                //Pl_1scoreを持たせる
            }
            else if (Player2)
            {
                Debug.Log("2を選択した");
                playerID = MapKind.Player2;
            }


         //タブレット用
            else if (Player1 && Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Debug.Log("1をタップした");
                }
            }
            else if (Player2 && Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Debug.Log("2をタップした");
                }
            }
        }
    }
}

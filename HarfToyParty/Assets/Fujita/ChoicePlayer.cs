using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoicePlayer : MonoBehaviour{

    public GameObject Player1, Player2;
    public static int Pl_1score = 0;
    public static int Pl_2score = 0;

    [SerializeField]
    private Button button;
    [SerializeField]
    private Button button2;
    public MapKind playerID;//Player1か2

    //public int[] player = new int[2];
    
    void Start()
    {
    }

    public void choice()
    {
        //デバック用
        if (Player1)
        {
            
            button.interactable = false;
            Debug.Log("1を選択した");
            playerID = MapKind.Player1;
            if(button.interactable == false && button2.interactable == false)
            {
                GameManager.instance.StateChange();
                SceneManager.LoadScene("MainGame");
                Debug.Log("GameSceneを呼んでるよ");
            }
        }
        else if (Player2)
        {
            
            button2.interactable = false;
            Debug.Log("2を選択した");
            playerID = MapKind.Player2;
            if (button.interactable == false && button2.interactable == false)
            {
                GameManager.instance.StateChange();
                SceneManager.LoadScene("MainGame");
                Debug.Log("GameSceneを呼んでるよ");
            }
        }
        
        else if (Application.isEditor)
        {
        //タブレット用
            if (button && Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Debug.Log("1をタップした");
                    button.interactable = false;
                    playerID = MapKind.Player1;
                    if (button.interactable == false && button2.interactable == false)
                    {
                        Debug.Log("Sceneを呼んでるよ");
                    }
                }
            }
            else if (button2 && Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Debug.Log("2をタップした");
                    button.interactable = false;
                    playerID = MapKind.Player1;
                    if (button.interactable == false && button2.interactable == false)
                    {
                        Debug.Log("Sceneを呼んでるよ");
                    }
                }
            }
        }
    }
}

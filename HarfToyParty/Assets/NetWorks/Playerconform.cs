using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerconform : MonoBehaviour
{
    /// <summary>
    /// プレイヤー準備確認
    /// </summary>
    // Start is called before the first frame update

    bool player1 = false;
    bool player2 = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("player1Conform");
            player1 = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("player2Conform");
            player2 = true;
        }

        if (player1 && player2)
        {
            //シーン遷移
            Debug.Log("mainSceneに移動");
            SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
            player1 = false;
            player2 = false;
        }
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class result : MonoBehaviour
{
    public static int Pl_1score;
    public static int Pl_2score;

    public static int getPl_1score()
    {
        return Pl_1score;
    }
    public static int getPl_2score()
    {
        return Pl_2score;
    }


    /// <summary>
    /// どちらかが2セットとったらリザルト画面に飛ぶ
    /// </summary>
    private void GameEnd()
    {
        if(Pl_1score >= 2 || Pl_2score >= 2)
        {
            SceneManager.LoadScene("result");
        }
    }
}

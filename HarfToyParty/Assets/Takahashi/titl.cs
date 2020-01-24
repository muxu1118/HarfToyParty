using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titl : MonoBehaviour
{   
    private void Start()
    {
        
    }

    /// <summary>
    /// タイトルシーンに移動
    /// </summary>
    public void Onclick()
    {
        GameManager.instance.StateChange();
        SceneManager.LoadScene("Choice");        
        this.gameObject.SetActive(false);
    }

    private void gomi()
    {
        //    [SerializeField]
        //string[] serihu;
        //string ko;

        //int[] mozisuu = new int[3];

        //private void Start()
        //{
        //    for (int i = 0; i < serihu.Length; i++)
        //    {
        //        ko = serihu[i];
        //        mozisuu[i] = ko.Length;
        //        Debug.Log(mozisuu[i]);
        //    }
        //    //ko = serihu[0];
        ////serihu[0] = "ko";
        //mozisuu[0] = ko.Length;
        //Debug.Log(mozisuu[0]);
    }
}      
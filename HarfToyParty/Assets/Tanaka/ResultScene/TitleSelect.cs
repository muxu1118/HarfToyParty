using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ボタンクリック→タイトルシーンへ遷移
public class TitleSelect : MonoBehaviour
{ 
    public void Onclick()
    {
        SceneManager.LoadScene("Title");
    }
}

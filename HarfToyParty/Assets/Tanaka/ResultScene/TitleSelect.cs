using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ボタンクリック→タイトルシーンへ遷移
public class TitleSelect : MonoBehaviour
{
    [SerializeField]
    GameObject Panel;
    public void Onclick()
    {
        GameManager.instance.StateChange();
        SceneManager.LoadScene("Choice");
        Panel.SetActive(false);
    }
}

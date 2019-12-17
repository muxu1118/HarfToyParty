using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{

    [SerializeField] private GameObject Panel;

    // 関数呼び出しでリザルト画面パネルを出す
    public void WinnerDisplay()
    {
        Panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Onclick()
    {
        SceneManager.LoadScene("Title");
    }
}
// ほかのスクリプトで呼び出された時パネルを呼び出し
// 分けて書くことを意識する、分けて書くと後でバグが起きた時に分かりやすい
// 例.ボタン一つ一つで分ける,,,,同じようなスクリプトで分けるのはわかりにくくなるからNG
// SerializeFieldはできるだけ使わない

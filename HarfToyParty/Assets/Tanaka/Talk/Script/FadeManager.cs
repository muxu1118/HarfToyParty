using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{

    public static Canvas fadeCanvas;
    public static Image fadeImage;

    public static float alpha = 0.0f;

    public static bool isFadeIn = false;
    public static bool isFadeOut = false;

    public static float fadeTime = 0.2f;
    
    // 遷移先のシーン番号
    public static int nextScene = 2;
    
    // フェード用のCanvasとImage生成
    static void Init()
    {
        // フェード用の Canvas と Image 生成
        GameObject FadeCanvasObject = new GameObject("CanvasaFade");
        fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<FadeManager>();

        // 最前面になるようソートオーダー設定
        fadeCanvas.sortingOrder = 100;
        
        // フェード用の Image 生成
        fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector3.zero;
        
        // Image サイズの設定
        fadeImage.rectTransform.sizeDelta = new Vector2(2000, 2000);
    }

    // フェードイン開始
    public static void FadeIn()
    {
        if (fadeImage == null) Init();
        fadeImage.color = Color.black;
        isFadeIn = true;
    }

    // フェードアウト開始
    public static void FadeOut(int n)
    {
        if (fadeImage == null) Init();
        nextScene = n;
        fadeImage.color = Color.clear;
        fadeCanvas.enabled = true;
        isFadeOut = true;
    }

    void Update()
    {
        // フラグ有効なら毎フレームフェードイン / アウト処理
        if(isFadeIn)
        {
            // 経過時間から透明度計算
            alpha -= Time.deltaTime / fadeTime;

            // フェードイン終了判定
            if (alpha <= 0.0f)
            {
                isFadeIn = false;
                alpha = 0.0f;
                fadeCanvas.enabled = false;
            }

            // フェード用 Image の色、透明度設定
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        else if (isFadeOut)
        {
            // 経過時間から透明度計算
            alpha += Time.deltaTime / fadeTime;

            // フェードアウト終了判定
            if (alpha >= 1.0f)
            {
                isFadeOut = false;
                alpha = 1.0f;

                // 次のシーン遷移
                SceneManager.LoadScene(nextScene);
            }

            // フェード用の Image の色、透明度設定
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }
}

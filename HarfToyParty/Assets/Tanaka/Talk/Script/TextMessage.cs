using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextMessage : MonoBehaviour
{
    List<string> messageList = new List<string>();

    [SerializeField] GameObject Check;
    [SerializeField] Text text;
    [SerializeField] float novelSpeed;
    [SerializeField] float sentenceSpeed;

    int novelListIndex = 0;

    public static string[] sentence = new string[7];
    public static int sentenceNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        FadeManager.FadeIn();
        Check.SetActive(false);
        Sentence();
        for (int i = 0; i < sentence.Length; i++)
            messageList.Add(sentence[i]);
        
        StartCoroutine(Novel());
    }

    private void Update()
    {
        Scene();
    }

    void ColorChange()
    {
        // キャラの不透明度とカラー替え
        if (novelListIndex == 6)
            sentenceNum = 3;
        else if (novelListIndex % 2 == 0)
            sentenceNum = 1;
        else
            sentenceNum = 2;
    }

    private IEnumerator Novel()
    {
        ColorChange();
        int messageCount = 0;
        text.text = "";

        while(messageList[novelListIndex].Length > messageCount)
        {
            text.text += messageList[novelListIndex][messageCount];
            messageCount++;
            yield return new WaitForSeconds(novelSpeed);
        }

        yield return new WaitForSeconds(sentenceSpeed);
        novelListIndex++;

        if (novelListIndex < messageList.Count)
            StartCoroutine(Novel());

        else if (novelListIndex == 7)
            Check.SetActive(true);
    }

    public void Scene()
    {
        if (Check.activeSelf == true)
        {
            if (Input.GetKeyDown("joystick button 2"))
            {
                GameManager.instance.StateChange();
                FadeManager.FadeOut(2);
            }
        }
    }

    // シナリオ変わる可能性有り
    public void Sentence()
    {
        sentence[0] = "やっほー！\n今日は僕たちと遊んでくれる？";
        sentence[1] = "くれるよね？";
        sentence[2] = "僕と弟はこのトイパーティを\n開いてみたんだ";
        sentence[3] = "僕とお兄ちゃんはそっくりでしょ？\n双子なんだ";
        sentence[4] = "そんな僕たちの\n体を半分にしてみたから";
        sentence[5] = "僕たちを操作して\n先に体のパーツをゲットしてね";
        sentence[6] = "それじゃあ、レッツスタート！";
    }
}

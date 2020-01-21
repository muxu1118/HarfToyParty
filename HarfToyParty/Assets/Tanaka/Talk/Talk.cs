using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    private Text text;
    
    // 文章の数の宣言 二次元配列[]↓[]→
    private string[] sentence = new string[10];

    // 文章の番号
    public int sentenceNum = 0;
    // 今出力されている文字数
    private int textNum;

    // 0.1秒ごとに文字が出力される
    private float textSpeed = 0.1f;
    private float maxTextSpeed;

    private bool textFlag = false;

    private void Start()
    {
        text = GetComponent<Text>();
        Sentence();
        maxTextSpeed = textSpeed;
        //StartCoroutine("test");
        //StartCoroutine("aaa");
    }

    private void Update()
    {
        if (sentenceNum >= 10) { return; }

        if (Time.timeScale == 0) { return; }

        // 0.1秒ずつ減っていって0を下回った時
        textSpeed -= Time.deltaTime;

        if (textSpeed <= 0)
        {
            // 文字を出力する
            DisplaySentence();

            // deltTimeで減った分をもとに戻す
            textSpeed = maxTextSpeed;
            // 出力する文字数を増やす
            textNum++;

            if (sentence[sentenceNum].Length == textNum - 1)
            {
                //textFlag = false;
                StartCoroutine("aaa");
                Debug.Log("QTi ami-3");
                textNum = 0;
                sentenceNum++;

                if (sentenceNum > sentence.Length - 1) return;

                //textNum = 0;
                //textFlag = true;
            }
        }
    }





    IEnumerator aaa()
    {
        Debug.Log("QTi ami-");
        yield return new WaitForSeconds(1);
        Debug.Log("QTi ami-2");
        //textNum = 0;
        //sentenceNum++;
    }

    IEnumerator test()
    {
        Debug.Log("aaa");
        for (int a = 0; a < 10; a++)
        {
            Debug.Log(a);
            if (a == 2)
            {
                Debug.Log("bbb");
                yield return new WaitForSeconds(5);
                //text.GetComponent<Text>().text = ;
            }
         }
    }

    public void Sentence()
    {
        sentence[0] = "こんにちは";
        sentence[1] = "初めまして";
        sentence[2] = "HalfToyPartyへ\nようこそ";
        sentence[3] = "今からルール説明を\nしていくね";
        sentence[4] = "邪魔なブロックを動かして\n体のパーツを探してきてね";
        sentence[5] = "先に体のパーツを\n取り戻したほうが勝利だよ";
        sentence[6] = "ルールは大体こんな感じ";
        sentence[7] = "分かったかな？";
        sentence[8] = "それじゃあ行ってらっしゃい";
        sentence[9] = "また会おうねっ";
    }

    public void DisplaySentence()
    {
        text.text = sentence[sentenceNum].Substring(0, textNum);
    }

    private void Select()
    {
        if (Input.GetKeyDown("joystick 1 button 2"))
        {
            SceneManager.LoadScene("MainGame");
        }
    }

    private void Scene()
    {
        if (Input.GetKeyDown("joystick 1 button 2"))
        {
            SceneManager.LoadScene("Title");
        }
    }
}

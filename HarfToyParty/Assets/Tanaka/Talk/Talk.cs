using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    private Text text;
    // 文章の数の宣言
    private string[] sentence = new string[5];
    // 文章の番号
    private int sentenceNum = 0;
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
    }

    private void Update()
    {
        if (sentenceNum >= 5) { return; }
        if (Time.timeScale == 0) { return; }

        if(Input.GetMouseButtonDown(0))
        {
            textNum = 0;
            sentenceNum++;
            textFlag = true;
        }
        // 0.1秒ずつ減っていって0を下回った時
        textSpeed -= Time.deltaTime;
        if (textSpeed <= 0 && textFlag)
        {
            // 文字を出力する
            DisplaySentence();
            // deltTimeで減った分をもとに戻す
            textSpeed = maxTextSpeed;
            // 出力する文字数を増やす
            textNum++;

            if (sentence[sentenceNum].Length == textNum - 1)
            {
                textFlag = false;
            }
        }
    }

    public void Sentence()
    {
        sentence[0] = "こんにちは";
        sentence[1] = "はじめまして";
        sentence[2] = "Half Toy Partyへようこそ";
        sentence[3] = "僕たちの体のパーツを\n探してきて";
        sentence[4] = "行ってらっしゃい";
    }

    public void DisplaySentence()
    {
        text.text = sentence[sentenceNum].Substring(0, textNum);
    }

    private void Scene()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Title");
        }
    }
}

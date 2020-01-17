using System.Collections;
using System.Collections.Generic;
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
    }

    private void Update()
    {
        if (sentenceNum >= 5) { return; }

        if (Time.timeScale == 0) { return; }

        //if (Input.GetKeyDown("joystick button 2"))
        //{
            textNum = 0;
            sentenceNum++;
            textFlag = true;
        //}
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
        sentence[0] = "1";
        sentence[1] = "2";
        sentence[2] = "3";
        sentence[3] = "4";
        sentence[4] = "5";
        sentence[5] = "6";
        sentence[6] = "7";
        sentence[7] = "8";
        sentence[8] = "9";
        sentence[9] = "10";
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

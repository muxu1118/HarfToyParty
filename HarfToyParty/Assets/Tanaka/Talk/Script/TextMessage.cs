using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextMessage : MonoBehaviour
{
    List<string> messageList = new List<string>();

    [SerializeField] Text text;
    [SerializeField] float novelSpeed;
    [SerializeField] float sentenceSpeed;

    int novelListIndex = 0;

    public static string[] sentence = new string[10];
    public static int sentenceNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        Sentence();
        for (int i = 0; i < sentence.Length; i++)
            messageList.Add(sentence[i]);
        
        StartCoroutine(Novel());
    }

    void sssss()
    {
        if (novelListIndex == 9 || novelListIndex == 2)
            sentenceNum = 3;
        else if(novelListIndex % 2 == 0)
            sentenceNum = 1;
        else
            sentenceNum = 2;
    }

    private IEnumerator Novel()
    {
        sssss();
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

        else if (novelListIndex == 10)
            SceneManager.LoadScene("Choice");
    }

    public void Sentence()
    {
        sentence[0] = "こんにちは";
        sentence[1] = "初めまして";
        sentence[2] = "Harf Toy Partyへようこそ";
        sentence[3] = "今からルール説明をしていくね";
        sentence[4] = "邪魔なブロックを動かして\n体のパーツを探してきてね";
        sentence[5] = "先に体のパーツを\n取り戻したほうが勝利だよ";
        sentence[6] = "ルールは大体こんな感じ";
        sentence[7] = "分かったかな？";
        sentence[8] = "それじゃあ行ってらっしゃい";
        sentence[9] = "また会おうね^^";
    }
}

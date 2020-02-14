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

    public static string[] sentence = new string[9];
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
        if (novelListIndex == 7 || novelListIndex == 8)
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

        else if (novelListIndex == 9)
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
        sentence[0] = "やっほ～？僕たちトイドールの双子、\n僕はお兄ちゃん";
        sentence[1] = "僕は弟、名前はまだないよ？";
        sentence[2] = "僕たちはおもちゃ箱で\nトイパーティーを開催したよ！";
        sentence[3] = "おもちゃのブロックを引いたり、\n押したり";
        sentence[4] = "おもちゃの爆弾を投げたり！";
        sentence[5] = "相手の邪魔をしながら、\n僕たちを操作してお兄ちゃんより";
        sentence[6] = "弟より先に！";
        sentence[7] = "ゲットしてね！";
        sentence[8] = "それじゃあ、レッツスタート！！";
    }
}

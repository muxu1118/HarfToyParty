using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMock : MonoBehaviour
{
    private float time;
    [SerializeField]
    private float interval;
    private GameObject Panel;
    void Start()
    {
        FadeManager.FadeIn();
        time = Time.time;
        GameManager.instance.startScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > time)
        {

        }
        SceneLoad();
    }
    #region
    private void TapStart()
    {
        //テキストが出終わったら点滅開始
        for (int i = 0; i < 45; i++)
        {
            StartCoroutine("SakuraOut");
        }
    }

    private void TapOut()
    {
        //前回の点滅の処理を止める
        for (int i = 0; i < 45; i++)
        {
            StopCoroutine("SakuraOut");
        }
    }

    //透明度を1~0と0~1へと徐々に変更することにより点滅させる(fadein,fadeoutの要領)
    IEnumerator SakuraOut()
    {
        while (true)
        {
            //fadein
            while (interval <= 1)
            {
                //Panel.GetComponent<>().color += new Color(0, 0, 0, fade);
                //fadeInOut += fade;
                yield return null;
            }
            //fadeout
            while (interval >= 0)
            {
                //Sakura.GetComponent<Image>().color -= new Color(0, 0, 0, fade);
                //fadeInOut -= fade;
                yield return null;
            }
        }
    }
    #endregion
    private void SceneLoad()
    {
        if (Input.GetKeyDown("joystick button 2"))
        {     
            GameManager.instance.StateChange();
            FadeManager.FadeOut(1);
            Debug.Log("ChoiceSceneを呼んでるよ");
        }
    }
}

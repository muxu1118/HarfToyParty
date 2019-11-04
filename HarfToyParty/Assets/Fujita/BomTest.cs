using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomTest : MonoBehaviour
{
    private GameObject Bomb;
    public Text BomText; //画面タイマー表示用テキスト
    [SerializeField]
    private float timeExplosion;//爆発までの時間
    private float timeEpTrigger = 0;

    void Update()
    {
        Explosion();
    }

    private void Explosion()
    {
        if(timeExplosion >= timeEpTrigger)
        {
            timeExplosion -= Time.deltaTime;
            if(timeExplosion <= timeEpTrigger)
            {
                Destroy(gameObject);
            }
        BomText.text = "爆発まで" + timeExplosion.ToString("f1") + "秒";
        }
    }
}

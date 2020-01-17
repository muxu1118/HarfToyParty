using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomTest : MonoBehaviour
{
    [SerializeField]
    private GameObject BombRange;

    public Text BomText; //画面タイマー表示用テキスト
    [SerializeField]
    private float timeExplosion;//爆発までの時間
    private float timeEpTrigger = 0;

    public Vector2 MyPosi { get; set; } = new Vector2();
    private void Start()
    {
        BombRange.SetActive(false);
    }
    void Update()
    {
        WaitTime();
    }

    private void WaitTime()
    {
        // 爆発までの時間
        if(timeExplosion >= timeEpTrigger)
        {
            timeExplosion -= Time.deltaTime;
           
        //BomText.text = "爆発まで" + timeExplosion.ToString("f0") + "秒";
        }else
        {
            //爆発したいよ
            Explosion();
            transform.GetChild(0).gameObject.SetActive(false);
            BombRange.SetActive(true);
            StartCoroutine(DestroyObject());
        }
    }

    /// <summary>
    /// 爆発させる関数
    /// </summary>
    private void Explosion()
    {
        int x = (int)MyPosi.x, y = (int)MyPosi.y;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (y - j > 6 || y - j < 0 || x + i > 6 || x + i < 0) 
                {
                    continue;
                }
                if (Map.instance.mapInt[y - j, x + i] == (int)MapKind.Player1 || Map.instance.mapInt[y - j, x + i] == (int)MapKind.Player2) 
                {
                    // プレイヤーにダメージを与えたい
                    Debug.Log("プレイヤーの位置X:" + (x + i) + "Y:" + (y - j));
                    Debug.Log((MapKind)Map.instance.mapInt[y - j, x + i] + "にダメージ");
                }
                if (Map.instance.mapInt[y - j, x + i] >= (int)MapKind.BreakWall1 && Map.instance.mapInt[y - j, x + i] <= (int)MapKind.BreakWall6)
                {
                    BreakWall[] breakObjects = FindObjectsOfType<BreakWall>();
                    foreach (BreakWall BW in breakObjects)
                    {
                        if((int)BW.MyWallP == Map.instance.mapInt[y - j, x + i])
                        {
                            Debug.Log(BW+"を破壊");
                            Destroy(BW.transform.gameObject);
                            Map.instance.mapInt[y - j, x + i] = (int)MapKind.YUKA;
                        }
                    }
                }
            }
        }
    }

    IEnumerator DestroyObject()
    {
        float time = 0.5f;
        while(time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }
}

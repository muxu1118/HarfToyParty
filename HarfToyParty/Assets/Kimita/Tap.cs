using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * タップを検知する
 */
public class Tap : MonoBehaviour
{
    
    protected Vector2 TouchPosition = Vector2.zero;
    protected Vector2 StartPosition = Vector2.zero;

    // タップしたオブジェクト
    protected GameObject tapObject;
    private Canvas canvas;
    //　触ったボタン 0：移動 1：ギミック 
    protected static int[] nowTouching = new int[5];
    protected static Touch[] tc = new Touch[5];
    private static bool isBomb = false;
    private GameObject BombAria;
    private string playerName;
    private MapKind playerKind;

    public static bool IsBomb { get => isBomb; set => isBomb = value; }

    private void Awake()
    {
        BombAria = GameObject.Find("BombAria");
    }
    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        BombAria.SetActive(false);
        playerName = "Player2";
        playerKind = MapKind.Player2;
    }

    public virtual void GetTap()
    {
        // スマホタップの取得 touchCountが0以上でタップ判定
        if (Input.touchCount <= 0) return;


        // タッチ情報の取得
        Touch touch = Input.GetTouch(0);
        // タッチされている指の数だけ処理
        for (int i = 0; i < Input.touchCount; i++)
        {
            // タッチ情報をコピー
            Touch t = Input.GetTouch(i);
            // タッチしたときかどうか
            if (t.phase == TouchPhase.Began)
            {
                
                Vector3 v3 = new Vector3();
                v3 = GameObject.Find("MoveButton").transform.position;
                Vector2 vec2 = Camera.main.ScreenToWorldPoint(t.position);
                if ((vec2.x >= v3.x-0.5f) && (vec2.x <= v3.x + 0.5f)&& (vec2.y >= v3.y - 0.5f) && (vec2.y <= v3.y + 0.5f))
                {
                    Debug.Log("今のタップは" + i + "本目で" + "移動キーを押しています");
                    nowTouching[0] = i;
                    tc[i] = t;
                }
                v3 = GameObject.Find("BombButton").transform.position;
                if ((vec2.x >= v3.x - 0.7f) && (vec2.x <= v3.x + 0.7f) && (vec2.y >= v3.y - 0.7f) && (vec2.y <= v3.y + 0.7f))
                {
                    Debug.Log("今のタップは" + i + "本目で" + "ギミックボタンを押しています");
                    if (isBomb)
                    {
                        isBomb = false;
                        Debug.Log("爆弾置く");
                        BombAria.SetActive(false);
                        // 爆弾を置く
                        Map.instance.BombAria(GameObject.Find(playerName).GetComponent<Player>().rot, playerKind);
                    }
                    else
                    {
                        isBomb = true;
                        Debug.Log(BombAria);
                        BombAria.SetActive(true);
                        // 爆弾範囲をだす
                        StartCoroutine(Map.instance.AriaSet(playerKind, MapKind.BombAria));
                    }
                    nowTouching[1] = i;
                    tc[i] = t;
                }

                v3 = GameObject.Find("MoveButton (1)").transform.position;
                if ((vec2.x >= v3.x - 0.5f) && (vec2.x <= v3.x + 0.5f) && (vec2.y >= v3.y - 0.5f) && (vec2.y <= v3.y + 0.5f))
                {
                    Debug.Log("今のタップは" + i + "本目で" + "移動キーを押しています");
                    nowTouching[0] = i;
                    tc[i] = t;
                }
                v3 = GameObject.Find("BombButton (1)").transform.position;
                if ((vec2.x >= v3.x - 0.7f) && (vec2.x <= v3.x + 0.7f) && (vec2.y >= v3.y - 0.7f) && (vec2.y <= v3.y + 0.7f))
                {
                    Debug.Log("今のタップは" + i + "本目で" + "ギミックボタンを押しています");
                    if (isBomb)
                    {
                        isBomb = false;
                        Debug.Log("爆弾置く");
                        BombAria.SetActive(false);
                        // 爆弾を置く
                        Map.instance.BombAria(GameObject.Find("Player1").GetComponent<Player>().rot, MapKind.Player1);
                    }
                    else
                    {
                        isBomb = true;
                        Debug.Log(BombAria);
                        BombAria.SetActive(true);
                        // 爆弾範囲をだす
                        StartCoroutine(Map.instance.AriaSet(playerKind, MapKind.BombAria));
                    }
                    nowTouching[1] = i;
                    tc[i] = t;
                }
            }
            //if (t.phase == TouchPhase.Ended)
            //{
                
            //    Vector3 v3 = new Vector3();
            //    v3 = GameObject.Find("BombButton").transform.position;
            //    Vector2 vec2 = Camera.main.ScreenToWorldPoint(t.position);
            //    if ((vec2.x >= v3.x - 0.7f) && (vec2.x <= v3.x + 0.7f) && (vec2.y >= v3.y - 0.7f) && (vec2.y <= v3.y + 0.7f)&&isBomb)
            //    {
            //        Debug.Log("爆弾置く");
            //        GameObject.Find("Player1").transform.GetChild(0).GetComponent<Bomb>().BombPut();
            //    }
            //}
        }

        //// タップしたオブジェクトを取得
        //if (touch.phase == TouchPhase.Began )
        //{
        //    // タップしているところを取得
        //    TouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //    StartPosition = TouchPosition;
        //    // タッチした位置からRayを飛ばす
        //    Ray ray = Camera.main.ScreenPointToRay(TouchPosition);
        //    // Rayを飛ばす
        //    RaycastHit2D hit = new RaycastHit2D();
        //    if (Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 100,2))
        //    {
        //        tapObject = hit.collider.gameObject;
        //    }
        //}

    }
}

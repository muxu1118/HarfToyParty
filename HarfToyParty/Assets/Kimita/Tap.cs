using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/**
 * タップを検知する
 */
public class Tap : NetworkBehaviour
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
    private static bool isBomb2 = false;
    
    private GameObject BombAria;
    private string playerName;
    private MapKind playerKind;


    public static bool IsBomb { get => isBomb; set => isBomb = value; }
    public static bool IsBomb2 { get => isBomb2; set => isBomb2 = value; }
    
    private void Awake()
    {

    }

    private void Start()
    {
        playerName = transform.GetChild(0).name;
        playerKind = (MapKind.Player1.ToString() == playerName) ? MapKind.Player1 : MapKind.Player2;
        BombAria = (playerKind == MapKind.Player1) ? GameObject.Find("BombAria1") : GameObject.Find("BombAria2");
        Debug.Log("名:" + playerName + "種:" + playerKind + "範囲:" + BombAria);
         canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        
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
                if ((vec2.x >= v3.x - 0.5f) && (vec2.x <= v3.x + 0.5f) && (vec2.y >= v3.y - 0.5f) && (vec2.y <= v3.y + 0.5f))
                {
                    Debug.Log("今のタップは" + i + "本目で" + "移動キーを押しています");
                    nowTouching[0] = i;
                    tc[i] = t;
                }
                v3 = GameObject.Find("BombButton").transform.position;
                if ((vec2.x >= v3.x - 0.7f) && (vec2.x <= v3.x + 0.7f) && (vec2.y >= v3.y - 0.7f) && (vec2.y <= v3.y + 0.7f))
                {
                    nowTouching[1] = i;
                    tc[i] = t;
                    
                }
                //v3 = GameObject.Find("BOOB").transform.position;
                //if ((vec2.x >= v3.x - 0.7f) && (vec2.x <= v3.x + 0.7f) && (vec2.y >= v3.y - 0.7f) && (vec2.y <= v3.y + 0.7f))
                //{
                //    Debug.Log("今のタップは" + i + "本目で" + "ギミックボタン2を押しています");
                //    if (isBomb2)
                //    {
                //        isBomb2 = false;
                //        Debug.Log("爆弾置く");
                //        BombAria[0].SetActive(false);
                //        // 爆弾を置く
                //        Map.instance.BombAria(GameObject.Find("Player1").GetComponent<Player>().rot, MapKind.Player1);
                //    }
                //    else
                //    {
                //        isBomb2 = true;
                //        Debug.Log(BombAria);
                //        BombAria[0].SetActive(true);
                //        // 爆弾範囲をだす
                //        Map.instance.AriaSet(MapKind.Player1, MapKind.BombAria1);
                //    }
                //    nowTouching[2] = i;
                //    tc[i] = t;
                //}

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

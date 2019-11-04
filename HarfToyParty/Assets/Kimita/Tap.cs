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
    protected int[] nowTouching = new int[5];

    private void Start()
    {
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
                if ((vec2.x >= v3.x-0.5f) && (vec2.x <= v3.x + 0.5f)&& (vec2.y >= v3.y - 0.5f) && (vec2.y <= v3.y + 0.5f))
                {
                    Debug.Log("今のタップは" + i + "本目で" + "移動キーを押しています");
                    nowTouching[0] = i;
                }
                v3 = GameObject.Find("BombButton").transform.position;
                if ((vec2.x >= v3.x - 0.7f) && (vec2.x <= v3.x + 0.7f) && (vec2.y >= v3.y - 0.7f) && (vec2.y <= v3.y + 0.7f))
                {
                    Debug.Log("今のタップは" + i + "本目で" + "ギミックボタンを押しています");
                    GameObject.Find("Player1").GetComponent<Player>().BombAria();
                    nowTouching[1] = i;
                }
            }
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

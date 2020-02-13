using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartThrow : MonoBehaviour
{
    [SerializeField]
    GameObject throwingObj,drow_throwObj;
    
    public int x, y,g; //加える力と重力

    [SerializeField]
    Rigidbody2D rb2_main, rd2_drow;
    Vector2 pos;

    float Middle_x, Middle_y;
    float colorDown = 1.5f;

    bool colorflag = false;
    float Scale_x, Scale_y;
    Vector2 obj; //初期の大きさを記録
    Vector2 initial; //初期地点を記録
    Vector2 initial_scale; //大きさの変更値

    void Start()
    {
        //Middle_x = (throwingObj.transform.position.x + targetObj.transform.position.x) / 2;
        //Debug.Log(Middle_x);  
        obj = throwingObj.transform.localScale;
        initial = throwingObj.transform.position;
        initial_scale = throwingObj.transform.localScale;
        //initial = gameObject.transform.localScale;
    }    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            partThrow();
        }
        //StartCoroutine("stop");
        
        //透明度と大きさを下げる
        if (colorflag)
        {
            colorDown -= Time.deltaTime;
            throwingObj.GetComponent<Image>().color = new Color(255, 255, 255, colorDown);

            obj.x -= 0.002f;
            obj.y -= 0.002f;

            throwingObj.transform.localScale = obj;
        }
        //下がりきったらおしまい
        if (colorDown < 0)
        {
            x = 50;                                                                   //力の方向を初期化
            colorflag = !colorflag;                                                   //カラーを変更しないようにする
            throwingObj.GetComponent<Image>().color = new Color(255, 255, 255, 1.5f); //色を初期化
            throwingObj.transform.position = initial;                                 //位置を初期化
            throwingObj.transform.localScale = initial_scale;                         //大きさを初期化
            obj = initial_scale;                                                      //大きさを変更していた値を初期化
            rb2_main.gravityScale = -2;                                                    //重力を初期化
            rb2_main.constraints = RigidbodyConstraints2D.FreezeAll;                       //移動しないようにする
            colorDown = 1.5f;                                                         
            throwingObj.SetActive(false);                                             //飛ばすパーツを非表示
            Debug.Log("戻ったよ");
        }        
    }

    /// <summary>
    /// パーツを放物線を描いて飛ばす
    /// </summary>
    public void partThrow()
    {
        //rb2 = throwingObj.GetComponent<Rigidbody2D>();
        rb2_main.constraints = RigidbodyConstraints2D.None;
        rb2_main.gravityScale = -2;
        //パーツのカラーを徐々に薄くするトリガー
        colorflag = !colorflag;
        //放物線上を求める
        pos = new Vector2(x * 10 + 2 / (9.8f * 10 * 10), y * 10 + 2 / (9.8f * 10 * 10));
        //一度だけ力を加える
        rb2_main.AddForce(pos, ForceMode2D.Impulse);
        StartCoroutine("switching");        
    }

    //IEnumerator stop()
    //{
    //    yield return new WaitForSeconds(1.5f);

    //    partThrow();
    //}

    //力を加えたオブジェクトに重力をかける
    IEnumerator switching()
    {        
        yield return new WaitForSeconds(0.45f);

        rb2_main.gravityScale = g;
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int myNumber;
    Map map;
    public int rot = 2;
    private GameObject bomb;
    private Vector2 bombPos = new Vector2(1,1);
    bool isMove = false;
    private void Start()
    {

        map = Map.instance;
        myNumber = (gameObject.name == "Player1") ? (int)MapKind.Player1: (int)MapKind.Player2;
        int x, y;
        x = (gameObject.name == "Player1") ? 0 : 6;
        y = (gameObject.name == "Player1") ? 6 : 0;
        map.mapInt[y, x] = myNumber;
        transform.position = Map.instance.SpritePos[y][x];
        

        StartCoroutine(SetMoveButton());
    }
    IEnumerator SetMoveButton()
    {
        yield return new WaitForSeconds(0.3f);
        
        GameObject.Find("MoveButton").GetComponent<MoveButton>().MyPlayer = gameObject.GetComponent<Player>(); 

    }
    private void Update()
    {
        //if (bomb.activeSelf)
        //{
        //    switch (rot)
        //    {
        //        case 0:// 上
        //            bomb.transform.position = new Vector2(0, bombPos.y);
        //            break;
        //        case 1:// 下
        //            bomb.transform.position = new Vector2(0, -bombPos.y);
        //            break;
        //        case 2:// 右
        //            bomb.transform.position = new Vector2(bombPos.x,0);
        //            break;
        //        case 3:// 左
        //            bomb.transform.position = new Vector2(-bombPos.x, 0);
        //            break;
        //    }
        //}
        switch (rot)
        {
            case 0:// 上
                //transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case 1:// 下
                //transform.eulerAngles = new Vector3(0, 0, 270);
                break;
            case 2:// 右
                //transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case 3:// 左
                //transform.eulerAngles = new Vector3(0, 0, 180);
                break;
        }
    }

    public void Move(int x)
    {
        switch (x)
        {
            case 0:// 上
                rot = 0;
                Move((MapKind)myNumber, new Vector2(0, 1));
                
                break;
            case 1:// 下
                rot = 1;
                Move((MapKind)myNumber, new Vector2(0, -1));
                break;
            case 2:// 右
                rot = 2;
                Move((MapKind)myNumber, new Vector2(1, 0));
                break;
            case 3:// 左
                rot = 3;
                Move((MapKind)myNumber, new Vector2(-1, 0));
                break;

        }
    }

    public void Move(MapKind player, Vector2 vec2)
    {
        if (isMove) return;
        int max = 6, min = 0;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if (map.mapInt[j, i] == (int)player)
                {
                    // Debug
                    Debug.Log("X" + i + "Y" + j + "移動X" + vec2.x + "移動Y" + vec2.y);

                    if (!((i + (int)vec2.x <= max && i + (int)vec2.x >= min) && (j - (int)vec2.y <= max && j - (int)vec2.y >= min)))
                    {
                        Debug.Log("位置が悪いよ");
                        // WarpWallで移動
                        return;
                    }
                    if (map.mapInt[j - (int)vec2.y, i + (int)vec2.x] == (int)MapKind.Wall)
                    {
                        Debug.Log("壁があるよ");
                        return;
                    }
                    if (map.mapInt[j - (int)vec2.y, i + (int)vec2.x] >= (int)MapKind.Movewall0)
                    {
                        if(!map.MoveWalls[map.mapInt[j - (int)vec2.y, i + (int)vec2.x] - (int)MapKind.Movewall0].GetComponent<MoveWall>().MoveCheck(vec2, map.SpritePos))
                        {
                            return;
                        }
                        // 壁を押す
                    }
                }
            }
        }
        isMove = true;
        StartCoroutine(MoveAnim(1, player, vec2));
    }
    IEnumerator MoveAnim(float wait, MapKind player, Vector2 vec2)
    {
       
        float time = wait / Time.deltaTime;
        int x = -1, y = -1;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if (map.mapInt[j, i] == (int)player)
                {
                    x = i;
                    y = j;
                }
            }
        }
        while (wait >= 0)
        {
            wait -= Time.deltaTime;
            gameObject.transform.position += new Vector3((map.SpritePos[y - (int)vec2.y][x + (int)vec2.x].x - map.SpritePos[y][x].x) / time, (map.SpritePos[y - (int)vec2.y][x + (int)vec2.x].y - map.SpritePos[y][x].y) / time);
            //MapObject[(int)map].transform.position += new Vector3((MapObject[(int)map].transform.position.x-map.SpritePos[y - (int)vec2.y][x + (int)vec2.x].x) / time , (map.SpritePos[y - (int)vec2.y][x + (int)vec2.x].y - MapObject[(int)map].transform.position.y  ) / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.position = new Vector3(map.SpritePos[y - (int)vec2.y][x + (int)vec2.x].x, map.SpritePos[y - (int)vec2.y][x + (int)vec2.x].y);
        map.mapInt[y, x] = 0;
        map.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)player;
        isMove = false;
    }


}

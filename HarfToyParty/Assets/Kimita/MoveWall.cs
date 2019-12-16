﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MoveWall :MonoBehaviour
{

    private enum form
    {
        square = 0,
        Rectangle,
        cross,
        VerticalRect,
    }
    [SerializeField]
    private form myForm;

    [SerializeField]
    private MapKind MyWall;

    [SerializeField]
    public Vector2 XY;

    GameObject player;
    bool PosUpdateRequest = false;
    int[] warpx = new int[2];
    int[] warpy = new int[2];

    [SerializeField]
    GameObject WarpObject;

    private void Start()
    {
        player= GameObject.FindObjectOfType<SyvnPos>().gameObject;
        int x = (int)XY.x, y = (int)XY.y;
        switch ((int)myForm)
        {
            case 0:
                Map.instance.mapInt[y, x] = (int)MyWall;
                break;
            case 1:
                Map.instance.mapInt[y, x] = (int)MyWall;
                Map.instance.mapInt[y, x + 1] = (int)MyWall;
                break;
            case 2:
                Map.instance.mapInt[y, x] = (int)MyWall;
                Map.instance.mapInt[y + 1, x] = (int)MyWall;
                Map.instance.mapInt[y - 1, x] = (int)MyWall;
                Map.instance.mapInt[y, x + 1] = (int)MyWall;
                Map.instance.mapInt[y, x - 1] = (int)MyWall;
                break;
            case 3:
                Map.instance.mapInt[y, x] = (int)MyWall;
                Map.instance.mapInt[y + 1, x] = (int)MyWall;
                break;

        }
        for (int i = 0; i < 2; i++)
        {
            warpx[i] = (int)Map.instance.WarpPoint[i].x;
            warpy[i] = (int)Map.instance.WarpPoint[i].y;
        }
        WarpCheck();
    }

    private void FixedUpdate()
    {
        if (PosUpdateRequest)
        {
            player.GetComponent<SyvnPos>().UpdateMePosition(this.gameObject, this.transform.position, this.XY);
            
        }
    }

    /// <summary>
    /// ワープ近くにあるかチェック
    /// </summary>
    private bool WarpCheck()
    {
        // ワープの向きが縦だったら
        if (Map.instance.isWarpVertical)
        {
            switch ((int)myForm)
            {
                case 0:
                    for (int i = 0; i < 2; i++)
                    {
                        if (warpy[i] == (int)XY.y)
                        {
                            Debug.Log("ワープ発見");
                            WarpObject.SetActive(true);
                            return true;
                        }
                        else
                        {
                            WarpObject.SetActive(false);
                        }
                    }
                    break;

                case 2: break;
                default:
                    for (int i = 0; i < 2; i++)
                    {
                        if ((warpy[i] == (int)XY.y && warpx[i] == (int)XY.x) || (warpy[i] == (int)XY.y + 1 && warpx[i] == (int)XY.x))
                        {
                            Debug.Log("ワープ発見");
                            WarpObject.SetActive(true);
                            return true;
                        }
                        else
                        {
                            WarpObject.SetActive(false);
                        }
                    }
                    break;
            }
        }
        return false;
    }
    private bool WarpMoveCheck(Vector2 mov2)
    {
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if (Map.instance.mapInt[j, i] == (int)MyWall)
                {
                    
                    if ((j - (int)mov2.y > 6 || j - (int)mov2.y < 0)) {
                        if (-(int)mov2.y > 0) {
                            if (Map.instance.mapInt[warpy[0], warpx[0]] != (int)MapKind.YUKA &&
                                Map.instance.mapInt[warpy[0], warpx[0]] != (int)MyWall) return false;
                        }else if(-(int)mov2.y < 0)
                        {
                            if (Map.instance.mapInt[warpy[1], warpx[1]] != (int)MapKind.YUKA &&
                                Map.instance.mapInt[warpy[1], warpx[1]] != (int)MyWall) return false;
                        }
                        continue;
                    }
                    Debug.Log("ワープ移動できるかチェックするよ！--");
                    Debug.Log(Map.instance.mapInt[j - (int)mov2.y, i + (int)mov2.x]);
                    Debug.Log("ここまで--------");
                    if (Map.instance.mapInt[j - (int)mov2.y, i + (int)mov2.x] != (int)MapKind.YUKA && Map.instance.mapInt[j - (int)mov2.y, i + (int)mov2.x] != (int)MyWall) return false;
                }
            }
        }
        return true;
    }

    private void WarpMove(int form, Vector2 mov2)
    {
        // 形によって入れるわーぷが変わるため移動を変える
        switch (form)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                // 横移動だったら返す
                if (mov2.x != 0) return;

                if (mov2.y > 0)
                {
                    Debug.Log("WarpCheck↑");
                    StartCoroutine(WarpMoveAnim(1,Map.instance.mapInt, mov2,Map.instance.SpritePos));
                }
                else
                {
                    Debug.Log("WarpCheck↓");
                    StartCoroutine(WarpMoveAnim(1, Map.instance.mapInt, mov2, Map.instance.SpritePos));
                }
                break;
            default:
                break;
        }
    }
    IEnumerator WarpMoveAnim(float wait, int[,] mapInt, Vector2 vec2, List<List<Vector3>> vec3)
    {
        float time = wait / Time.deltaTime;
        int x = (int)XY.x, y = (int)XY.y;
        List<int> my = new List<int>();
        List<int> mx = new List<int>();
        switch ((int)myForm)
        {
            case 0:
                Map.instance.mapInt[y, x] = 0;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)MyWall;
                break;
            case 1:
                Map.instance.mapInt[y, x] = 0;
                Map.instance.mapInt[y, x + 1] = 0;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)MyWall;
                Map.instance.mapInt[y - (int)vec2.y, x + 1 + (int)vec2.x] = (int)MyWall;
                break;
            case 2:
                Map.instance.mapInt[y, x] = 0;
                Map.instance.mapInt[y + 1, x] = 0;
                Map.instance.mapInt[y - 1, x] = 0;
                Map.instance.mapInt[y, x + 1] = 0;
                Map.instance.mapInt[y, x - 1] = 0;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)MyWall;
                Map.instance.mapInt[y - (int)vec2.y + 1, x + (int)vec2.x] = (int)MyWall;
                Map.instance.mapInt[y - (int)vec2.y - 1, x + (int)vec2.x] = (int)MyWall;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x + 1] = (int)MyWall;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x - 1] = (int)MyWall;
                break;
            case 3:
                Debug.Log("WarpMove");
                for (int i = 0; i <= 6; i++)
                {
                    for (int j = 0; j <= 6; j++)
                    {
                        if(Map.instance.mapInt[j,i] == (int)MyWall)
                        {
                            my.Add(j);
                            mx.Add(i);
                            Map.instance.mapInt[j, i] = 0;
                        }
                    }
                }
                
                for(int i = 0; i < my.Count; i++)
                {
                    Debug.Log("Count"+my.Count);
                    if (my[i] - (int)vec2.y == 7) { Debug.Log("ワープの場所X" + warpx[0]+ "Y" + warpy[0]); my[i] = warpy[0]-1; mx[i] = warpx[0]; }
                    else if (my[i] - (int)vec2.y == 0) { my[i] = warpy[1]+1; mx[i] = warpx[1]; }
                    Map.instance.mapInt[my[i] - (int)vec2.y, mx[i]] = (int)MyWall;
                }
                break;
        }
        while (wait >= 0)
        {
            wait -= Time.deltaTime;
            gameObject.transform.position += new Vector3((vec3[y - (int)vec2.y][x + (int)vec2.x].x - vec3[y][x].x) / time, (vec3[y - (int)vec2.y][x + (int)vec2.x].y - vec3[y][x].y) / time);

            yield return new WaitForSeconds(Time.deltaTime);
        }
        XY.x += vec2.x;
        XY.y -= vec2.y;
        Debug.Log("X:" + XY.x + "Y:" + XY.y);
        gameObject.transform.position = new Vector3(vec3[y - (int)vec2.y][x + (int)vec2.x].x, vec3[y - (int)vec2.y][x + (int)vec2.x].y);
        PosUpdateRequest = true;
        yield return new WaitForSeconds(0.1f);
        PosUpdateRequest = false;
    }

    /// <summary>
    /// 移動できるか
    /// </summary>
    /// <returns></returns>
    public bool MoveCheck(Vector2 mov2, List<List<Vector3>> vec3)
    {
        int[,] map = Map.instance.mapInt;
        bool isMove;
        int x = 0,y = 0;
        switch ((int)myForm)
        {
            case 0:
                if (XY.x + mov2.x >= 6 || XY.x + mov2.x < 0) return false;
                if (XY.y - mov2.y >= 6 || XY.y - mov2.y < 0) return false;
                if (map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] == (int)MapKind.Bomb || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0) return false;
                break;
            case 1:
                x = 0;
                Debug.Log("今X" + XY.x + "後" + mov2.x);
                Debug.Log("今Y" + XY.y+"後"+mov2.y);
                if (XY.x + 1 + mov2.x > 6 || XY.x + mov2.x < 0) return false;
                if (XY.y - mov2.y > 6 || XY.y - mov2.y < 0) return false;
                if (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb|| (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)) return false;
                x = 1;
                if (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)) return false;
                break;
            case 2:
                if (XY.x + 1 + mov2.x > 6 || XY.x - 1 + mov2.x < 0) return false;
                if (XY.y + 1 - mov2.y > 6 || XY.y - 1 - mov2.y < 0) return false;
                
                x = -1; y = 0;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)) return false;
                x = 1; y = 0;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)) return false;
                x = 0; y = -1;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)) return false;
                x = 0; y = 1;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0&& map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)) return false;
                //Map.instance.mapInt[y, x] = (int)MyWall;
                //Map.instance.mapInt[y + 1, x] = (int)MyWall;
                //Map.instance.mapInt[y - 1, x] = (int)MyWall;
                //Map.instance.mapInt[y, x + 1] = (int)MyWall;
                //Map.instance.mapInt[y, x - 1] = (int)MyWall;
                break;
            case 3:
                y = 0;
                if (WarpCheck())
                {
                    if (WarpMoveCheck(mov2))
                    {
                        WarpMove((int)myForm, mov2);
                        return true;
                    }
                }
                    if (XY.x + mov2.x >= 6 || XY.x + mov2.x < 0) return false;
                if (XY.y + 1 - mov2.y > 6 || XY.y - mov2.y < 0) return false;
                Debug.Log((MapKind)map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x]);
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall) return false;
                //if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)) return false;
                y = 1;
                Debug.Log((MapKind)map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x]);
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall) return false;
                //if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)) return false;
                break;
        }
        StartCoroutine(WallMoveAnim(1, map, mov2, vec3));
        return true;
    }

    private void WallMove()
    {
        
    }
    IEnumerator WallMoveAnim(float wait, int[,] mapInt, Vector2 vec2, List<List<Vector3>> vec3)
    {
        float time = wait / Time.deltaTime;
        int x = (int)XY.x, y = (int)XY.y;
        switch ((int)myForm)
        {
            case 0:
                Map.instance.mapInt[y, x] = 0;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)MyWall;
                break;
            case 1:
                Map.instance.mapInt[y, x] = 0;
                Map.instance.mapInt[y, x+1] = 0;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)MyWall;
                Map.instance.mapInt[y - (int)vec2.y, x + 1 +(int)vec2.x] = (int)MyWall;
                break;
            case 2:
                Map.instance.mapInt[y, x] = 0;
                Map.instance.mapInt[y + 1, x] = 0;
                Map.instance.mapInt[y - 1, x] = 0;
                Map.instance.mapInt[y, x + 1] = 0;
                Map.instance.mapInt[y, x - 1] = 0;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)MyWall;
                Map.instance.mapInt[y - (int)vec2.y + 1, x + (int)vec2.x] = (int)MyWall;
                Map.instance.mapInt[y - (int)vec2.y - 1, x + (int)vec2.x] = (int)MyWall;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x + 1] = (int)MyWall;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x - 1] = (int)MyWall;
                break;
            case 3:
                Map.instance.mapInt[y, x] = 0;
                Map.instance.mapInt[y + 1, x] = 0;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)MyWall;
                Map.instance.mapInt[y + 1 - (int)vec2.y, x + (int)vec2.x] = (int)MyWall;
                break;
        }
        while (wait >= 0)
        {
            wait -= Time.deltaTime;
            gameObject.transform.position += new Vector3((vec3[y - (int)vec2.y][x + (int)vec2.x].x - vec3[y][x].x) / time, (vec3[y - (int)vec2.y][x + (int)vec2.x].y - vec3[y][x].y) / time);
            
            yield return new WaitForSeconds(Time.deltaTime);
        }
        XY.x += vec2.x;
        XY.y -= vec2.y;
        Debug.Log("X:" + XY.x + "Y:" + XY.y);
        gameObject.transform.position = new Vector3(vec3[y - (int)vec2.y][x + (int)vec2.x].x, vec3[y - (int)vec2.y][x + (int)vec2.x].y);
        PosUpdateRequest = true;
        yield return new WaitForSeconds(0.1f);
        PosUpdateRequest = false;
    }

}

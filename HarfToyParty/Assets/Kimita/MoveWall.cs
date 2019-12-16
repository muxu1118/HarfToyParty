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
                    StartCoroutine(WarpMoveAnim(1,Map.instance.mapInt, mov2,Map.instance.SpritePos));
                }
                else
                {

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
                Map.instance.mapInt[y + 1, x] = 0;
                Map.instance.mapInt[y, x] = 0;
                int my = (y + 1 - (int)vec2.y);
                int mx = x + (int)vec2.x;
                if (my == 7) { my = warpy[0]; mx = warpx[0]; }
                if (my == 0) { my = warpy[1]; mx = warpx[1]; }
                Map.instance.mapInt[my, mx] = (int)MyWall;
                my = (y - (int)vec2.y);
                if (my == 7) { my = warpy[0]; mx = warpx[0]; }
                if (my == 0) { my = warpy[1]; mx = warpx[1]; }
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)MyWall;
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
                Debug.Log("縦X" + (XY.x + mov2.x));
                Debug.Log("縦Y" + (mov2.y));
                if (XY.x + mov2.x >= 6 || XY.x + mov2.x < 0) return false;
                if (XY.y + 1 - mov2.y > 6 || XY.y - mov2.y < 0) return false;
                Debug.Log((MapKind)map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x]);
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)) return false;
                y = 1;
                Debug.Log((MapKind)map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x]);
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)) return false;
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

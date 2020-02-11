using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

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
    int[] Hwarpx = new int[2];
    int[] Hwarpy = new int[2];
    [SerializeField]
    GameObject WarpObject;
    [SerializeField]
    GameObject SPRObject;
    bool isMove;
    public bool grab;

    private void Start()
    {
       // player= GameObject.FindObjectOfType<SyvnPos>().gameObject;
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
            Hwarpx[i] = (int)Map.instance.HWarpPoint[i].x;
            Hwarpy[i] = (int)Map.instance.HWarpPoint[i].y;
        }
        isMove = false;
        HWarpCheck();
        WarpCheck();
        grab = false;
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
                        if (warpy[i] == (int)XY.y && warpx[i] == (int)XY.x)
                        {
                            Debug.Log("ワープ発見");
                            WarpObject.SetActive(true);
                            WarpObject.transform.localPosition = Map.instance.warpPos[0, (i == 0) ? 1 : 0];
                            return true;
                        }
                        else
                        {
                            WarpObject.SetActive(false);
                        }
                    }
                    break;
                case 1: break;
                case 2: break;
                default:
                    for (int i = 0; i < 2; i++)
                    {
                        Debug.Log("ワープY:" + warpy[i] + "ブロックY" + XY.y);
                        Debug.Log("ワープX:" + warpx[i] + "ブロックX" + XY.x);
                        if ((warpy[i] == (int)XY.y && warpx[i] == (int)XY.x) || (warpy[i] == (int)XY.y + 1 && warpx[i] == (int)XY.x) || (warpy[i] == (int)XY.y - 1 && warpx[i] == (int)XY.x))
                        {
                            Debug.Log("ワープ発見");
                            WarpObject.SetActive(true);
                            //WarpObject.transform.localPosition = Map.instance.warpPos[3, (i == 0) ? 1 : 0];
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
    private bool HWarpCheck()
    {
        if (Map.instance.isWarpHorizontal)
        {
            switch ((int)myForm)
            {
                case 0:
                    for (int i = 0; i < 2; i++)
                    {
                        if (Hwarpy[i] == (int)XY.y && Hwarpx[i] == (int)XY.x)
                        {
                            Debug.Log("ワープ発見");
                            WarpObject.SetActive(true);
                            WarpObject.transform.localPosition = Map.instance.warpPos[0, (i == 0) ? 1 : 0];
                            return true;
                        }
                        else
                        {
                            WarpObject.SetActive(false);
                        }
                    }
                    break;

                case 2: break;
                case 3: break;
                default:
                    for (int i = 0; i < 2; i++)
                    {
                        Debug.Log("ワープY:" + Hwarpy[i] + "ブロックY" + XY.y);
                        Debug.Log("ワープX:" + Hwarpx[i] + "ブロックX" + XY.x);
                        if ((Hwarpy[i] == (int)XY.y && Hwarpx[i] == (int)XY.x) || (Hwarpy[i] == (int)XY.y && Hwarpx[i] == (int)XY.x+1)|| (Hwarpy[i] == (int)XY.y && Hwarpx[i] == (int)XY.x - 1))
                        {
                            Debug.Log("ワープ発見");
                            WarpObject.SetActive(true);
                            WarpObject.transform.localPosition = Map.instance.warpPos[1,(i == 0)?1:0];
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
        bool isChange = false, Up = false;
        // ワープの向きが縦だったら
        if (Map.instance.isWarpVertical)
        {
            switch ((int)myForm)
            {
                case 0:
                    // 横移動だったら返す
                    if (mov2.x != 0) return false;
                    break;
                case 1: return false;
                case 2: break;
                default:
                    // 横移動だったら返す
                    if (mov2.x != 0) return false;
                    break;
            }
        }
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
                            if (XY.y - (int)mov2.y > 6)
                            {
                                Vector3 temp = Vector3.zero;
                                temp = SPRObject.transform.position;
                                transform.position = new Vector3(Map.instance.SpritePos[warpy[0]][warpx[0]].x, Map.instance.SpritePos[warpy[0]][warpx[0]].y - (Map.instance.SpritePos[warpy[0] + 1][warpx[0]].y - Map.instance.SpritePos[warpy[0]][warpx[0]].y ), 0);
                                WarpObject.transform.position = temp;
                                Debug.Log("Change");
                                Up = false;
                                isChange = true;
                            }
                        }else if(-(int)mov2.y < 0)
                        {
                            if (Map.instance.mapInt[warpy[1], warpx[1]] != (int)MapKind.YUKA &&
                                Map.instance.mapInt[warpy[1], warpx[1]] != (int)MyWall) return false;
                            if (XY.y - (int)mov2.y < 0)
                            {
                                Vector3 temp = Vector3.zero;
                                temp = SPRObject.transform.position;
                                transform.position = new Vector3(Map.instance.SpritePos[warpy[1]][warpx[1]].x, Map.instance.SpritePos[warpy[1]][warpx[1]].y + (Map.instance.SpritePos[1][0].y - Map.instance.SpritePos[0][0].y), 0);
                                WarpObject.transform.position = temp;
                                Debug.Log("Change");
                                Up = true;
                                isChange = true;
                            }
                        }
                        continue;
                    }
                    if (Map.instance.mapInt[j - (int)mov2.y, i + (int)mov2.x] != (int)MapKind.YUKA && Map.instance.mapInt[j - (int)mov2.y, i + (int)mov2.x] != (int)MyWall) return false;
                }
            }
        }
        if (isChange)
        {
            XY = (Up)? new Vector2(warpx[1], warpy[1]) : new Vector2(warpx[0], warpy[0]);
        }else
        {
            XY.x += mov2.x;
            XY.y -= mov2.y;
        }
        return true;
    }
    private bool HWarpMoveCheck(Vector2 mov2)
    {
        bool isChange = false, Up = false;
        // ワープの向きが横だったら
        if (Map.instance.isWarpHorizontal)
        {
            switch ((int)myForm)
            {
                case 0:
                    // 縦移動だったら返す
                    if (mov2.y != 0) return false;
                    break;
                case 3: return false;
                case 2: break;
                default:
                    // 縦移動だったら返す
                    if (mov2.y != 0) return false;
                    break;
            }
        }
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if (Map.instance.mapInt[j, i] == (int)MyWall)
                {
                    if ((i + (int)mov2.x > 6 || i + (int)mov2.x < 0))
                    {
                        // 右の時
                        if ((int)mov2.x > 0)
                        {
                            if (Map.instance.mapInt[Hwarpy[0], Hwarpx[0]] != (int)MapKind.YUKA &&
                                Map.instance.mapInt[Hwarpy[0], Hwarpx[0]] != (int)MyWall) return false;
                            if (XY.x + (int)mov2.x > 6)
                            {
                                Vector3 temp = Vector3.zero;
                                temp = SPRObject.transform.position;
                                transform.position = new Vector3(Map.instance.SpritePos[Hwarpy[0]][Hwarpx[0]].x + (Map.instance.SpritePos[Hwarpy[0]][Hwarpx[0]].x - Map.instance.SpritePos[Hwarpy[0]][Hwarpx[0] + 1].x) , Map.instance.SpritePos[Hwarpy[0]][Hwarpx[0]].y, 0);
                                WarpObject.transform.position = temp;
                                Debug.Log("Change");
                                Up = true;
                                isChange = true;
                            }
                        }
                        else if ((int)mov2.x < 0)
                        {
                            if (Map.instance.mapInt[Hwarpy[1], Hwarpx[1]] != (int)MapKind.YUKA &&
                                Map.instance.mapInt[Hwarpy[1], Hwarpx[1]] != (int)MyWall) return false;
                            Debug.Log("YOYO" + (i + (int)mov2.x));
                            if (XY.x + (int)mov2.x < 0)
                            {
                                Vector3 temp = Vector3.zero;
                                temp = SPRObject.transform.position;
                                transform.position = new Vector3(Map.instance.SpritePos[Hwarpy[1]][Hwarpx[1]].x - (Map.instance.SpritePos[0][0].x - Map.instance.SpritePos[0][1].x), Map.instance.SpritePos[Hwarpy[1]][Hwarpx[1]].y , 0);
                                WarpObject.transform.position = temp;
                                Debug.Log("Change");
                                Up = false;
                                isChange = true;
                            }
                        }
                        continue;
                    }
                    if (Map.instance.mapInt[j - (int)mov2.y, i + (int)mov2.x] != (int)MapKind.YUKA && Map.instance.mapInt[j - (int)mov2.y, i + (int)mov2.x] != (int)MyWall) return false;
                }
            }
        }
        if (isChange)
        {
            XY = (Up) ? new Vector2(Hwarpx[0], Hwarpy[0]) : new Vector2(Hwarpx[1], Hwarpy[1]);
        }
        else
        {
            XY.x += mov2.x;
            XY.y -= mov2.y;
        }
        return true;
    }
    private void WarpMove(int form, Vector2 mov2)
    {
        // 形によって入れるわーぷが変わるため移動を変える
        switch (form)
        {
            case 0:
                StartCoroutine(WarpMoveAnim(1, Map.instance.mapInt, mov2, Map.instance.SpritePos));
                return;
            case 1:
                if (mov2.y != 0) return;

                if (mov2.x > 0)
                {
                    StartCoroutine(WarpMoveAnim(1, Map.instance.mapInt, mov2, Map.instance.SpritePos));
                    return;
                }
                else
                {
                    StartCoroutine(WarpMoveAnim(1, Map.instance.mapInt, mov2, Map.instance.SpritePos));
                    return;
                }
            case 2:
                break;
            case 3:
                // 横移動だったら返す
                if (mov2.x != 0) return;

                if (mov2.y > 0)
                {
                    StartCoroutine(WarpMoveAnim(1,Map.instance.mapInt, mov2,Map.instance.SpritePos));
                    return;
                }
                else
                {
                    StartCoroutine(WarpMoveAnim(1, Map.instance.mapInt, mov2, Map.instance.SpritePos));
                    return;
                }
            default:
                break;
        }
        return;
    }
    IEnumerator WarpMoveAnim(float wait, int[,] mapInt, Vector2 vec2, List<List<Vector3>> vec3)
    {
        float time = wait/Time.deltaTime;
        int x = (int)XY.x, y = (int)XY.y;
        List<int> my = new List<int>();
        List<int> mx = new List<int>();
        Vector3 vec = transform.position;
        isMove = true;
        switch ((int)myForm)
        {
            case 0:
                Map.instance.mapInt[y, x] = 0;
                if (Map.instance.isWarpVertical)
                {
                    Map.instance.mapInt[(int)XY.y, (int)XY.x] = (int)MyWall;
                }
                break;
            case 1:
                for (int i = 0; i <= 6; i++)
                {
                    for (int j = 0; j <= 6; j++)
                    {
                        if (Map.instance.mapInt[j, i] == (int)MyWall)
                        {
                            my.Add(j);
                            mx.Add(i);
                            Map.instance.mapInt[j, i] = 0;
                        }
                    }
                }
                HWarpCheck();
                for (int i = 0; i < my.Count; i++)
                {
                    if (mx[i] + (int)vec2.x == 7) {
                        my[i] = Hwarpy[0];
                        mx[i] = Hwarpx[0]-1;
                    }
                    else if (mx[i] + (int)vec2.x == -1) {
                        my[i] = Hwarpy[1];
                        mx[i] = Hwarpx[1]+1;
                    }
                    Map.instance.mapInt[my[i], mx[i] + (int)vec2.x] = (int)MyWall;
                }
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
                WarpCheck();
                for (int i = 0; i < my.Count; i++)
                {
                    if (my[i] - (int)vec2.y == 7) {
                        my[i] = warpy[0]-1;
                        mx[i] = warpx[0];
                    }
                    else if (my[i] - (int)vec2.y == -1) {
                        my[i] = warpy[1]+1;
                        mx[i] = warpx[1];
                    }
                    Map.instance.mapInt[my[i] - (int)vec2.y, mx[i]] = (int)MyWall;
                }
                break;
        }

        while (wait >= 0)
        {
            wait -= Time.deltaTime;
            gameObject.transform.position += new Vector3((vec3[y][x].x-vec.x)/time, vec3[y][x].y - vec.y);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        isMove = false;
        WarpCheck();
        HWarpCheck();
        gameObject.transform.position = new Vector3(vec3[y][x].x, vec3[y][x].y);
    }

    /// <summary>
    /// 移動できるか
    /// </summary>
    /// <returns></returns>
    public bool MoveCheck(Vector2 mov2, List<List<Vector3>> vec3)
    {
        Debug.Log(isMove);
        int[,] map = Map.instance.mapInt;
        if (isMove) return false;
        if (grab) return false;
        int x = 0,y = 0;
        switch ((int)myForm)
        {
            case 0:
                
                if (WarpCheck())
                {
                    if (WarpMoveCheck(mov2))
                    {
                        isMove = true;
                        WarpMove((int)myForm, mov2);
                        return true;
                    }
                }
                if (HWarpCheck())
                {
                    if (HWarpMoveCheck(mov2))
                    {
                        isMove = true;
                        WarpMove((int)myForm, mov2);
                        return true;
                    }
                }
                if (XY.x + mov2.x > 6 || XY.x + mov2.x < 0)
                {
                    isMove = false;
                    return false;
                }
                if (XY.y - mov2.y > 6 || XY.y - mov2.y < 0) {
                    isMove = false;
                    return false;
                }
                if (map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] != (int)MyWall)
                {
                    isMove = false;
                    return false;
                }
                break;
            case 1:
                if (HWarpCheck())
                {
                    if (HWarpMoveCheck(mov2))
                    {
                        isMove = true;
                        WarpMove((int)myForm, mov2);
                        return true;
                    }
                }
                x = 0;
                if (XY.x + 1 + mov2.x > 6 || XY.x + mov2.x < 0)
                {
                    isMove = false;
                    return false;
                }
                if (XY.y - mov2.y > 6 || XY.y - mov2.y < 0)
                {
                    isMove = false;
                    return false;
                }

                if (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall|| (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.BreakWall1 && map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] <= (int)MapKind.BreakWall6)|| map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb|| (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall))
                {
                    isMove = false;
                    return false;
                }
                x = 1;
                if (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall|| (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.BreakWall1 && map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] <= (int)MapKind.BreakWall6)|| map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall))
                {
                    isMove = false;
                    return false;
                }
                break;
            case 2:
                if (XY.x + 1 + mov2.x > 6 || XY.x - 1 + mov2.x < 0)
                {
                    isMove = false;
                    return false;
                }
                if (XY.y + 1 - mov2.y > 6 || XY.y - 1 - mov2.y < 0)
                {
                    isMove = false;
                    return false;
                }

                x = -1; y = 0;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.BreakWall1 && map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] <= (int)MapKind.BreakWall6) || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall))
                {
                    isMove = false;
                    return false;
                }
                x = 1; y = 0;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.BreakWall1 && map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] <= (int)MapKind.BreakWall6) || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall))
                {
                    isMove = false;
                    return false;
                }
                x = 0; y = -1;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.BreakWall1 && map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] <= (int)MapKind.BreakWall6) || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0 && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall))
                {
                    isMove = false;
                    return false;
                }
                x = 0; y = 1;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.BreakWall1 && map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] <= (int)MapKind.BreakWall6) || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb || (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] >= (int)MapKind.Movewall0&& map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall))
                {
                    isMove = false;
                    return false;
                }
                break;
            case 3:
                y = 0;
                if (WarpCheck())
                {
                    if (WarpMoveCheck(mov2))
                    {
                        isMove = true;
                        WarpMove((int)myForm, mov2);
                        return true;
                    }
                }
                if (XY.x + mov2.x >= 6 || XY.x + mov2.x < 0)
                {
                    isMove = false;
                    return false;
                }
                if (XY.y + 1 - mov2.y > 6 || XY.y - mov2.y < 0)
                {
                    isMove = false;
                    return false;
                }
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)
                {
                    isMove = false;
                    return false;
                }
                y = 1;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall)
                {
                    isMove = false;
                    return false;
                }
                break;
        }
        isMove = true;
        StartCoroutine(WallMoveAnim(1, map, mov2, vec3));
        return true;
    }
    IEnumerator WallMoveAnim(float wait, int[,] mapInt, Vector2 vec2, List<List<Vector3>> vec3)
    {
        float time = 0;
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
        XY.x += vec2.x;
        XY.y -= vec2.y;
        WarpCheck();
        while (wait >= time)
        {
            time += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(vec3[y][x], vec3[y - (int)vec2.y][x + (int)vec2.x], time / wait);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        gameObject.transform.position = new Vector3(vec3[y - (int)vec2.y][x + (int)vec2.x].x, vec3[y - (int)vec2.y][x + (int)vec2.x].y);
        isMove = false;
        WarpCheck();
        HWarpCheck();
    }

    public bool PullMoveCheck(int Player,Vector2 mov2, List<List<Vector3>> vec3)
    {
        int[,] map = Map.instance.mapInt;
        int x = 0, y = 0;
        switch ((int)myForm)
        {
            case 0:
                if (XY.x + mov2.x > 6 || XY.x + mov2.x < 0) return false;
                if (XY.y - mov2.y > 6 || XY.y - mov2.y < 0) return false;

                if (map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] != (int)MyWall && map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] != (int)Player) return false;
                break;
            case 1:
                
                x = 0;
                if (XY.x + 1 + mov2.x > 6 || XY.x + mov2.x < 0) return false;
                if (XY.y - mov2.y > 6 || XY.y - mov2.y < 0) return false;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)Player) return false;
                x = 1;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)Player) return false;
                break;
            case 2:
                if (XY.x + 1 + mov2.x > 6 || XY.x - 1 + mov2.x < 0) return false;
                if (XY.y + 1 - mov2.y > 6 || XY.y - 1 - mov2.y < 0) return false;
                x = 0; y = 1;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != Player) return false; 
                x = -1; y = 0;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != Player) return false;
                x = 1; y = 0;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != Player) return false;
                x = 0; y = -1;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != Player) return false;
                
                break;
            case 3:
                
                y = 0;
                if (XY.x + mov2.x >= 6 || XY.x + mov2.x < 0) return false;
                if (XY.y + 1 - mov2.y > 6 || XY.y - mov2.y < 0) return false;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)Player) return false;
                y = 1;
                if (map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MapKind.YUKA && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)MyWall && map[(int)XY.y + y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] != (int)Player) return false;
                break;
        }
        return true;
    }
    public void PullWall(Vector2 mov2)
    {
        isMove = true;
        StartCoroutine(WallMoveAnim(1, Map.instance.mapInt, mov2, Map.instance.SpritePos));
    }
}

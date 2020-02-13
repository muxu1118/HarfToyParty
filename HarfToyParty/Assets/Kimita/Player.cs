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
    [SerializeField]
    bool _isMove = false;
    bool isMove {
        get {
            return _isMove;
        }
        set {
            _isMove = value;
        }
    }
    System.Action _callback = null;
    [SerializeField]
    Sprite[] PlayerSprite = new Sprite[3]; //プレイヤーの画像
    SpriteRenderer spRen;
    public int[] SpawnXY = new int[2];

    private void Start()
    {
        myNumber = (gameObject.name == "Player1") ? (int)MapKind.Player1: (int)MapKind.Player2;
        PlayerMapWrite();
        spRen = GetComponent<SpriteRenderer>();
    }
    IEnumerator SetMoveButton()
    {
        yield return new WaitForSeconds(0.3f);

    }
    public void PlayerMapWrite()
    {
        map = Map.instance;
        map.mapInt[SpawnXY[0], SpawnXY[1]] = myNumber;
        transform.position = Map.instance.SpritePos[SpawnXY[0]][SpawnXY[1]];
    }

    private void Update()
    {
        switch (rot)
        {
            case 0:// 上
                spRen.sprite = PlayerSprite[1];
                break;
            case 1:// 下
                spRen.sprite = PlayerSprite[2];
                break;
            case 2:// 右
                spRen.sprite = PlayerSprite[0];
                transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case 3:// 左
                spRen.sprite = PlayerSprite[0];
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
        }
    }

    public void SetActionCallback(System.Action callback)
    {
        _callback = callback;
    }
    public void PullMove(int x,int wall)
    {

        if (isMove) return;
        switch (x)
        {
            case 0:// 上
                rot = 0;
                isMove = true;
                PullMove((MapKind)myNumber, new Vector2(0, 1),wall);
                break;
            case 1:// 下
                rot = 1;
                isMove = true;
                PullMove((MapKind)myNumber, new Vector2(0, -1), wall);
                break;
            case 2:// 右
                rot = 2;
                isMove = true;
                PullMove((MapKind)myNumber, new Vector2(1, 0), wall);
                break;
            case 3:// 左
                rot = 3;
                isMove = true;
                PullMove((MapKind)myNumber, new Vector2(-1, 0), wall);
                break;

        }
    }
    public void Move(int x)
    {

        if (isMove) return;
        switch (x)
        {
            case 0:// 上
                rot = 0;
                isMove = true;
                Move((MapKind)myNumber, new Vector2(0, 1));
                break;
            case 1:// 下
                rot = 1;
                isMove = true;
                Move((MapKind)myNumber, new Vector2(0, -1));
                break;
            case 2:// 右
                rot = 2;
                isMove = true;
                Move((MapKind)myNumber, new Vector2(1, 0));
                break;
            case 3:// 左
                rot = 3;
                isMove = true;
                Move((MapKind)myNumber, new Vector2(-1, 0));
                break;

        }
    }

    public void PullMove(MapKind player, Vector2 vec2,int wall)
    {
        int max = 6, min = 0;
        float speed = 0.45f;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if (map.mapInt[j, i] == (int)player)
                {
                    bool isWarp = false;
                    // Debug
                    if (!((i + (int)vec2.x <= max && i + (int)vec2.x >= min) && (j - (int)vec2.y <= max && j - (int)vec2.y >= min)) && !isWarp)
                    {
                        // 範囲外
                        isMove = false;
                        PlayerDoNotMove();
                        return;
                    }
                    if ((wall != map.mapInt[j - (int)vec2.y, i + (int)vec2.x]) &&map.mapInt[j - (int)vec2.y, i + (int)vec2.x] >= (int)MapKind.Movewall0 && map.mapInt[j - (int)vec2.y, i + (int)vec2.x] <= (int)MapKind.Movewall10)
                    {
                        isMove = false;
                        return;
                        // 移動壁を押す
                        if (!map.MoveWalls[map.mapInt[j - (int)vec2.y, i + (int)vec2.x] - (int)MapKind.Movewall0].GetComponent<MoveWall>().MoveCheck(vec2, map.SpritePos))
                        {
                            isMove = false;
                            return;
                        }
                        speed = 0.8f;
                        isMove = true;
                        StartCoroutine(MoveAnim(speed, player, vec2));
                        return;
                    }
                    if (map.mapInt[j - (int)vec2.y, i + (int)vec2.x] == (int)MapKind.Wall || (map.mapInt[j - (int)vec2.y, i + (int)vec2.x] >= (int)MapKind.BreakWall1 && map.mapInt[j - (int)vec2.y, i + (int)vec2.x] <= (int)MapKind.BreakWall10))
                    {
                        // 移動できない壁
                        isMove = false;
                        PlayerDoNotMove();
                        return;
                    }
                    if (map.mapInt[j - (int)vec2.y, i + (int)vec2.x] == (int)MapKind.Player1 || map.mapInt[j - (int)vec2.y, i + (int)vec2.x] == (int)MapKind.Player2)
                    {
                        // 他プレイヤーがいる
                        isMove = false;
                        PlayerDoNotMove();
                        return;
                    }
                    if (PullWallCheck(vec2, wall))
                    {
                        // 引き動作
                        isMove = true;
                        speed = 0.8f;
                        StartCoroutine(MoveAnim(speed, player, vec2));
                        map.MoveWalls[wall - (int)MapKind.Movewall0].GetComponent<MoveWall>().PullWall(vec2);
                        return;
                    }
                    else
                    {
                        isMove = false;
                        return;
                    }
                }
            }
        }
    }
    private bool PullWallCheck(Vector2 vector2,int wall)
    {
        return map.MoveWalls[wall-(int)MapKind.Movewall0].GetComponent<MoveWall>().PullMoveCheck(myNumber,vector2, map.SpritePos);
    }
    public void Move(MapKind player, Vector2 vec2)
    {
        int max = 6, min = 0;
        float speed = 0.45f; ;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if (map.mapInt[j, i] == (int)player)
                {
                    bool isWarp=false;
                    if (!((i + (int)vec2.x <= max && i + (int)vec2.x >= min) && (j - (int)vec2.y <= max && j - (int)vec2.y >= min)) && !isWarp)
                    {
                        //マップの端
                        isMove = false;
                        PlayerDoNotMove();
                        return;
                    }
                    if (map.mapInt[j - (int)vec2.y, i + (int)vec2.x] >= (int)MapKind.Movewall0 && map.mapInt[j - (int)vec2.y, i + (int)vec2.x] <= (int)MapKind.Movewall10)
                    {
                        // 壁を押す
                        if (!map.MoveWalls[map.mapInt[j - (int)vec2.y, i + (int)vec2.x] - (int)MapKind.Movewall0].GetComponent<MoveWall>().MoveCheck(vec2, map.SpritePos))
                        {
                            isMove = false;
                            return;
                        }
                        speed = 0.8f;

                    }
                    if (map.mapInt[j - (int)vec2.y, i + (int)vec2.x] == (int)MapKind.Wall||(map.mapInt[j - (int)vec2.y, i + (int)vec2.x] >= (int)MapKind.BreakWall1&& map.mapInt[j - (int)vec2.y, i + (int)vec2.x] <= (int)MapKind.BreakWall10))
                    {
                        //動けない壁がある
                        isMove = false;
                        PlayerDoNotMove();
                        return;
                    }
                    if (map.mapInt[j - (int)vec2.y, i + (int)vec2.x] == (int)MapKind.Player1 || map.mapInt[j - (int)vec2.y, i + (int)vec2.x] == (int)MapKind.Player2)
                    {
                        //他プレイヤーがいるところに移動しないように
                        isMove = false;
                        PlayerDoNotMove();
                        return;
                    }
                }
            }
        }
        isMove = true;
        // 移動
        StartCoroutine(MoveAnim(speed, player, vec2));
    }
    IEnumerator MoveAnim(float wait, MapKind player, Vector2 vec2)
    {
        float time = 0;
        int x = -1, y = -1;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if (map.mapInt[j, i] == (int)player)
                {
                    x = i;
                    y = j;
                    map.mapInt[y, x] = 0;
                    map.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)player;
                }
            }
        }
        while (wait >= time)
        {
            time += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(map.SpritePos[y][x], map.SpritePos[y - (int)vec2.y][x + (int)vec2.x], time / wait);
            //gameObject.transform.position += new Vector3((map.SpritePos[y - (int)vec2.y][x + (int)vec2.x].x - map.SpritePos[y][x].x) / time, (map.SpritePos[y - (int)vec2.y][x + (int)vec2.x].y - map.SpritePos[y][x].y) / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.position = new Vector3(map.SpritePos[y - (int)vec2.y][x + (int)vec2.x].x, map.SpritePos[y - (int)vec2.y][x + (int)vec2.x].y);
        isMove = false;
        map.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)player;
        map.updateMap = true;
        yield return new WaitForSeconds(0.1f);
        map.updateMap = false;
    }

    private void PlayerDoNotMove()
    {
        if (_callback != null)
            _callback();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public enum MapKind
{
    
    YUKA = 0,
    Bomb,
    Player1,
    Player2,
    BombAria1,
    BombAria2,
    Wall,
    Movewall0,
    Movewall1,
    Movewall2,
    Movewall3,
    Movewall4,
    Movewall5,
    Movewall6,
    Movewall7,
    Movewall8,
    Movewall9,
    Movewall10,
}


public class Map : NetworkBehaviour
{
    //singleton 始まり
    public static Map instance;
    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this as Map;
        }
    }
    protected void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    //singleton 終わり

    List<List<Vector3>> spritePos = new List<List<Vector3>>();// マップの位置
    List<List<GameObject>> mapObj = new List<List<GameObject>>();
    public int[,] mapInt = new int[7, 7];

    public List<MapKind> maps = new List<MapKind>();
    [SerializeField]
    private GameObject[] MapObject = new GameObject[System.Enum.GetNames(typeof(MapKind)).Length];
    bool isMove;
    private Vector2 BombPos = new Vector2();

    public List<List<Vector3>> SpritePos { get => spritePos; set => spritePos = value; }
    public Vector2 BombPos1 { get => BombPos; set => BombPos = value; }
    public List<GameObject> MoveWalls { get; set; } = new List<GameObject>();
    public bool updateMap=false;
   

    private void OnEnable()
    {

        // マップを探す
        foreach (Transform Y in gameObject.transform)
        {
            spritePos.Add(new List<Vector3>());
            mapObj.Add(new List<GameObject>());
            for (int i = 0; i <= 6; i++)
            {
                if (Y.name == "Y" + i)
                {
                    foreach (Transform X in Y.transform)
                    {
                        mapObj[i].Add(X.transform.gameObject);
                        spritePos[i].Add(X.transform.position);
                        mapInt[i, i] = 0;

                    }
                }
            }

        }
        StartCoroutine(moveWallAdd());
        for (int i = 0; i < System.Enum.GetNames(typeof(MapKind)).Length; i++)
        {
            maps.Add((MapKind)i);
        }

    }
    IEnumerator moveWallAdd()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (Transform movewall in GameObject.Find("MoveWalls").transform)
        {
            MoveWalls.Add(movewall.gameObject);
        }
    }
    private void Start()
    {



    }
    private void Update()
    {
        // Debug
        if (Input.GetKeyDown(KeyCode.A))
        {
            string str = "";
            for (int i = 0; i <= 6; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    if (mapInt[i, j] < 10) { str += " "; }
                    str += mapInt[i, j].ToString();
                }
                str += "\n";
            }
            Debug.Log(str);
        }
    }

    private void CheckMap()
    {
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                for (int l = 0; l < System.Enum.GetNames(typeof(MapKind)).Length; l++)
                {
                    if (mapInt[j, i] == (int)maps[l])
                    {
                        MapObject[(int)maps[l]].transform.position = spritePos[i][j];
                    }

                }
            }
        }
    }


    //public void Move(MapKind map, Vector2 vec2)
    //{
    //    if (isMove) return;
    //    int max = 6, min = 0;
    //    for (int i = 0; i <= 6; i++)
    //    {
    //        for (int j = 0; j <= 6; j++)
    //        {
    //            if (mapInt[j, i] == (int)map)
    //            {
    //                // Debug
    //                Debug.Log("X" + i + "Y" + j + "移動X" + vec2.x + "移動Y" + vec2.y);

    //                if (!((i + (int)vec2.x <= max && i + (int)vec2.x >= min) && (j - (int)vec2.y <= max && j - (int)vec2.y >= min)))
    //                {
    //                    Debug.Log("位置が悪いよ");
    //                    // WarpWallで移動
    //                    return;
    //                }
    //                if (mapInt[j - (int)vec2.y, i + (int)vec2.x] == (int)MapKind.Wall)
    //                {
    //                    Debug.Log("壁があるよ");
    //                    return;
    //                }
    //                if (mapInt[j - (int)vec2.y, i + (int)vec2.x] == (int)MapKind.Movewall)
    //                {
    //                    // 壁を押す
    //                    if (!MapObject[(int)MapKind.Movewall].GetComponent<MoveWall>().MoveCheck(vec2,spritePos)) return;

    //                }
    //            }
    //        }
    //    }
    //    isMove = true;
    //    StartCoroutine(MoveAnim(1, map, vec2));
    //}
    /// <summary>
    /// ボム範囲を設定
    /// </summary>
    /// <param name="r"></param>
    /// <param name="player"></param>
    public void BombAria(int r, MapKind player)
    {
        int y = 0, x = 0;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if ((int)player == mapInt[j, i])
                {
                    y = j; x = i;
                }

            }
        }
        MapKind booAria = (player == MapKind.Player1) ? MapKind.BombAria1 : MapKind.BombAria2;
        GameObject obj = Instantiate(MapObject[(int)MapKind.Bomb], MapObject[(int)booAria].transform.position, Quaternion.identity);
        obj.GetComponent<BomTest>().MyPosi = BombPos1;

    }
    //IEnumerator MoveAnim(float wait,MapKind map,Vector2 vec2)
    //{
    //    float time = wait / Time.deltaTime;
    //    int x=-1, y=-1;
    //    for (int i = 0; i <= 6; i++)
    //    {
    //        for (int j = 0; j <= 6; j++)
    //        {
    //            if (mapInt[j, i] == (int)map)
    //            {
    //                x = i;
    //                y = j;
    //            }
    //        }
    //    }
    //    while (wait >= 0)
    //    {
    //        wait -= Time.deltaTime;
    //        MapObject[(int)map].transform.position += new Vector3((spritePos[y - (int)vec2.y][x + (int)vec2.x].x - spritePos[y][x].x) / time, (spritePos[y - (int)vec2.y][x + (int)vec2.x].y-spritePos[y][x].y) / time);
    //        //MapObject[(int)map].transform.position += new Vector3((MapObject[(int)map].transform.position.x-spritePos[y - (int)vec2.y][x + (int)vec2.x].x) / time , (spritePos[y - (int)vec2.y][x + (int)vec2.x].y - MapObject[(int)map].transform.position.y  ) / time);
    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }
    //    MapObject[(int)map].transform.position = new Vector3(spritePos[y - (int)vec2.y][x + (int)vec2.x].x, spritePos[y - (int)vec2.y][x + (int)vec2.x].y);
    //    mapInt[y, x] = 0;
    //    mapInt[y - (int)vec2.y,x + (int)vec2.x] = (int)map;
    //    isMove = false;
    //}

    //public bool GetPartCheck(MapKind player,Vector2 xy)
    //{
    //    for (int i = 0; i <= 6; i++)
    //    {
    //        for (int j = 0; j <= 6; j++)
    //        {
    //            if (mapInt[j, i] == (int)MapKind.RedPart&&player == MapKind.Player1)
    //            {

    //                return true;
    //            }
    //            if (mapInt[j, i] == (int)MapKind.BluePart && player == MapKind.Player2)
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}

    public void AriaSet(MapKind player,MapKind Aria)
    {
        StartCoroutine(MapObject[(int)Aria].GetComponent<Bomb>().AriaSet(player,Aria,BombPos1));
    }
    public void PushMoveWall(Vector2 vec2,MapKind kind)
    {
        Debug.Log("押す壁"+((int)kind - (int)MapKind.Movewall0));
        if (!MoveWalls[(int)kind-(int)MapKind.Movewall0].GetComponent<MoveWall>().MoveCheck(vec2, spritePos)) return;
        return;

    }
}

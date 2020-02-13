using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;
public enum MapKind
{
    YUKA = 0,
    Bomb,
    Player1,
    Player2,
    BombAria1,
    BombAria2,
    Wall,
    BreakWall1,
    BreakWall2,
    BreakWall3,
    BreakWall4,
    BreakWall5,
    BreakWall6,
    BreakWall7,
    BreakWall8,
    BreakWall9,
    BreakWall10,
    PartRHand,
    PartRLeg,
    PartRFace,
    PartBHand,
    PartBLeg,
    PartBFace,
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


public class Map : MonoBehaviour
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

    public Vector2[] WarpPoint = new Vector2[2];
    public bool isWarpVertical;

    public Vector2[] HWarpPoint = new Vector2[2];
    public bool isWarpHorizontal;
    
    public Vector3[,] warpPos = new Vector3[4, 2];

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
                        // マップ初期化
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
        warpPos[0, 0] = new Vector3(0,18.31f,0);
        warpPos[0, 1] = new Vector3(18.4f, 0, 0);
        warpPos[1, 0] = new Vector3(-16.93f, 0, 0);
        warpPos[1, 1] = new Vector3(19.53f, 0, 0);
        warpPos[3, 0] = new Vector3(0, 8.39f, 0);
    }

    private void Start()
    {

    }
    private void Update()
    {

    }

    IEnumerator moveWallAdd()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (Transform movewall in GameObject.Find("MoveWalls").transform)
        {
            MoveWalls.Add(movewall.gameObject);
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
    public void PlayerBomDown(MapKind map)
    {
        MapObject[(int)map].GetComponent<PlayerInput>().BombCrash();
    }
}

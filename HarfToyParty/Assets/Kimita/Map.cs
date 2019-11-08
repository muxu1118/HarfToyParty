using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapKind
{
    YUKA = 0,
    Bomb,
    Movewall,
    Player1,
    Player2,
    BombAria,
    Wall
}

public class Map : SingletonMonoBehaviour<Map>
{
    List<List<Vector3>> spritePos = new List<List<Vector3>>();
    List<List<GameObject>> mapObj = new List<List<GameObject>>();
    public int[,] mapInt = new int[7,7];

    public List<MapKind> maps = new List<MapKind>();
    [SerializeField]
    private GameObject[] MapObject = new GameObject[System.Enum.GetNames(typeof(MapKind)).Length];
    bool isMove;
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
        for (int i = 0; i < System.Enum.GetNames(typeof(MapKind)).Length; i++)
        {
            maps.Add((MapKind)i);
        }

    }
    private void Start()
    {
        
        
        
    }
    private void Update()
    {
        // Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string str = "";
            for (int i = 0; i <= 6; i++)
            {
                str += i;
                for (int j = 0; j <= 6; j++)
                {
                    str +=mapInt[j,i].ToString();
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
                    if(mapInt[j,i] == (int)maps[l])
                    {
                        MapObject[(int)maps[l]].transform.position = spritePos[i][j];
                    }

                }
            }
        }
    }
    public void Move(MapKind map, Vector2 vec2)
    {
        if (isMove) return;
        int max = 6, min = 0;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                // Debug
                if (mapInt[j, i] == (int)map)
                {
                    Debug.Log("X" + i + "Y" + j + "移動X" + vec2.x + "移動Y" + vec2.y);
                }
                if (mapInt[j, i] == (int)map && !((i + (int)vec2.x <= max && i + (int)vec2.x >= min) && (j - (int)vec2.y <= max && j - (int)vec2.y >= min)))
                {
                    Debug.Log("位置が悪いよ");
                    return;
                }
                if (mapInt[j, i] == (int)map)
                {
                    if (mapInt[j - (int)vec2.y, i + (int)vec2.x] == 6)
                    {
                        Debug.Log("壁があるよ");
                        return;
                    }
                }
            }
        }
        isMove = true;
        StartCoroutine(MoveAnim(1, map, vec2));
    }
    public void BombAria(int r,MapKind player)
    {
        int y= 0, x =0;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if((int)player == mapInt[j, i])
                {
                    y = j;x = i;
                }
                
            }
        }
        Instantiate(MapObject[(int)MapKind.Bomb], MapObject[(int)MapKind.BombAria].transform.position, Quaternion.identity);
        
        //switch (r)
        //{
        //    case 0:// 上
        //        if (y - 1 < 0) break;
        //        Instantiate(MapObject[(int)MapKind.Bomb], new Vector3(spritePos[y - 1][x].x, spritePos[y - 1][x].y, 1), Quaternion.identity);
        //        break;
        //    case 1:// 下
        //        if (y + 1 > 6) break;
        //        Instantiate(MapObject[(int)MapKind.Bomb], new Vector3(spritePos[y + 1][x].x, spritePos[y + 1][x].y, 1), Quaternion.identity);
        //        break;
        //    case 2:// 右
        //        if (x + 1 > 6) break;
        //        Instantiate(MapObject[(int)MapKind.Bomb], new Vector3(spritePos[y][x + 1].x, spritePos[y][x + 1].y, 1), Quaternion.identity);
        //        break;
        //    case 3:// 左
        //        if (x - 1 < 0) break;
        //        Instantiate(MapObject[(int)MapKind.Bomb], new Vector3(spritePos[y][x - 1].x, spritePos[y][x - 1].y, 1), Quaternion.identity);
        //        break;
        //    default:
        //        break;
        //}

    }
    IEnumerator MoveAnim(float wait,MapKind map,Vector2 vec2)
    {
        float time = wait / Time.deltaTime;
        int x=-1, y=-1;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if (mapInt[j, i] == (int)map)
                {
                    x = i;
                    y = j;
                }
            }
        }
        while (wait >= 0)
        {
            wait -= Time.deltaTime;
            MapObject[(int)map].transform.position += new Vector3((spritePos[y - (int)vec2.y][x + (int)vec2.x].x - spritePos[y][x].x) / time, (spritePos[y - (int)vec2.y][x + (int)vec2.x].y-spritePos[y][x].y) / time);
            //MapObject[(int)map].transform.position += new Vector3((MapObject[(int)map].transform.position.x-spritePos[y - (int)vec2.y][x + (int)vec2.x].x) / time , (spritePos[y - (int)vec2.y][x + (int)vec2.x].y - MapObject[(int)map].transform.position.y  ) / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        MapObject[(int)map].transform.position = new Vector3(spritePos[y - (int)vec2.y][x + (int)vec2.x].x, spritePos[y - (int)vec2.y][x + (int)vec2.x].y);
        mapInt[y, x] = 0;
        mapInt[y - (int)vec2.y,x + (int)vec2.x] = (int)map;
        isMove = false;
    }
    public IEnumerator AriaSet(MapKind player,MapKind Aria)
    {
        while (Tap.IsBomb)
        {
            int x = 0, y = 0;
            Debug.Log(MapObject[(int)player].GetComponent<Player>().rot);
            switch (MapObject[(int)player].GetComponent<Player>().rot)
            {
                case 0://上
                    y = -1;
                    break;
                case 1://下
                    y = 1;
                    break;
                case 2://右
                    x = 1;
                    break;
                case 3://左
                    x = -1;
                    break;
            }
            for (int i = 0; i <= 6; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    if (mapInt[j, i] == (int)player)
                    {
                        if (i + x > 6 || i + x < 0) { x = 0; }
                        if(j + y > 6 || j + y < 0){ y = 0; }
                        MapObject[(int)Aria].transform.position = new Vector3(spritePos[j + y][i + x].x, spritePos[j + y][i + x].y, 1);
                    }
                }
            }
            yield return  new WaitForSeconds(Time.deltaTime);
        }
        yield return null;

    }
}

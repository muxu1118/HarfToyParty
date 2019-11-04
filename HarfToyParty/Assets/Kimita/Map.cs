using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapKind
{
    YUKA = 0,
    Bomb,
    Movewall,
    Player1,
    Player2
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
    private void Start()
    {
        // マップを探す
        foreach (Transform Y in gameObject.transform)
        {
            Debug.Log(Y.name);
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i <= 6; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    Debug.Log("Y:" + i + "X:" + j + "     " + spritePos[i][j]);
                    Debug.Log("X:" + i + "Y:" + j + "     " + mapInt[i, j]);
                }
            }
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
        //max = (GameObject.Find(map.ToString()).transform.GetChild(0).gameObject.activeSelf) ? 5 : 6;
        //min = (GameObject.Find(map.ToString()).transform.GetChild(0).gameObject.activeSelf) ? 1 : 0;
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {

                if (mapInt[j, i] == (int)map)
                {
                    Debug.Log("X" + i + "Y" + j + "移動X" + vec2.x + "移動Y" + vec2.y);
                }
                if (mapInt[j, i] == (int)map && !((i + (int)vec2.x <= max && i + (int)vec2.x >= min) && (j - (int)vec2.y <= max && j - (int)vec2.y >= min)))
                {
                    Debug.Log("位置が悪いよ");
                    return;
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
        Debug.Log("向き:" + r + "X:" + x + "Y:" + y);
        switch (r)
        {
            case 0:// 上
                if (y - 1 < 0) break;
                mapObj[y - 1][x].GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 1:// 下
                if (y + 1 > 6) break;
                mapObj[y + 1][x].GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 2:// 右
                if (x + 1 > 6) break;
                mapObj[y][x + 1].GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 3:// 左
                if (x - 1 < 0) break;
                mapObj[y][x-1].GetComponent<SpriteRenderer>().color = Color.red;
                break;
            default:
                break;
        }

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

}

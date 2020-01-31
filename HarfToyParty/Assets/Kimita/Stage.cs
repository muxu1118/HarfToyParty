using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : SingletonMonoBehaviour<Stage>
{
    public GameObject[] Map = new GameObject[3];
    [SerializeField]
    GameObject[] Player = new GameObject[2];

    private void Start()
    {
        StageSelect(2);
    }
    /// <summary>
    /// ステージを選択する
    /// </summary>
    /// <param name="num"></param>
    public void StageSelect(int num)
    {
        switch (num)
        {
            case 0:
                Instantiate(Map[num]);
                //foreach (Transform playerRoot in Map[num].transform)
                //{
                //    if (playerRoot.name == "Player1ROOT")
                //    {
                //        Player[0] = playerRoot.transform.GetChild(0).gameObject;
                //    }
                //    else if (playerRoot.name == "Player2ROOT")
                //    {
                //        Player[1] = playerRoot.transform.GetChild(0).gameObject;
                //    }
                //}
                //Player[0].GetComponent<Player>().SpawnXY[0] = 6;
                //Player[0].GetComponent<Player>().SpawnXY[1] = 0;
                //Player[1].GetComponent<Player>().SpawnXY[0] = 0;
                //Player[1].GetComponent<Player>().SpawnXY[1] = 6;
                //Player[0].GetComponent<Player>().PlayerMapWrite();
                //Player[1].GetComponent<Player>().PlayerMapWrite();
                break;
            case 1:
                Instantiate(Map[num]);
                break;
            case 2:
                Instantiate(Map[num]);
                break;
        }
    }

    public void StageReset()
    {
        Destroy(GameObject.Find("Tutorial"));
        Destroy(GameObject.Find("Map1"));
        Destroy(GameObject.Find("Map2"));
    }



}

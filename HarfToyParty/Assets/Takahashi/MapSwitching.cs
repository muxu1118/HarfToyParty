using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSwitching : MonoBehaviour
{
    //[SerializeField]
    //GameObject[] gameMap;
    int mapNumber = 0;

    string[] serihu;
    int[] mozisuu;

    private void Start()
    {
        serihu[0] = "taka";
        mozisuu[0] = serihu[0].Length;
        Debug.Log(mozisuu[0]);
    }

    //public void MapDesplay()
    //{
    //    MapFalse();
    //    gameMap[mapNumber].SetActive(true);
    //    mapNumber++;
    //}

    //private void MapFalse()
    //{
    //    switch (mapNumber)
    //    {
    //        case 0:
    //            gameMap[mapNumber].SetActive(false);
    //            break;
    //        case 1:
    //            gameMap[mapNumber].SetActive(false);
    //            break;
    //        case 2:
    //            gameMap[mapNumber].SetActive(false);
    //            break;
    //    }
    //}
}

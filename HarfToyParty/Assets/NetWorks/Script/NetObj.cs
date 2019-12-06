using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetObj : NetworkBehaviour
{
    public GameObject mapobj;
    public GameObject map;
    [SerializeField]
    private Vector2 P3;
    private GameObject _crossWall;

    void Start()
    {
       
        
    }

    void createMap()
    {
        GameObject _map = Instantiate(map, transform.position, Quaternion.identity);
        NetworkServer.Spawn(_map);

        GameObject _mapobj = Instantiate(mapobj);
        NetworkServer.Spawn(_mapobj);
    }

    // Update is called once per frame
    void Update()
    {
             
    }
}

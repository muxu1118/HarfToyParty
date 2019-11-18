using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomDiscovery : NetworkDiscovery
{
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log(fromAddress);

        NetworkManager.singleton.networkAddress = fromAddress;
        NetworkManager.singleton.StartClient();

        StopBroadcast();
    }
}

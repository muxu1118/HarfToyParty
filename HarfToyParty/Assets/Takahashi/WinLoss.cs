using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WinLoss : NetworkBehaviour
{    
    private int winnerDesplay;
    [SerializeField]
    private Text winnerText;
    [SerializeField]
    GameObject panel;

    Text Server;
    Text Client;

    string ServerText = "Lose";
    string ClientText = "Lose";

    void Start()
    {        
        // これがホストだったら
        if (isServer)
        {
            // 1P側の変更
            Server = winnerText;
            Server.text = ServerText;            
        }
        // これがクライアントだったら
        if (isClient)
        {
            // 2P側の変更
            Client = winnerText;
            Client.text = ClientText;
        }
    }

    private void Update()
    {
        if(isServer && winnerDesplay == 1)
        {
            Server.text = ServerText;
        }
        if(isClient && winnerDesplay == 2)
        {
            Client.text = ClientText;
        }
    }

    public void WinOrLoss(int winner)
    {
        winnerDesplay = winner;
        switch (winnerDesplay)
        {
            case 1:
                panel.SetActive(true);
                ServerText = "Win";                
                break;
            case 2:
                panel.SetActive(true);
                ClientText = "Win";
                break;
        }        
    }
}

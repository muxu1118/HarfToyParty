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
    string ServerText = "Loss";
    string ClientText = "Loss";

    void Start()
    {        
        // これがホストだったら
        if (isServer)
        {
            // 1P側の変更
            winnerText.text = ServerText;
            Debug.Log(winnerText.text);
        }
        // これがクライアントだったら
        if (isClient)
        {
            // 2P側の変更
            winnerText.text = ClientText;
            Debug.Log(winnerText.text);
        }
    }

    private void Update()
    {
        if(isServer && winnerDesplay == 1)
        {
            winnerText.text = ServerText;
        }
        if(isClient && winnerDesplay == 2)
        {
            winnerText.text = ClientText;
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

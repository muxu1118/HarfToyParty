using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public class CustomLocalNet : NetworkManager
{
    [SerializeField]
    public GameObject[] CharacterPerfab;
    [SerializeField]
    private Text iptext;
    string ipAddress;
    public int playingPlayer = 0;


    public void ShowIp()
    {
        iptext = GameObject.Find("ipText").GetComponent<Text>();
        ipAddress = IPManager.GetlocalIp();

        iptext.text = ipAddress;
    }

    public void Startuphost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
        OnStartServer();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();


        NetworkManager.singleton.StartClient();
    }

    public void CloseGame()
    {
        //NetworkManager.singleton.OnStopServer();
    }

    void SetIPAddress()
    {
        string inputIp = "localhost";
        NetworkManager.singleton.networkAddress = inputIp;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
       Vector3 Player1Pos = GameObject.Find("P1").transform.position;
        Vector3 Player2Pos = GameObject.Find("P2").transform.position;
        GameObject player;

        if (playingPlayer == 0)
        {
            player = (GameObject)GameObject.Instantiate(CharacterPerfab[0], Player1Pos, Quaternion.identity);
            playingPlayer++;
        }
        else 
        {
            player = (GameObject)GameObject.Instantiate(CharacterPerfab[1], Player2Pos, Quaternion.identity);
        }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            StartCoroutine(SetupMenuSceneButton());
        }
        else
        {
            SetupotherSceneButton();
        }
    }

    IEnumerator SetupMenuSceneButton()
    {
        yield return new WaitForSeconds(0.5f);
        //GameObject.Find("Host").GetComponent<Button>().onClick.RemoveAllListeners();
        //GameObject.Find("Host").GetComponent<Button>().onClick.AddListener(Startuphost);

        //GameObject.Find("Join").GetComponent<Button>().onClick.RemoveAllListeners();
        //GameObject.Find("Join").GetComponent<Button>().onClick.AddListener(JoinGame);

        //GameObject.Find("ShowIpBtn").GetComponent<Button>().onClick.RemoveAllListeners();
        //GameObject.Find("ShowIpBtn").GetComponent<Button>().onClick.AddListener(ShowIp);

        networkAddress = IPManager.GetIP(ADDRESSFAM.IPv4);
        playingPlayer = 0;
    }

    void SetupotherSceneButton()
    {
        //GameObject.Find("DisConnect").GetComponent<Button>().onClick.RemoveAllListeners();
        //GameObject.Find("DisConnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
    }

    public static class IPManager
    {
        public static string GetLocalIPAddress()
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }

        public static string GetlocalIp()
        {
                string strHostName= "";
                strHostName = System.Net.Dns.GetHostName();

                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                IPAddress[] addr= ipEntry.AddressList;

                return addr[addr.Length - 1].ToString();
                
        }

        public static string GetIP(ADDRESSFAM Addfam)
        {
            //Return null if ADDRESSFAM is Ipv6 but Os does not support it
            if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
            {
                return null;
            }

            string output = "";

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
                NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
                NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

                if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        //IPv4
                        if (Addfam == ADDRESSFAM.IPv4)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                output = ip.Address.ToString();
                            }
                        }

                        //IPv6
                        else if (Addfam == ADDRESSFAM.IPv6)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                            {
                                output = ip.Address.ToString();
                            }
                        }
                    }
                }
            }
            return output;
        }
    }

    public enum ADDRESSFAM
    {
        IPv4, IPv6
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bgm : MonoBehaviour
{
    public bool DontDestroyBgm = true;

    void Start()
    {
        if (DontDestroyBgm == true)
        {
            DontDestroyOnLoad(this);
        }
        SceneManager.sceneLoaded += SceneLoaded;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainGame"){
            DontDestroyBgm = false;
            Destroy(this.gameObject);
        }
    }
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        if (nextScene.name == "TalkScene") return;
        DontDestroyBgm = false;
        Destroy(this.gameObject);
    }
}

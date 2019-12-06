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

    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        DontDestroyBgm = false;
<<<<<<< HEAD
        Destroy(this.gameObject);
=======
        //Destroy(this.gameObject);
>>>>>>> origin/Lai
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

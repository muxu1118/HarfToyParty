using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMonoBehaviour<SceneController>
{
    /// <summary>
    /// シーン遷移する際に使って
    /// sceneNameに遷移したい名前打てば大丈夫
    /// </summary>
    /// <param name="sceneName"></param>
    public void sceneSwitching(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

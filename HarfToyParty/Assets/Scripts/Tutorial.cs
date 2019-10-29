using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {
    
    void Start() {
        
    }
    
    void Update() {
        
    }

    public void TutorialEnd() {
        SceneManager.LoadScene("MapSelectScene");
    }
}

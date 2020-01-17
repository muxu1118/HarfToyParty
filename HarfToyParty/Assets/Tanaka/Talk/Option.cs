using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject menu;

    private bool activeFlag = false;

    private void Start()
    {
        Panel.SetActive(true);
    }

    public void Menumenu()
    {
        Panel.SetActive(true);
    }

    public void Numenume()
    {
        Panel.SetActive(false);
    }
}

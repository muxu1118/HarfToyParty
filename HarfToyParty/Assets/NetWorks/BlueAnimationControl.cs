using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAnimationControl : MonoBehaviour
{
    Animator _animator;
    public bool Win;
    public bool Lose;

    // Start is called before the first frame update
    void Start()
    {
        Win = false;
        Lose = false;
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Win && !Lose) {
            _animator.SetBool("WinTriggle", true);
        }
        else if (!Win && Lose) {
            _animator.SetBool("LoseTriggle", true);
        }
        else 
        {
            _animator.SetBool("WinTriggle", false);
            _animator.SetBool("LoseTriggle", false);
        }
    }
}

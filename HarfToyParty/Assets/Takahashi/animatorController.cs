using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorController : SingletonMonoBehaviour<animatorController>
{
    Animator _animator;
    bool win = false;
    bool lose = false;
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (win && !lose)
        {
            _animator.SetBool("WinTriggle", true);
        }
        else if (!win && lose)
        {
            _animator.SetBool("LoseTriggle", true);
        }
        else
        {
            _animator.SetBool("WinTriggle", false);
            _animator.SetBool("LoseTriggle", false);
        }
    }

    public void redWinAnimation()
    {

    }
}

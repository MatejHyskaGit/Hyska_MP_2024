using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NathanielLeaveScript : MonoBehaviour
{
    private Animator NathanielAnimator;

    void Start()
    {
        NathanielAnimator = GetComponent<Animator>();
    }
    public void SetNathanielTrue()
    {
        NathanielAnimator.Play("TavernNathanielHere");
    }
    public void SetNathanielFalse()
    {
        NathanielAnimator.Play("TavernNathanielGone");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoinkScript : MonoBehaviour
{
    public static bool onePlaying;

    [SerializeField] private Animator BartenderAnimator;

    public void SetJduNahoru()
    {
        BartenderAnimator.ResetTrigger("dolu");
        BartenderAnimator.SetTrigger("nahoru");
    }
    public void SetJduDolu()
    {
        BartenderAnimator.ResetTrigger("nahoru");
        BartenderAnimator.SetTrigger("dolu");
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BartenderTurn : MonoBehaviour
{
    private Animator BartenderAnimator;


    [SerializeField] private GameObject BartenderObject;

    bool playingD = false;

    void Start()
    {
        BartenderAnimator = BartenderObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (BartenderObject.transform.position.y - 0.4 > MovementManager.instance.actualPos.y)
        {
            if (!playingD)
            {
                BartenderAnimator.Play("1D");
                playingD = true;
            }
        }
        else if (playingD)
        {
            BartenderAnimator.Play("1R");
            playingD = false;
        }
    }
}

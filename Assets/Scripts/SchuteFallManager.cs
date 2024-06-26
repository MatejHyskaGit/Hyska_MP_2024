using System.Collections;
using System.Collections.Generic;
using System.Security;
using Cinemachine;
using UnityEngine;

public class SchuteFallManager : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera CameraVC;
    [SerializeField] Animator CameraAnimator;
    [SerializeField] public GameObject PlayerObject;

    public static SchuteFallManager instance;

    void Awake()
    {
        instance = this;
    }

    public void StartChute()
    {
        if (GameManager.instance.lastSceneName == "MalirRoom4" || GameManager.instance.lastSceneName == "Menu")
        {
            SetFollowPlayer();
        }
        else
        {
            CameraVC.Follow = null;
            CameraAnimator.Play("CameraFall");
        }
    }
    public void SetFollowPlayer()
    {
        CameraVC.Follow = PlayerObject.transform;
        GameManager.instance.loading = false;
    }
}

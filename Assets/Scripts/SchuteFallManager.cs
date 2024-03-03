using System.Collections;
using System.Collections.Generic;
using System.Security;
using Cinemachine;
using UnityEngine;

public class SchuteFallManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera CameraVC;
    [SerializeField] Animator CameraAnimator;
    [SerializeField] GameObject PlayerObject;

    public static SchuteFallManager instance;

    void Awake()
    {
        instance = this;
    }

    public void StartChute()
    {
        CameraVC.Follow = null;
        CameraAnimator.Play("CameraFall");
    }
    public void SetFollowPlayer()
    {
        CameraVC.Follow = PlayerObject.transform;
        GameManager.instance.loading = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlideLoadScript : MonoBehaviour
{
    public void SetLoadTrue()
    {
        GameManager.instance.loading = true;
    }
    public void SetLoadFalse()
    {
        GameManager.instance.loading = false;
    }
}

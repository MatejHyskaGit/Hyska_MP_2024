using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemNeedScript : MonoBehaviour
{
    public void UseItem(Item item, GameObject calledObject)
    {

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MalirRoom3")
        {
            //Debug.Log("MalirRoom3");
            if (item.Name == "Opracovaný kámen")
            {
                //Debug.Log("Opracovaný kámen");
                if (calledObject.name == "ItemNeederMid" && !GameManager.instance.statueFixed)
                {
                    //Debug.Log("Success");
                    GameManager.instance.statueFixed = true;
                    GameManager.instance.RemoveItem(item);
                }
            }

        }
    }
}

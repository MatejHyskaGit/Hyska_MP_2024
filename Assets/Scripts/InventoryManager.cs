using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] GameObject singleItemPanel;

    [SerializeField] GameObject[] itemsArray;

    public List<Item> ItemList;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
   
    void Start()
    {
        ItemList = GameManager.instance.ItemListGM;
        singleItemPanel.SetActive(false);
    }

    private void LoadItems()
    {
        Sprite transparent = Resources.Load<Sprite>("Sprites/Transparent");
        foreach (GameObject item in itemsArray)
        {
            item.GetComponentInChildren<Image>().sprite = transparent;
            item.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        int index = 0;
        foreach (Item item in ItemList)
        {
            if(item.Icon != null)
            {
                Debug.LogWarning(item.Icon);
                itemsArray[index].GetComponentInChildren<Image>().sprite = item.Icon;
            }
            itemsArray[index].GetComponentInChildren<TextMeshProUGUI>().text = item.Name;
            index++;
        }
    }

    public void UpdateList()
    {
        ItemList = GameManager.instance.ItemListGM;
        LoadItems();
    }

    public void OpenItem(string itemname)
    {
        singleItemPanel.SetActive(true);
        Item item = ItemList.Where(i => i.Name == itemname).FirstOrDefault();
        singleItemPanel.GetComponentsInChildren<Image>()[2].sprite = item.Icon;
        TextMeshProUGUI[] txtList = singleItemPanel.GetComponentsInChildren<TextMeshProUGUI>();
        txtList[0].text = item.Name;
        txtList[1].text = item.Description;
    }
    public void Close()
    {
        singleItemPanel.SetActive(false);
    }
}

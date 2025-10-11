using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    static Inventory instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SetItem(Item item)
    {
        if (instance == null)
        {
            return;
        }

        instance.items.Add(item);
        UiManager.SetInventoryImage(item);
    }
}

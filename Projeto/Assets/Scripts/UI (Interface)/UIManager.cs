using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public static UiManager instance;

    public Image[] iventoryImages;

    public GameObject interactionPanel;
    public TMP_Text interactionText;
    public Image portrait;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }




    public static void SetInventoryImage(Item item)
    {
        if (instance == null)
        {
            return;
        }

        for (int i = 0; i < instance.iventoryImages.Length; i++)
        {
            if (!instance.iventoryImages[i].gameObject.activeInHierarchy)
            {
                instance.iventoryImages[i].sprite = item.itemImage;
                instance.iventoryImages[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    internal static void RemoveInventoryImage(Item usedItem)
    {
        throw new NotImplementedException();
    }
}
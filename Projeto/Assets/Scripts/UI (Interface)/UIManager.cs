using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{

    public static UiManager instance;

    public Image[] iventoryImages;

    public GameObject interactionPanel;
    public TMP_Text interactionText;
    public Image portrait;

    TextInteractable textInteractable;


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

    public static void SetText(TextInteractable interactable)
    {
        if (instance == null)
        {
            return;
        }

        instance.portrait.sprite = interactable.portraitImage;

        if(interactable.conditionalItem != null)
        {
            Debug.Log("Tem item condicional");
            if(Inventory.HasItem(interactable.conditionalItem))
            {
                Debug.Log("Tem o item");
                instance.interactionText.text = interactable.conditionalText;
            }
            else
            {
                Debug.Log("Jogador não tem o item");
                instance.interactionText.text = interactable.conditionalText;
            }
        }
        else
        {
            Debug.Log("Não tem item condicional");
            instance.interactionText.text = interactable.conditionalText;
        }

        instance.interactionPanel.SetActive(true);
        instance.textInteractable = interactable;
    }

    public static void DisableInteraction()
    {
        if(instance == null)
        {
            return;
        }
        
        instance.interactionPanel.SetActive(false);
        if(instance.textInteractable != null)
        {
            instance.textInteractable.isInteracting = false;
        }
    }
}
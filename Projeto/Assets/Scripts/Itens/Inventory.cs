using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Tenta pegar slots filhos primeiro
        itemSlots = new List<ItemSlot>(GetComponentsInChildren<ItemSlot>());

        // Se nada foi encontrado, tenta buscar globalmente (fallback)
        if (itemSlots.Count == 0)
        {
            ItemSlot[] found = FindObjectsOfType<ItemSlot>();
            itemSlots = new List<ItemSlot>(found);
            Debug.LogWarning($"Inventory: nenhum ItemSlot filho encontrado, usando fallback. Slots achados = {itemSlots.Count}");
        }
        else
        {
            Debug.Log($"Inventory: slots encontrados como filhos = {itemSlots.Count}");
        }
    }

    public static void SetItem(Item item)
    {
        if (instance == null) return;

        instance.items.Add(item);
        UiManager.SetInventoryImage(item);
    }

    public static bool HasItem(Item item)
    {
        if (instance == null) return false;
        return instance.items.Contains(item);
    }


    public ItemSlot selectedSlot;

    public void SetSelectedSlot(ItemSlot slot)
    {
        selectedSlot = slot;
    }


    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            var slot = itemSlots[i];
            if (slot != null)
            {
                slot.Deselect();
            }
        }
    }
}

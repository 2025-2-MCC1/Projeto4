using UnityEngine;

public class CollectableItem : Interactable
{
    public Item item;

    public override void Interact()
    {
        if (item == null)
        {
            Debug.LogWarning($"O objeto {gameObject.name} não possui um Item definido!");
            return;
        }

        // Adiciona o item ao inventário
        Inventory.SetItem(item);
        Debug.Log($"Coletou {item.name}");

        // Encontra todos os objetos do tipo, inclusive desativados
        CollectableItem[] allItems = Resources.FindObjectsOfTypeAll<CollectableItem>();

        foreach (CollectableItem obj in allItems)
        {
            if (obj != null && obj.item == item)
            {
                if (obj.gameObject.scene.IsValid()) // ignora prefabs fora da cena
                {
                    Destroy(obj.gameObject);
                }
            }
        }
    }
}

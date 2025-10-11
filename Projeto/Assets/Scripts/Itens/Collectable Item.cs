using Unity.VisualScripting;
using UnityEngine;

public class CollectableItem : Interactable
{
    public Item item;

    public override void Interact()
    {
        //adicionar ao inventário

        Inventory.SetItem(item);    
        Debug.Log("Coletou " + item.name);
        Destroy(gameObject);
        
    }
}

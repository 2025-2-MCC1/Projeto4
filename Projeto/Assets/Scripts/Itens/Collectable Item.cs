using Unity.VisualScripting;
using UnityEngine;

public class CollectableItem : Interactable
{
    public Item item;

    public override void Interact()
    {
        //adicionar ao invent�rio
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Coletou " + item.name);
            Destroy(gameObject);
        }
        
    }
}

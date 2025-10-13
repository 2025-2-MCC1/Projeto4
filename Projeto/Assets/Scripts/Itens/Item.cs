using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("Identifica��o �nica")]
    public string itemID;

    public string itemName;
    public Sprite itemImage;
    [TextArea]
    public string description;
}

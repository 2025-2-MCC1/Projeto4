using UnityEngine;


public enum ObjectType
{
    ground, door, text, dialogue, collectable, none
}

public class Outline : MonoBehaviour
{
    public ObjectType objectType;
}

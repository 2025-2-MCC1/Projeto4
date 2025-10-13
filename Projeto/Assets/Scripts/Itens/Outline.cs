using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ObjectType
{
    ground, door, text, dialogue, collectable, none, interactable
}

public class Outline : MonoBehaviour
{
    public ObjectType objectType;
}

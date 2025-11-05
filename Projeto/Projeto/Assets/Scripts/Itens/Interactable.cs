using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]     //az o objeto necessitar do componete script "Outline"
public abstract class Interactable : MonoBehaviour
{
    public ObjectType objectType;       //função que determina o tipo de objeto

    public bool isInteracting;      //função que determina se está interagindo
    public Item conditionalItem;        //função que determina o item como condicional

    public Outline outline;     //permite acessar o script Outline

    private void Awake()        //é chamado automaticamente quando um objeto de jogo é instanciado na cena, antes que o primeiro frame seja atualizado
    {
        //tenta encontrar o componente outline e deixa ele como desativado
        try
        {
            outline = GetComponent<Outline>();
            outline.enabled = false;
        }
        //informa se o componete não está presente
        catch
        {
            Debug.LogError("Objeto " + gameObject.name + " precisa de um Outline");
        }

    }

    public abstract void Interact();        //função que permite o uso desse script
}
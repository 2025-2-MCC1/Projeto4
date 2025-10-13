using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]     //az o objeto necessitar do componete script "Outline"
public abstract class Interactable : MonoBehaviour
{
    public ObjectType objectType;       //fun��o que determina o tipo de objeto

    public bool isInteracting;      //fun��o que determina se est� interagindo
    public Item conditionalItem;        //fun��o que determina o item como condicional

    public Outline outline;     //permite acessar o script Outline

    private void Awake()        //� chamado automaticamente quando um objeto de jogo � instanciado na cena, antes que o primeiro frame seja atualizado
    {
        //tenta encontrar o componente outline e deixa ele como desativado
        try
        {
            outline = GetComponent<Outline>();
            outline.enabled = false;
        }
        //informa se o componete n�o est� presente
        catch
        {
            Debug.LogError("Objeto " + gameObject.name + " precisa de um Outline");
        }

    }

    public abstract void Interact();        //fun��o que permite o uso desse script
}
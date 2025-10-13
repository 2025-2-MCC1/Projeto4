using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("UI")]
    public GameObject selectedShader; // painel de destaque (SelectedPanel)
    public bool thisItemSelected;
    public Item Item;

    [Header("Runtime")]
    public Inventory inventory; // pode arrastar no inspector ou ser� buscado no Start

    private void Awake()
    {
        // garante que o shader comece invis�vel
        if (selectedShader != null)
            selectedShader.SetActive(false);
    }

    private void Start()
    {
        // tenta resolver o inventory automaticamente se n�o foi definido no inspector
        if (inventory == null)
        {
            inventory = GetComponentInParent<Inventory>();
            if (inventory == null)
            {
                inventory = FindObjectOfType<Inventory>();
            }
        }

        Debug.Log($"ItemSlot.Start - name: {gameObject.name} | inventory: {(inventory != null ? inventory.name : "null")} | selectedShader: {(selectedShader != null ? selectedShader.name : "null")}");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"ItemSlot.OnPointerClick on {gameObject.name} button: {eventData.button}");
        if (eventData.button == PointerEventData.InputButton.Left)
            OnLeftClick();
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnRightClick();
    }

    private void OnLeftClick()
    {
        if (inventory == null)
        {
            Debug.LogError($"ItemSlot {gameObject.name}: inventory est� null ao clicar. Verifique refer�ncia.");
            return;
        }

        // se j� estava selecionado, desseleciona tudo
        if (thisItemSelected)
        {
            inventory.DeselectAllSlots();
            return;
        }

        // limpa todos e seleciona este
        inventory.DeselectAllSlots();
        Select();
    }

    private void OnRightClick()
    {
        
    }

    public void Select()
    {
        if (selectedShader != null)
            selectedShader.SetActive(true);

        thisItemSelected = true;

        // Atualiza o slot selecionado no Inventory
        if (inventory != null)
        {
            inventory.SetSelectedSlot(this);
        }
    }


    public void Deselect()
    {
        if (selectedShader != null)
            selectedShader.SetActive(false);
        thisItemSelected = false;
        // Debug.Log($"ItemSlot.Deselect -> {gameObject.name}");
    }

    internal void ClearSlot()
    {
        throw new NotImplementedException();
    }
}

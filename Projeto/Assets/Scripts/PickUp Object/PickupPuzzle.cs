using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PickupPuzzle : MonoBehaviour
{
    [Header("Informações do puzile")]
    [SerializeField] private UnityEvent _evento;

    [Header("Inspeção de Objeto")]
    [SerializeField] private PickUp objectPickUp;

    [Header("Objetos do puzile")]
    [SerializeField] private GameObject _pickupInterativo;
    [SerializeField] private GameObject _pickupPuzzle;
    [SerializeField] private GameObject _vc;

    [Header("Rotação da Sala")]
    [SerializeField] private RotacaoDaSala rotacaoDaSala;
    private bool _puzzlesStarts;

    void Update()
    {
        //BLOQUEIO DE SAÍDA DO PUZZLE DURANTE INSPEÇÃO
        if (_puzzlesStarts == true)
        {
            if (objectPickUp != null && objectPickUp.inspecting)
                return;

            if (Input.GetMouseButtonDown(1))
            {
                EndPuzzle();
                return;
            }
        }

    }

    public void StartPuzzle()
    {
        StartCoroutine(PuzzleStart());
        //Desativa o código LockPuzzleum
        if (rotacaoDaSala.pickupPuzzle != null)
        {
            rotacaoDaSala.pickupPuzzle.enabled = true;
        }
    }

    public void EndPuzzle()
    {
        GameManager.Instance.UnPauseGame();

        //Reativa rotação da sala e restaura paredes normais
        if (rotacaoDaSala != null)
        {
            rotacaoDaSala.enabled = true;
            rotacaoDaSala.RestaurarParedesPadrao();
        }

        _pickupInterativo.SetActive(true);
        _pickupPuzzle.SetActive(false);
        _vc.SetActive(false);
        _puzzlesStarts = false;
    }

    IEnumerator PuzzleStart()
    {
        //Desabilita rotação da sala e exibe todas as paredes
        if (rotacaoDaSala != null)
        {
            rotacaoDaSala.enabled = false;
            rotacaoDaSala.ForcarTodasAsParedes(true);
        }

        _vc.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.PauseGame();
        _pickupInterativo.SetActive(false);
        _pickupPuzzle.SetActive(true);
        _puzzlesStarts = true;
    }
}

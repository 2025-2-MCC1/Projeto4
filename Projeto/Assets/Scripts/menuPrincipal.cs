using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
   
   [SerializeField] private GameObject painelmenuinicial;
   [SerializeField] private GameObject painelopcoes;
   [SerializeField] private GameObject MenudoNewGame;
    public void Iniciar()
    {
       MenudoNewGame.SetActive(true);
    }

    public void NovoJogo()
    {
        SceneManager.LoadScene("MapaT");
    }

    public void VoltarAoMenu()
    {
       painelmenuinicial.SetActive(true);
        MenudoNewGame.SetActive(false);
    }

    public void AbrirOpcoes()
        {
        painelmenuinicial.SetActive(false);
        painelopcoes.SetActive(true);
    }


    public void FecharOpcoes()

    { 
    painelmenuinicial.SetActive(true);
    painelopcoes.SetActive(false);

    }

    public void Sair()
    {
        Debug.Log("Sair");
        Application.Quit();
    }


}

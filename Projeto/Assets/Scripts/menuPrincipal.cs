using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
   [SerializeField] private string NomeDaFase;
   [SerializeField] private GameObject painelmenuinicial;
   [SerializeField] private GameObject painelopcoes;
    public void Iniciar()
    {
        SceneManager.LoadScene("MapaT");
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

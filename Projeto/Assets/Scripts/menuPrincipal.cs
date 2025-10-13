using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
   
    //fun��es que criam os elementos do menu
    //SerializeField permite acessar essa fun��o no espectro da unity, mas n�o permite editar ele com outros scripts
   [SerializeField] private GameObject painelmenuinicial;
   [SerializeField] private GameObject painelopcoes;
   [SerializeField] private GameObject MenudoNewGame;
    
    //fun��o que determina se o menu do jogo vai ser ativado
    public void Iniciar()
    {
       MenudoNewGame.SetActive(true);
    }

    //fun��o que carrega a fase escolhida caso o bot�o seja pressionado
    public void NovoJogo()
    {
        SceneManager.LoadScene("MapaT");        //LoadScene muda para a cena determinada
    }

    //fun��o que permite retornar ao menu inicial
    public void VoltarAoMenu()
    {
       painelmenuinicial.SetActive(true);       //ativa o painel
        MenudoNewGame.SetActive(false);     //desativa a fun��o menunewgame
    }

    //fun��o que permite acessar o menu de op��es do jogo ao pressionar o bot�o
    public void AbrirOpcoes()
        {
        painelmenuinicial.SetActive(false);     //desativa o painel menuinicial
        painelopcoes.SetActive(true);       //ativa o painel
    }


    //fun��o que fecha o menu de op��es do jogo ao pressionar o bot�o
    public void FecharOpcoes()

    { 
    painelmenuinicial.SetActive(true);      //ativa o painel
        painelopcoes.SetActive(false);      //desativa o painel de op��es

    }

    //fun��o que permite sair do jogo ao pressionar o bot�o
    public void Sair()
    {
        Debug.Log("Sair");      //escreve no console o que foi solicitado
        Application.Quit();     //fecha o jogo
    }


}

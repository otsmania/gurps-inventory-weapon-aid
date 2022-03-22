using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text txtNome;
    public Text txtQuantidade;
    public Button btnEnter; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AtualizarInfo(string nomeSala, int playersAtual, int playersTotal, Launcher ui)
    {
        txtNome.text = nomeSala;
        txtQuantidade.text = string.Format("{0}/{1}", playersAtual,playersTotal);
        btnEnter.onClick.AddListener(delegate { ui.CriarOuEntrarNaSala(nomeSala, false); });
    }


}

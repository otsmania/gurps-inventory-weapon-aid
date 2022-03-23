using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    /////////////////////////////////////////////////////////////////////////
    //colocar o tipo aviso.normal e o. erro em toda a mensagem q pedir    //
    //  txtAviso.text = "<color=#FF0000>" + msg + "</color>";            //
    //////////////////////////////////////////////////////////////////////


    private string gameVersion = "1";
    private byte maxPlayer = 4;
    public Text txtAviso;
    
    [Header("LOGIN")]
    public GameObject painelLogin;
    
    public InputField tbNick;
    [Header("PAINEL SALA")]
    public GameObject painelSala;
    public InputField tbSala;
    public GameObject prefabItemSala;
    public Transform painelSalasExistentes;

    public enum TipoAviso
    {
        NORMAL, ERRO
    }


    void Start()
    {
        PegarNickSalvo();
        TrocarViewLogin(true);
        TrocarViewSala(false);
        
        ExibirAviso("", TipoAviso.NORMAL);

        
    }

    
    void Update()
    {
        
    }

    void PegarNickSalvo()
    {
        if(PlayerPrefs.HasKey("NICK"))
        {
            tbNick.text = PlayerPrefs.GetString("NICK");
        }
    }    

    void TrocarViewLogin(bool vizualizar)
    {
        painelLogin.SetActive(vizualizar);

    }
    void TrocarViewSala(bool vizualizar)
    {
        painelSala.SetActive(vizualizar);

    }

    void ExibirAviso(string msg, TipoAviso tipo)
    {
        if(tipo == TipoAviso.NORMAL)
        {
            txtAviso.text = msg;
        }
        else
        {
            txtAviso.text = "<color=#e64d6e>" + msg + "</color>";
        }
        
       
    }

    public void onclick_botaoNick()
    {
        ExibirAviso("", TipoAviso.NORMAL);
        string nick = tbNick.text;
        if(nick.Length < 3)
        {
            ExibirAviso("INVALID NICKNAME", TipoAviso.ERRO);
            return;
        }
        
        PlayerPrefs.SetString("NICK", nick);

        ExibirAviso("CONNECTING.......", TipoAviso.NORMAL);
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.LocalPlayer.NickName = nick;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Onclick_criarSala()
    {
        ExibirAviso("", TipoAviso.NORMAL);
        string nomesala = tbSala.text;
        if(nomesala.Length < 2)
        {
            ExibirAviso("INVALID ROOM NAME", TipoAviso.ERRO);
            return;
        }

        CriarOuEntrarNaSala(nomesala, true);
        
    }

    public void CriarOuEntrarNaSala(string nomeSala, bool criarNova = false)
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = maxPlayer;

        PhotonNetwork.JoinOrCreateRoom(nomeSala, ro, TypedLobby.Default);
        if(criarNova)
        {
            ExibirAviso("CREATING ROOM...", TipoAviso.NORMAL);
        }
        else
        {
            ExibirAviso("JOINING ROOM... ", TipoAviso.NORMAL);
        }
    }

   
    public override void OnConnectedToMaster()
    {
        ExibirAviso(string.Format("CONNECTING IN : '{0}' ",PhotonNetwork.CloudRegion), TipoAviso.NORMAL);
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        TrocarViewLogin(false);
        TrocarViewSala(true);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("jogo");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AtualizarListaSalas(roomList);
    }

    void AtualizarListaSalas(List<RoomInfo> lista)
    {
        foreach (RoomInfo item in lista)
        {
            GameObject copia = Instantiate(prefabItemSala, Vector3.zero, Quaternion.identity, painelSalasExistentes);
            copia.GetComponent<RoomItem>().AtualizarInfo(item.Name,item.PlayerCount, item.MaxPlayers, this);
        }
    }





}


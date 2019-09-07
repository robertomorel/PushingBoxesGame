using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using System.Net;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkController : MonoBehaviourPunCallbacks
{

    [Header("GO")]
    public GameObject loginGO;
    public GameObject partidasGO;
    public GameObject botao1;
    public GameObject botao2;

    [Header("Player")]
    public InputField playerNameInput;
    string playerNameTemp;
    public GameObject myPlayer;

    //[Header("Room")]
    public string nomeDaSala;

    // Use this for initialization
    void Start()
    {
        playerNameTemp = "Player" + UnityEngine.Random.Range(1000, 10000);
        playerNameInput.text = playerNameTemp;

        nomeDaSala = "Room" + UnityEngine.Random.Range(1000, 10000);

        loginGO.gameObject.SetActive(true);
        partidasGO.gameObject.SetActive(false);
        botao1.gameObject.SetActive(false);
        botao2.gameObject.SetActive(false);

    }

    public void Login()
    {
        if (playerNameInput.text != "")
        {
            PhotonNetwork.NickName = playerNameInput.text;
        }
        else
        {
            PhotonNetwork.NickName = playerNameTemp;
        }

        PhotonNetwork.ConnectUsingSettings();

        loginGO.gameObject.SetActive(false);
    }

    public void BotaoBuscarPartidaRapida()
    {
        //Optando por iniciar num lobby antes de entrar numa sala
        PhotonNetwork.JoinLobby();

        botao1.gameObject.SetActive(true);
        botao2.gameObject.SetActive(true);
    }

    public void BotaoCriarSala()
    {
        string roomTemp = nomeDaSala;
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomTemp, roomOptions, TypedLobby.Default);
    }


    //######################## PunCallbacks ####################################
    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        Debug.Log("Server: " + PhotonNetwork.CloudRegion + " ==> Ping: " + PhotonNetwork.GetPing());

        partidasGO.gameObject.SetActive(true);

        //Optando por iniciar num lobby antes de entrar numa sala
        //PhotonNetwork.JoinLobby();

    }

    //Ao entrar num Lobby, tentaremos entrar numa sala randomica
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    //Tratando a falha ao entrar numa sala randomica
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string nomeDeSalaTemporaria = "Room" + UnityEngine.Random.Range(1000, 10000) + UnityEngine.Random.Range(1000, 10000);
        PhotonNetwork.CreateRoom(nomeDeSalaTemporaria);
    }

    //Método responsável por tratar o usuário pós entrada numa sala
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        Debug.Log("Room Name: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Current player in room: " + PhotonNetwork.CurrentRoom.PlayerCount);

        loginGO.gameObject.SetActive(false);
        partidasGO.gameObject.SetActive(false);
        botao1.gameObject.SetActive(true);
        botao2.gameObject.SetActive(true);

        //Instantiate(myPlayer, myPlayer.transform.position, myPlayer.transform.rotation);
        //PhotonNetwork.Instantiate(myPlayer.name, myPlayer.transform.position, myPlayer.transform.rotation, 0);

    }

}

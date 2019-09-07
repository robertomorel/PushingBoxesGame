using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviourPun
{
    public void CenaComJoystick()
    {
        GameManager.joystick = true;
        PhotonNetwork.LoadLevel("Principal");
    }

    public void CenaSemJoystick()
    {
        GameManager.joystick = false;
        PhotonNetwork.LoadLevel("Principal");
    }
}

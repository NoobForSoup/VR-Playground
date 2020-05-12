using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public MultiplayerHandler mpHandler;
    public Text statusText;

    private bool pressed;

    private void Update()
    {
        statusText.text = PhotonNetwork.NetworkClientState.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!pressed && other.CompareTag("GameController"))
        {
            pressed = true;
            mpHandler.Connect();
        }
    }
}

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MPPlayer : MonoBehaviourPunCallbacks
{
    public List<GameObject> enable;
    public List<GameObject> disable;
    public Camera camera;

    public void Start()
    {
        if(GetComponent<PhotonView>().IsMine)
        {
            camera.enabled = true;

            GetComponent<SteamVR_PlayArea>().enabled = true;
            foreach(GameObject obj in enable)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in disable)
            {
                obj.SetActive(false);
            }
        }
    }
}

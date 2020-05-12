using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCharge : MonoBehaviour
{
    public Wand_Charge wand;
    public bool released = false;
    public float defaultSize = 0.1f;
    public float speed = 25f;
    public bool destroyOnCollision;

    private void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] data = GetComponent<PhotonView>().InstantiationData;
        this.wand = PhotonView.Find((int)data[0]).GetComponent<Wand_Charge>();
    }

    void Update()
    {
        if(released == false)
        {
            float size = defaultSize + 0.1f * wand.charge;

            transform.localScale = new Vector3(size, size, size);

            foreach(TrailRenderer tr in GetComponentsInChildren<TrailRenderer>())
            {
                tr.widthMultiplier = size / 10;
            }

            transform.position = wand.muzzle.position;
            transform.rotation = wand.muzzle.rotation;
        }
        else
        {
            GetComponent<Collider>().enabled = true;  
        }
    }

    public void Release(Vector3 velocity)
    {
        GetComponent<Rigidbody>().velocity = velocity * speed;
        released = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(destroyOnCollision)
        {
            Destroy(gameObject);
        }
    }
}

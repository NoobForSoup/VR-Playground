using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosion;
    public AudioSource audioSource;
    public AudioClip audio;

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        GameObject obj = Instantiate(explosion, transform.position, explosion.transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightsaber : Sword
{
    public Color color;
    public List<GameObject> lightParts;

    public AudioSource audioSource;

    public void Start()
    {
        SetColor();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void SetColor()
    {
        foreach(GameObject obj in lightParts)
        {
            obj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
            obj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color);
        }
    }
}

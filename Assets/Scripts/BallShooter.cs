using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public GameObject prefab;
    public Transform muzzle;

    public float delay = 5f;
    private float currentDelay;

    public float force = 5f;

    void Start()
    {
        currentDelay = delay;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(currentDelay >= 0f)
        {
            currentDelay -= Time.deltaTime;

            if(currentDelay <= 0f)
            {
                currentDelay = delay;
                SpawnBall();
            }
        }
    }

    public void SpawnBall()
    {
        GameObject obj = Instantiate(prefab, muzzle.position, muzzle.rotation);
        obj.GetComponent<Rigidbody>().AddForce(obj.transform.forward * force, ForceMode.Impulse);
    }
}

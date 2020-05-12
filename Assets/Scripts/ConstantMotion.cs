using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMotion : MonoBehaviour
{
    public Vector3 movement;
    public Vector3 rotation;

	void Update()
    {
        transform.position += ((transform.forward * movement.x) + (transform.up * movement.y) + (transform.right * movement.z)) * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(rotation * Time.deltaTime);
	}
}

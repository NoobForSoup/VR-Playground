using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDetector : MonoBehaviour
{
    public bool activated;
    public float activationTime;

    public bool leftTrigger = false;
    public bool rightTrigger = false;

	void Update()
    {
        Timer();

        if(activated)
        {
            Check();
        }
	}

    private void Timer()
    {
        if (activationTime > 0f)
        {
            activationTime -= Time.deltaTime;

            if (activationTime < 0f)
            {
                activationTime = 0f;
                activated = false;
            }
        }
    }

    private void Check()
    {
        if(leftTrigger && rightTrigger)
        {
            Debug.Log("YEET");
        }
    }
}

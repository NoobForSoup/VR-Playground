using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerMovement : MonoBehaviour
{
    public Transform head;
    public GrapplingGun gun;

    public SteamVR_Action_Boolean m_PadAction = SteamVR_Input.__actions_default_in_Teleport;
    public SteamVR_Action_Vector2 m_PadLocation = SteamVR_Input.__actions_default_in_TrackpadPosition;

    public float speed;

    private float startGravity;
    private float gravity = -9.81f;
    private Vector3 velocity;
    private CharacterController cc;

    private void Start()
    {
        startGravity = gravity;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gun != null && gun.grapple != null && gun.grapple.GetComponent<Grapple>().stuck)
        {
            if (Vector3.Distance(transform.position, gun.grapple.transform.position) > 3f)
            {
                var cc = GetComponent<CharacterController>();
                var offset = gun.grapple.transform.position - transform.position;

                if (offset.magnitude > .1f)
                {
                    offset = offset.normalized * 20f;
                    cc.Move(offset * Time.deltaTime);
                }
            }

            gravity = 0;
        }
        else
        {
            gravity = startGravity;
        }

        if (cc.isGrounded)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        cc.Move(velocity * Time.deltaTime);

        if (m_PadAction.GetState(SteamVR_Input_Sources.RightHand))
        {
            Vector3 movement;
            movement = head.forward * m_PadLocation.GetAxis(SteamVR_Input_Sources.RightHand).y * Time.deltaTime * speed;
            movement += head.right * m_PadLocation.GetAxis(SteamVR_Input_Sources.RightHand).x * Time.deltaTime * speed;
            movement.y = 0;
            cc.Move(movement);
        }
    }
}

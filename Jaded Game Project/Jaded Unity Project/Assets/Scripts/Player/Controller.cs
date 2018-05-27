/*
 * FILE: Controller.cs
 * CREATED ON: 13TH May 2018
 * CREATED BY: Aditya Subramanian
 * EDITED BY: Aditya Subramanian, 
 * PURPOSE: Creating a player controller behavior.
*/
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float fXMovement;
    public float fZMovement;
    public float fYJump;

    public bool bIsJumpingUp;
    public bool bIsFallingDown;

    public float fMovementSpeed;
    public float fJumpSpeed;
    public float fJumpMaxSpeed;

    public float fJumpInput;

    public Vector3 v3PlayerPosition;

    // Use this for initialization
    void Start ()
    {
        fYJump = 0.0f;
        bIsJumpingUp = false;
        bIsFallingDown = false;
        v3PlayerPosition = this.transform.position;
    }

    [MethodImpl(MethodImplOptions.NoOptimization)]
    // Update is called once per frame
    void FixedUpdate ()
    {
        fXMovement = Input.GetAxis("Horizontal");
        fZMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(fXMovement, 0.0f, fZMovement);
        Vector3 jumping = new Vector3(0.0f, 0.0f, 0.0f);

        GetComponent<Rigidbody>().velocity = movement * fMovementSpeed;

        if (bIsFallingDown)
        {
            Debug.Log("Is Falling Down");
            return;
        }

        if (bIsJumpingUp)
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            fYJump = fJumpInput;
            fJumpSpeed -= 0.095f;

            jumping.y = fYJump;
            GetComponent<Rigidbody>().velocity += jumping * fJumpSpeed;
            return;
        }

        if (fJumpSpeed < 0.0f)
        {
            fJumpSpeed = 0.0f;
            bIsJumpingUp = false;
            bIsFallingDown = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;

            jumping.y = -1.0f;
            GetComponent<Rigidbody>().velocity += jumping * fJumpMaxSpeed;
            return;
        }
    }

    private void JumpingEnable(Collision collision)
    {
        bIsFallingDown = false;
        if (Mathf.Abs(fJumpInput = Input.GetAxis("Jump")) >= 0.01f)
        {
            Debug.Log("Is Jumping!!");
            Physics.IgnoreCollision(collision.collider, this.GetComponent<Collider>(), true);
            bIsJumpingUp = true;
            fJumpSpeed = fJumpMaxSpeed;
        }
    }

    private void DeathEnable(Collision collision)
    {
        Debug.Log("Player Died!");
        this.transform.position = v3PlayerPosition;
        bIsFallingDown = false;
        bIsJumpingUp = false;
    }

    private void GroundedReset(Collision collision)
    {
        fYJump = 0.0f;
        fJumpSpeed = 0.0f;
        bIsJumpingUp = false;
        bIsFallingDown = true;
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            DeathEnable(collision);
            return;
        }

        if (collision.gameObject.tag == "Ground")
        {
            GroundedReset(collision);
            return;
        }

        if (collision.gameObject.tag == "StationaryObstacle")
        {
            GroundedReset(collision);
            return;
        }

        if (collision.gameObject.tag == "MovingObstacle")
        {
            GroundedReset(collision);
            return;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(bIsJumpingUp)
        {
            return;
        }

        if (collision.gameObject.tag == "Ground")
        {
            JumpingEnable(collision);
            return;
        }

        if (collision.gameObject.tag == "StationaryObstacle")
        {
            JumpingEnable(collision);
            return;
        }

        if (collision.gameObject.tag == "MovingObstacle")
        {
            JumpingEnable(collision);
            return;
        }
    }
}

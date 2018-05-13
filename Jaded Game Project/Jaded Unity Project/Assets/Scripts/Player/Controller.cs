/*
 * FILE: Controller.cs
 * CREATED ON: 13TH May 2018
 * CREATED BY: Aditya Subramanian
 * EDITED BY: Aditya Subramanian, 
 * PURPOSE: Creating a player controller behavior.
*/
using System.Collections;
using System.Collections.Generic;
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

    public Vector3 v3JumpLimiterPosition;

    public Vector3 v3PlayerPosition;

    // Use this for initialization
    void Start ()
    {
        fYJump = 0.0f;
        bIsJumpingUp = false;
        bIsFallingDown = false;
        v3PlayerPosition = this.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        fXMovement = Input.GetAxis("Horizontal") * Time.deltaTime * fMovementSpeed;
        fZMovement = Input.GetAxis("Vertical") * Time.deltaTime * fMovementSpeed;

        if(bIsJumpingUp)
        {
        }

        if(bIsFallingDown)
        {
            Debug.Log("Is Falling Down");
        }

        this.transform.Translate(fXMovement, fYJump, fZMovement);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Death")
        {
            Debug.Log("Player Died!");
            this.transform.position = v3PlayerPosition;
            bIsFallingDown = false;
            bIsJumpingUp = false;
            return;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider == null)
        {
            if (!bIsJumpingUp)
            {
                //In free fall!
                bIsFallingDown = true;
            }
            else
            {
                //In jumping mode!
                bIsFallingDown = false;
            }
            return;
        }

        if (collision.collider.tag == "Ground")
        {
            if(Input.GetAxis("Jump") != 0.0f)
            {
                Debug.Log("Is Jumping!!");
                collision.collider.enabled = false;
                bIsJumpingUp = true;
            }

            return;
        }
    }
}

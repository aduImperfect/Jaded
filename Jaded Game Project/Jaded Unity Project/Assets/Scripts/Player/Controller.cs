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

    public float SpeedMultiplier;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        fXMovement = Input.GetAxis("Horizontal") * Time.deltaTime * SpeedMultiplier;
        fZMovement = Input.GetAxis("Vertical") * Time.deltaTime * SpeedMultiplier;

        fYJump = Input.GetAxis("Jump") * Time.deltaTime * SpeedMultiplier;

        this.transform.Translate(fXMovement, fYJump, fZMovement);
	}
}

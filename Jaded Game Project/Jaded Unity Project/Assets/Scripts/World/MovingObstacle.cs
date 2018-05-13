using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float fXMovement;
    public float fYMovement;
    public float fZMovement;
    public float fMoveInput;

    public float fMovementSpeed;
    public float fMovementMaxSpeed;
    public bool bToggleDirectionZInwards;

	// Use this for initialization
	void Start ()
    {
        bToggleDirectionZInwards = false;
        fMovementSpeed = fMovementMaxSpeed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(fMovementSpeed < 0.0f)
        {
            bToggleDirectionZInwards = !bToggleDirectionZInwards;
            fMovementSpeed = fMovementMaxSpeed;
        }

        if (bToggleDirectionZInwards)
        {
            fYMovement = -fMoveInput * Time.deltaTime * fMovementSpeed;
            fMovementSpeed -= 0.05f;
        }
        else
        {
            fYMovement = fMoveInput * Time.deltaTime * fMovementSpeed;
            fMovementSpeed -= 0.05f;
        }
        this.transform.Translate(fXMovement, fYMovement, fZMovement);
    }
}

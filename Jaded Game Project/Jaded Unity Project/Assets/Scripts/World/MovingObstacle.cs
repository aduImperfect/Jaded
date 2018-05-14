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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 v3PlayerPos = collision.gameObject.transform.position;
            Vector3 v3CurrentToPlayer = v3PlayerPos - this.transform.position;

            float fMagnitude = v3CurrentToPlayer.magnitude;

            Vector3 v3CurrentYAxis = Vector3.zero;
            v3CurrentYAxis.y = 1.0f;

            Vector3 v3CurrentToPlayerNormalized = v3CurrentToPlayer.normalized;
            Vector3 v3CurrentYAxisNormalized = v3CurrentYAxis.normalized;

            //A.B.cos(theta).
            float fDotProduct = Vector3.Dot(v3CurrentToPlayerNormalized, v3CurrentYAxisNormalized);

            float fAngle = Mathf.Acos(fDotProduct) * Mathf.Rad2Deg;

            Debug.Log("DotProduct: " + fDotProduct + ", Angle: " + fAngle);
        }
    }
}

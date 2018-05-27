using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReverse : MonoBehaviour {

    Stack<Vector3> transformStack = new Stack<Vector3>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire1") == true)
        {
            if(transformStack.Count != 0)
            {
                this.transform.position = transformStack.Pop();
                return;
            }
        }

        transformStack.Push(this.transform.position);
    }
}

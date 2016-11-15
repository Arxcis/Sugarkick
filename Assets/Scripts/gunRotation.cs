﻿using UnityEngine;
using System.Collections;

public class gunRotation : MonoBehaviour {

    Transform gunTrans;

    float gunAngle;
    float xAxis = 0.0F;
	float yAxis = 0.0F;
    public float rotationSpeed = 10.0F;
	Vector2 direction = new Vector2(0,0);

    // Use this for initialization
    void Start ()
    {
        gunTrans = GetComponent<Transform>();
    }

	// Update is called once per frame
	void Update ()
    {	
				// GetAxisRawMakes sure that input is not smoothed
				// Keyboard buttons are either 1 and 0
				// https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html
		yAxis = Input.GetAxisRaw("Horizontal");
		xAxis = Input.GetAxisRaw("Vertical");
		if (yAxis != 0 || xAxis != 0){
			direction.x = xAxis;
			direction.y = yAxis;

					// We have left to
					// Prevent gun from resetting to default position each time the 
					// player lets of the throttle
			gunAngle = Vector2.Angle (Vector2.right, direction);

		 	       // Vector2.angle() only outputs positive angles
					// Depending on the configuration of x and y axis,
					// in two cases we have to make the gunAngle negative
			if (xAxis <  0 && yAxis >= 0) {    gunAngle *= -1;     }
			if (xAxis >= 0 && yAxis >= 0) {    gunAngle *= -1;     }

			Debug.Log("yAxis: " + yAxis + "  xAxis: " + xAxis + "  angle: " + gunAngle + '\n');
					// eulerAngles can be assigned a Vector3
			gunTrans.localRotation = Quaternion.Euler(0, 0, gunAngle);
		}

    }
}

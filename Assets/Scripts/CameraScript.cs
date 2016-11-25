// --------------------------------------------------------------------------------- //
// Filename    : CameraScript_jonas.cs
// Project     : Sugarkick
// Created by  : Jonas Solsvik
// Date        : ?
// Description : This script serves as a testing ground for different ways to
//                control the camera. 
//                It calculates the 'center of mass' of all the players in the scene,
//                for centering the camera. It also finds the maximum distance between
//                any player and 'center of mass'. Moves the cameras rigidbody2d for a 
//                smooth experience

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent (typeof(Rigidbody2D))]
public class CameraScript : MonoBehaviour {

    Main main;                            // GameState

	public float minimumZoom    = 15.0F;
	public float zoomScaler     = 0.3F;
	public float zoomExponent   = 0.8F;
	public float cameraDeadzone = 2.0F;
	public float moveScaler     = 0.1F;

    Camera      cam;
    Transform   camTrans;
	Rigidbody2D camRigid;

    float      xSum, ySum;
    float      maxDistance;
    float      camSize;  
	float      temp = 0;
	float      camDistance;

    List<Transform> playerTransforms = new List<Transform>();
    Vector2         centerOfMass     = Vector2.zero;             // Formula @ http://hyperphysics.phy-astr.gsu.edu/hbase/cm.html
	Vector2         camDirection     = Vector2.zero;

	void Start ()
    {
      main     = GameObject.Find("Camera").GetComponent<Main>();
      camTrans = gameObject.GetComponent<Transform>();      // Camera transform
	  camRigid = gameObject.GetComponent<Rigidbody2D>();    // Camera transform
      cam      = GetComponent<Camera>();                    // To manipulate camsize
	
      foreach( GameObject player in main.Players() ) {             // Getting all players transforms
			playerTransforms.Add(player.transform);
      }
	  camTrans.position = new Vector3 (0,0,-1);           // Make sure that cam is above the map
	}

      	                                                  // Update is called once per frame
	void LateUpdate ()
    {
		xSum = 0; ySum = 0; maxDistance=0;            // Reset data

		ComputeCenterOfMass();
        ComputeMaxDistance ();
		SetCamSize ();
		SetCamVelocity();
    }                                                               
		
	void SetCamVelocity() {                      				// Gets distance between camera and mass center. Starts moving towards it with a sqrMagnitude scale.
		camDistance  = Vector2.Distance( centerOfMass, camTrans.position );
		camDirection = (centerOfMass - (Vector2)camTrans.position); 

		if (camDistance < cameraDeadzone) {
			camRigid.velocity = Vector2.zero;
		} else {
			camRigid.velocity = camDirection * camDirection.sqrMagnitude * moveScaler;
		}
	}

	void SetCamSize() 
	{
		if (maxDistance <= minimumZoom) {						 // CamSize has to be adjusted and is a function of maxDistance
			cam.orthographicSize = minimumZoom;
		} else {
			cam.orthographicSize = (Mathf.Pow(maxDistance - minimumZoom, zoomExponent) * zoomScaler) + minimumZoom;
		}
	}
		
    void ComputeMaxDistance ()                    // Calculate
    {
		foreach (Transform playTrans in playerTransforms) {
			temp = ((centerOfMass - new Vector2 (playTrans.position.x, playTrans.position.y)).magnitude);
			if (temp > maxDistance) {
				maxDistance = temp;
			}
		}
		maxDistance = maxDistance * 2; 		   // Max is the radius of the player with maximum distance to
    }                                          // center of Mass. Multiply with 2 to find how wide the camera needs to scale.

	void ComputeCenterOfMass()
	{
		foreach (Transform playTrans in playerTransforms) {
			xSum += playTrans.position.x;
			ySum += playTrans.position.y;
		}
		centerOfMass.x = xSum / main.Players ().Count;             // Center of mass calculation
		centerOfMass.y = ySum / main.Players ().Count;
	}
}

// EOF

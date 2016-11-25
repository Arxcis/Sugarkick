// --------------------------------------------------------------------------------- //
// Filename    : CameraScript_jonas.cs
// Project     : Sugarkick
// Created by  : Jonas Solsvik
// Date        : ?
// Description : This script serves as a testing ground for different ways to
//                control the camera. The current implementation is not complete.
//                It calculates the 'center of mass' of all the players in the scene,
//                for centering the camera. It also finds the maximum distance between
//                any player and 'center of mass'.

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript_jonas : MonoBehaviour {

    Main main;                            // GameState

	public float minimumZoom;
	public float zoomScaler;
	public float zoomExponent;

    Camera           cam;
    Transform   camTrans;

    float       xSum, ySum;
    float      maxDistance;
    float      camDistance;

    List<float>     distances        = new List<float>();
    List<Transform> playerTransforms = new List<Transform>();
    Vector2 centerOfMass = new Vector2(0, 0);             // Formula @ http://hyperphysics.phy-astr.gsu.edu/hbase/cm.html

	void Start ()
    {
      main     = GameObject.Find("Camera").GetComponent<Main>();
      camTrans = gameObject.GetComponent<Transform>();    // Camera transform
      cam      = GetComponent<Camera>();                  // To manipulate camsize

      foreach( GameObject player in main.Players() ) {             // Getting all players transforms
          playerTransforms.Add(player.GetComponent<Transform>());
      }
	}
      	                                                // Update is called once per frame
	void Update ()
    {
        xSum = 0; ySum = 0; distances.Clear();                    // Reset data

        ComputeCenterOfMass( ref centerOfMass );
        ComputeMaxDistance( ref distances, ref maxDistance );

		Debug.Log ("MaxDistance:" + maxDistance);
		if (maxDistance <= minimumZoom) {
			camDistance = minimumZoom;
		} else {
			camDistance = (Mathf.Pow(maxDistance - minimumZoom, zoomExponent) * zoomScaler) + minimumZoom;
		}

        camTrans.position = main.ToVector3( centerOfMass, -1);    // Center of mass = camera position
        cam.orthographicSize =  camDistance;                      // Camera size varies depending on how
  }                                                               // far the players away from each other.

  void ComputeMaxDistance(ref List<float> distances, ref float max)
  {
    foreach( Transform playTrans in playerTransforms ) {
			distances.Add ((centerOfMass - new Vector2 (playTrans.position.x, playTrans.position.y)).magnitude);
    }
	max = GetMax( distances ) * 2; 		 // GetMax the radius of the player with maximum distance to
  }                                              // center of Mass. Multiply with 2 to find how wide the camera needs to scale.

  void ComputeCenterOfMass(ref Vector2 center)
  {
    foreach( Transform playTrans in playerTransforms ) {
      xSum += playTrans.position.x;
      ySum += playTrans.position.y;
    }
	center.x = xSum / main.Players().Count;             // Center of mass calculation
	center.y = ySum / main.Players().Count;
  }

  // Gets the max number from a list
  float GetMax(List<float> numbers)
  {
	float max = 0;
    foreach(float num in numbers) {
      if (num > max) {
        max = num;
      }
    }
    return max;
  }

}

// EOF

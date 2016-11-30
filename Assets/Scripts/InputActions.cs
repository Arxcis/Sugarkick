// --------------------------------------------------------------------------------- //
// Filename    : Actions.cs
// Project     : Sugarkick
// Created by  : Jonas Solsvik
// Date        : 29.11.2016
// Attached to : Main camera
// Description : This script is a general way of mapping the inputaxes of the player
//                to any function that is in relation to the player.
// Note        : The ordering of enum Device and DeviceNames has to be kept in sync (1:1 realtionship)
// 								for the initialization to work.



using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InputActions : MonoBehaviour {

	// Public											// The next two attributes has to have a 1:1 realtionship
	public enum Device : int {		// Used to access correct slot in string[] device names
		KeyboardMouse,
		Joy1,
		Joy3,
		Joy2,
		Joy4,
		Keyboard
	};

	string[] DeviceNames = {				// Names that works as keys to access the Input.GetAxis(name+axis)
		"KeyboardMouse",
		"Joy1",
		"Joy3",
		"Joy2",
		"Joy4",
		"Keyboard"
	};

	public static bool keyboardOn = true;													  // On/off keyboard 1 and 2
	public static List<Device> activeDevices = new List<Device>();   // Maps players to their respective device.

	// Private
	Vector2 moveInput   = Vector2.zero;							// Stores temp values for all the input axes
	Vector2 aimInput    = Vector2.zero;
	Vector2 mouseInput  = Vector3.zero;
	float   fireInput   = 0.0F;
	GunScript activeGun;                          // temp value


	// Use this for initialization
	void Start ()
	{
		MapActiveDevices();           // Sets up each player with an active input device
	}



	void FixedUpdate ()
	{
		for (int i = 0; i < Main.Players().Count; i++)	  	// Wanted to use a foreach here, but player actually never used
		{
			MoveAction(i);
			AimAction(i);
			FireAction(i);
		}
	}


	public static void MapActiveDevices()
	{
        activeDevices.Clear();                                  //clears the list to it can be re-written to.

		int k = (keyboardOn) ? 0 : 1;													// Maps players to a input device
		for( int i = 0; i < Main.Players().Count; i++ ) {		  //
			activeDevices.Add( (Device) k );   									// Cast to enum
			k++;
            print("Input Actions: " + k);
		}
	}


	void MoveAction( int i )
	{
				// -------------------- Move ------------------------
				moveInput.x = Input.GetAxisRaw( DeviceNames[ (int)activeDevices[i] ] + "_MoveHorizontal");
				moveInput.y = Input.GetAxisRaw( DeviceNames[ (int)activeDevices[i] ] + "_MoveVertical"  );

				Main.Player<MovePlayer>(i).Move( moveInput, i);

				//Debug.Log( DeviceNames[ (int)activeDevices[i] ] + "_Move" + moveInput);
	}

	void AimAction( int i )
	{
		// --------------------- Aim --------------------------
		if( activeDevices[i] == Device.KeyboardMouse ) {            				// Mouse aiming

			mouseInput = Input.mousePosition;

			activeGun = Main.Player<PuppetManip>(i).GetActiveGunScript();
			if (activeGun != null) {
				activeGun.MouseAimUpdate(mouseInput, i);
			}
			//Debug.Log(DeviceNames[ (int)activeDevices[i] ] + "_Mouse" + mouseInput);
		}
		else																													// Normal aiming
		{
			aimInput.x = Input.GetAxisRaw( DeviceNames[ (int)activeDevices[i] ] + "_AimHorizontal");
			aimInput.y = Input.GetAxisRaw( DeviceNames[ (int)activeDevices[i] ] + "_AimVertical"  );

			activeGun = Main.Player<PuppetManip>(i).GetActiveGunScript();
			if (activeGun != null) {
				activeGun.KeyAimUpdate( aimInput );
			}
			//Debug.Log( DeviceNames[ (int)activeDevices[i] ] + "_Aim" + aimInput);
		}
	}

	void FireAction( int i )
	{
		// --------------------- Fire ------------------------------
		fireInput = Input.GetAxisRaw( DeviceNames[ (int)activeDevices[i] ] + "_Fire" );

		activeGun = Main.Player<PuppetManip>(i).GetActiveGunScript();

		if(activeGun !=  null) {
			if( fireInput > 0.4) {
				activeGun.Fire(fireInput, i);
			}
		}
		i++;
		//Debug.Log( DeviceNames[ (int)activeDevices[i] ] + "_Fire" + fireInput);
	}

	// End of class
}

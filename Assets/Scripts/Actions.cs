using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Actions : MonoBehaviour {

	// Public
	public enum Device : int {		// Used to access correct slot in string[] device names
		Keyboard1,
		Joy1,
		Joy2,
		Joy3,
		Joy4,
		Keyboard2
	};

	public bool keyboardOn = false;													  // On/off keyboard 1 and 2
	public List<Device> activeDevices = new List<Device>();   // Maps players to their respective device.

	// Private
	string[] DeviceNames = {				// Names that works as keys to access the Input.GetAxis(name+axis)
		"Keyboard1",
		"Joy1",
		"Joy2",
		"Joy3",
		"Joy4",
		"Keyboard2"
	};

	Vector2 moveInput   = Vector2.zero;							// Stores temp values for all the input axes
	Vector2 aimInput    = Vector2.zero;
	Vector2 mouseInput  = Vector3.zero;
	float   fireInput   = 0.0F;
	GunScript activeGun;


	// Use this for initialization
	void Start ()
	{
		MapActiveDevices();           // Sets up each player with an active input device
	}



	void FixedUpdate ()
	{
		for (int i = 0; i < Main.Players().Count; i++)	  	// Wanted to use a foreach here, but player actually never used
		{
			CallMove(i);
			CallAim(i);
			CallFire(i);
		}
	}



	void MapActiveDevices()
	{
		int k = (keyboardOn) ? 0 : 2;													// Maps players to a input device
		for( int i = 0; i < Main.Players().Count; i++ ) {		  //
			activeDevices.Add( (Device) k );   									// Cast to enum
			k++;
		}
	}


	void CallMove( int i )
	{
				// -------------------- Move ------------------------
				moveInput.x = Input.GetAxisRaw( DeviceNames[ (int)activeDevices[i] ] + "_MoveHorizontal");
				moveInput.y = Input.GetAxisRaw( DeviceNames[ (int)activeDevices[i] ] + "_MoveVertical"  );

				Main.Player<MovePlayer>(i).Move( moveInput, i);

				// Debug.Log( DeviceNames[ (int)activeDevices[i] ] + "_Move" + moveInput);
	}

	void CallAim( int i )
	{
		// --------------------- Aim --------------------------
		if( activeDevices[i] == Device.Keyboard1 ) {            				// Mouse aiming

			mouseInput = Input.mousePosition;

			activeGun = Main.Player<PuppetManip>(i).GetActiveGunScript();
			if (activeGun != null) {
				activeGun.MouseAimUpdate(mouseInput, i);
			}
			// Debug.Log(DeviceNames[ (int)activeDevices[i] ] + "_Mouse" + mouseInput);
		}
		else																													// Normal aiming
		{
			aimInput.x = Input.GetAxisRaw( DeviceNames[ (int)activeDevices[i] ] + "_AimHorizontal");
			aimInput.y = Input.GetAxisRaw( DeviceNames[ (int)activeDevices[i] ] + "_AimVertical"  );

			activeGun = Main.Player<PuppetManip>(i).GetActiveGunScript();
			if (activeGun != null) {
				activeGun.KeyAimUpdate(aimInput);
			}
			// Debug.Log( DeviceNames[ (int)activeDevices[i] ] + "_Aim" + aimInput);
		}
	}

	void CallFire( int i )
	{
		// --------------------- Fire ------------------------------
		fireInput = Input.GetAxisRaw( DeviceNames[ (int)activeDevices[i] ] + "_Fire" );

		activeGun = Main.Player<PuppetManip>(i).GetActiveGunScript();
		Debug.Log( DeviceNames[ (int)activeDevices[i] ] + "_Fire" + fireInput);
		if( fireInput > 0.4)
			activeGun.Fire(fireInput, i);
		i++;
	}
	// End of class
}

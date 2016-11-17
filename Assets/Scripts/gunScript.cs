using UnityEngine;
using System.Collections;

public class gunScript : MonoBehaviour {

		// Public:
	public bool setPos      = true;			// Recoil
	public bool addForce    = false;	
	public bool setVelocity = true;

	public int weaponDamage       =   1;			// Shooting
	public float accuracy     = 100.0F;
	public float fireRate     =  50.0F;
	public float knockbackPow = 200.0F;
	public float projectileSpeed = 10.0F;   
	public GameObject bullets;
	public GameObject barrelEnd;
	public GameObject bulletParent;
		// Private:
	GameObject  player; 						// For accessing parent player
	Rigidbody2D     rb;
	Transform gunTrans;

	float recoilX;								// Recoil
	float recoilY; 
	float gunAngle;							    // Shooting
	float xAxis    = 0.0F;
	float yAxis    = 0.0F;
	float fire     = 0.0F;
	float cooldown = 0.0F;

	Vector2 direction = new Vector2(0,0);	    // Rotation
	float   diagonalCompensator = 0.0F;


    void Start ()
    {
		player   = gameObject.transform.parent.gameObject;	// Getting components
		rb       = player.GetComponent<Rigidbody2D> ();
        gunTrans = GetComponent<Transform>();
    }

	void FixedUpdate ()								// Fixed update is frame-rate independent
    {              
        xAxis = Input.GetAxisRaw("AimAxisX");       // GetAxisRawMakes sure that input is not 
        yAxis = Input.GetAxisRaw("AimAxisY");		// Keyboard buttons are either 1 and 0 smoothed
        fire = Input.GetAxisRaw ("Fire3");			// https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html

        if (xAxis != 0 || yAxis != 0){
			direction.x = yAxis;
			direction.y = xAxis;

			gunAngle = Vector2.Angle (Vector2.right, direction);	 // Vector2.angle() only outputs positive angles
																	 // Depending on the configuration of x and y axis,
			if (yAxis <  0 && xAxis >= 0) {    gunAngle *= -1;     } // in two cases we have to make the gunAngle negative
			if (yAxis >= 0 && xAxis >= 0) {    gunAngle *= -1;     } // Vector2.angle (base vector, target vector) -> diff vector

			// Debug.Log("yAxis: " + yAxis + "  xAxis: " + xAxis + "  angle: " + gunAngle + '\n');
             
			gunTrans.localRotation = Quaternion.Euler(0, 0, gunAngle); // Quaternion.Euler accepts regular angles (Grad)
		}

		if (cooldown > 0) {	  
			cooldown--;	   
		}


		if (fire > 0.4 && cooldown <= 0) {
			print ("Firing!"); 

			// The factor which makes a non-straight vector the same magnitude as a straight one.
			// E.G. (1, 1) or (0.7, 0.6) has a greater magnitude than (1, 0) or (0, 1), making the player move faster on diagonals. 
			// Hence, both 0.7 and 0.6 must be multiplied by this factor.
			diagonalCompensator =  (1/(Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y + 0.001F)));

			recoilY = (-1 * direction.y * diagonalCompensator);
			recoilX = (-1 * direction.x * diagonalCompensator);

			if (setPos) {
				player.GetComponent<Transform> ().position += new Vector3 (recoilY * (knockbackPow / 100),  recoilX * (knockbackPow / 100), 0);
			}

			if(addForce){
				rb.AddForce(new Vector2 (recoilY * (knockbackPow / 1), recoilX * (knockbackPow / 1)));
			}

			if(setVelocity){
				rb.velocity = new Vector3 (rb.velocity.x + recoilY * (knockbackPow / 10), rb.velocity.y + recoilX * (knockbackPow / 10), 0);
			}
			shoot ();

			cooldown = 1/(fireRate/1000); 
		}
        //print("Fire: " + fire + "Cooldown: " + cooldown);
    }

	// Creating a prefab, "bullets" set in the inspector. Created at the position of "barrelEnd", also set in the inspector. Then the bullet is parented to "bulletParent", also set in the inspector.
	// The velocity of the bullet is set in the direction of the barrel with a speed of "projectileSpeed", set in the inspector. The bullet spawns with the damage of "weaponDamage".
	void shoot(){
		var bullet = Instantiate (bullets, new Vector3(barrelEnd.GetComponent<Transform>().position.x, barrelEnd.GetComponent<Transform>().position.y, 0), Quaternion.identity) as GameObject;
		bullet.transform.parent = bulletParent.transform;
		bullet.GetComponent<Rigidbody2D> ().velocity = new Vector2 (direction.y*projectileSpeed, direction.x*projectileSpeed);
		bullet.GetComponent<Damage> ().damage = weaponDamage;
	}
}

using UnityEngine;
using System.Collections;

public class gunScript : MonoBehaviour {

		// Public:
	public bool setPos      = true;			// Recoil
	public bool addForce    = false;	
	public bool setVelocity = true;

	public float damage       =   1.0F;			// Shooting
	public float accuracy     = 100.0F;
    public float fireRate     =  50.0F;
    public float knockbackPow = 200.0F;
    // public GameObject bullets; (Jone): Unity complained that bullet wasn't defined. 
    public float rotationSpeed = 10.0F;			// Rotation

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

														// (jonas) This part needs some more commenting
			diagonalCompensator =  (1/(Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y + 0.001F)));

			recoilY = (-1 * direction.y * diagonalCompensator);
			recoilX = (-1 * direction.x * diagonalCompensator);

			if (setPos) {
				player.GetComponent<Transform> ().position += new Vector3 (recoilX * (knockbackPow / 300), 
																		   recoilY * (knockbackPow / 300), 0);
			}
			if(addForce){
				rb.AddForce(new Vector2 (recoilY * (knockbackPow / 1),
										 recoilX * (knockbackPow / 1)));
			}

			if(setVelocity){
				rb.velocity = new Vector3 (recoilY * (knockbackPow / 10),
									       recoilX * (knockbackPow / 10),0);
			}

			// Instantiate(bullets, new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y, 0), Quaternion.identity);
			// Missile clone = (Missile)Instantiate(missilePrefab(bullets, new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y, 0), Quaternion.identity);


			cooldown = 1/(fireRate/1000); 
		}
        print("Fire: " + fire + "Cooldown: " + cooldown);
    }
}

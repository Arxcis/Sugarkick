using UnityEngine;
using System.Collections;

public class gunScript : MonoBehaviour {

	                                                            // Accessing player
	GameObject player; 

	                                                            // Types of recoil
	public bool setPos = false;
	public bool addForce = false;
	public bool setVelocity = false;

	                                                            // Rotation
    Transform gunTrans;
    float gunAngle;
    float xAxis = 0.0F;
	float yAxis = 0.0F;
    float fire = 0.0F;
    public float rotationSpeed = 10.0F;
	Vector2 direction = new Vector2(0,0);

	                                                            // Shooting
	                                                            //public GameObject bullets; (Jone): Unity complained that bullet wasn't defined. 
	                                                            //Bullet missile;

	public float damage = 1.0F;
	public float accuracy = 100.0F;
    public float fireRate = 10;
    public float knockbackPow = 200.0F;
    float cooldown = 0;

                                                                // Misc.
    Rigidbody2D rb;
	float diagonalCompensator = 0.0F;

                                                                // Use this for initialization
    void Start ()
    {
		player = gameObject.transform.parent.gameObject;
        gunTrans = GetComponent<Transform>();
		rb = player.GetComponent<Rigidbody2D> ();
    }

	                                                            // Update is called once per frame
	void FixedUpdate ()
    {
                                                                // GetAxisRawMakes sure that input is not smoothed
                                                                // Keyboard buttons are either 1 and 0
                                                                // https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html
        xAxis = Input.GetAxisRaw("AimAxisX");
        yAxis = Input.GetAxisRaw("AimAxisY");
        fire = Input.GetAxisRaw("Fire");

        if (xAxis != 0 || yAxis != 0){
			direction.x = yAxis;
			direction.y = xAxis;

					                                            // We have left to
					                                            // Prevent gun from resetting to default position each time the 
					                                            // player lets of the throttle
			gunAngle = Vector2.Angle (Vector2.right, direction);

		 	                                                    // Vector2.angle() only outputs positive angles
					                                            // Depending on the configuration of x and y axis,
					                                            // in two cases we have to make the gunAngle negative
			if (yAxis <  0 && xAxis >= 0) {    gunAngle *= -1;     }
			if (yAxis >= 0 && xAxis >= 0) {    gunAngle *= -1;     }

			//Debug.Log("yAxis: " + yAxis + "  xAxis: " + xAxis + "  angle: " + gunAngle + '\n');

					                                            // eulerAngles can be assigned a Vector3
			gunTrans.localRotation = Quaternion.Euler(0, 0, gunAngle);
		}

		if (cooldown > 0) {
			cooldown--;
		}


		if (fire > 0.4 && cooldown <= 0) {
			print ("Firing!"); 


			diagonalCompensator =  (1/(Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y)));

			if (setPos) {
				player.GetComponent<Transform> ().position += new Vector3 (-1 * direction.y * (knockbackPow / 300) * diagonalCompensator, -1 * direction.x * (knockbackPow / 300) * diagonalCompensator, 0);
			}
			if(addForce){
				rb.AddForce(new Vector2 (-1 * direction.y * (knockbackPow / 1) * diagonalCompensator, -1 * direction.x * (knockbackPow / 1) * diagonalCompensator));
			}

			if(setVelocity){
				rb.velocity = (new Vector2 (-1 * direction.y * (knockbackPow / 10) * diagonalCompensator, -1 * direction.x * (knockbackPow / 10) * diagonalCompensator));
			}

			//Instantiate(bullets, new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y, 0), Quaternion.identity);
			//Missile clone = (Missile)Instantiate(missilePrefab(bullets, new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y, 0), Quaternion.identity);


			cooldown = 1/(fireRate/1000); 
		}
        print("Fire: " + fire + "Cooldown: " + cooldown);

    }
}

using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    Main main;
    GameObject cam;
    Camera camscript;
    public AudioClip GunSound;


        // Public:
    public bool setPos      = true;         // Recoil
    public bool addForce    = false;
    public bool setVelocity = true;

    public bool truePiercing = false;
    public int pierceNumber;

    public int   weaponDamage    =   1;         // Shooting
    public float accuracy        = 100.0F;
    public float fireRate        =  50.0F;
    public float knockbackPow    = 300.0F;
    public float projectileSpeed = 10.0F;
    public int bulletLifeTime = 1000;
    public float bulletSize = 1f;

    public GameObject bulletSeed;
    public GameObject barrelEnd;
	  public GameObject bulletParent;

    // Private:
    Transform gunTrans;

    float gunAngle;                             // Shooting
    float fire     = 0.0F;
    float cooldown = 0.0F;

    // Vector2 inputVec  = new Vector2(0,0);       // Direction input Vector
    Vector3 mousePos  = new Vector3(0,0,0);
    Vector2 recoilVec = new Vector2(0,0);       // Recoil vector

    Vector3 facingMouseVector  = new Vector3(0,0,0);

    void Start ()
    {
      cam = GameObject.Find("Camera");
      camscript = cam.GetComponent<Camera>();

      main = cam.GetComponent<Main>();
      gunTrans = GetComponent<Transform>();
    }

    void FixedUpdate ()                             // Fixed update is frame-rate independent
    {

      if (cooldown > 0) {
          cooldown--;
      }

      fire = Input.GetAxisRaw ("Fire");           // https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html

      if (fire > 0.4 && cooldown <= 0) {      // Scaling recoilVec with negative 1 to send the
                                              // player in opposite direction
          recoilVec = Main.GetUnitVector2( (gunAngle+90) * Mathf.Deg2Rad ) * -1;

          if (setPos) {                       // Casting vec2 to vec3 before adding to position
              Main.Player<Transform>(0).position += Main.ToVector3(recoilVec * (knockbackPow / 300.0F));
          }

          if(addForce){
              Main.Player<Rigidbody2D>(0).AddForce(recoilVec * (knockbackPow / 1));
          }

          if(setVelocity){
              Main.Player<Rigidbody2D>(0).velocity += recoilVec * (knockbackPow / 10);
          }

          shoot();
          cooldown = 1/(fireRate/1000);
      }

      if (gunAngle > -100 && gunAngle < 110)
          Main.Player<SpriteRenderer>(0, "Head").sprite = main.headBack;   // non-static part of main has to be called through an object.
      else
          Main.Player<SpriteRenderer>(0, "Head").sprite = main.headFront;
  }

	// Creating a prefab, "bullets" set in the inspector. Created at the position of "barrelEnd", also set in the inspector. Then the bullet is parented to "bulletParent", also set in the inspector.
	// The velocity of the bullet is set in the direction of the barrel with a speed of "projectileSpeed", set in the inspector. The bullet spawns with the damage of "weaponDamage".
	void shoot()
  {
        float soundPitch = Random.Range(-0.1f, 0.1f);         // the pitch deviation needs to be on a seperate line due to how the compiler deals with functions.
        SoundManager.instance.bangBang(GunSound, soundPitch); // plays designated gun sound

		float i = Random.Range (-50, 50);  // random float to make the spray deviate a bit.
		recoilVec = Main.GetUnitVector2( (gunAngle+90+ (i/50)*(100/accuracy)) * Mathf.Deg2Rad )*-1; // (i/50) is a float between -1 and 1 - for a random distribution
													    // between the extremals.
													    // (100/accuracy) determines the extremals for the firing diviation.
													    // The bigger the accuracy, the smaller the deviation.
		ProjectileInfo pInfo;
		var bullet = Instantiate (bulletSeed, new Vector3(barrelEnd.GetComponent<Transform>().position.x, barrelEnd.GetComponent<Transform>().position.y, 0), Quaternion.identity) as GameObject;
		bullet.transform.parent = bulletParent.transform;
		pInfo = bullet.GetComponent<ProjectileInfo> ();
		bullet.GetComponent<Rigidbody2D> ().velocity = recoilVec * -1 * projectileSpeed; // Sets the velocity to be a bit random.
		pInfo.damage = weaponDamage;
		pInfo.projectileSpeed = projectileSpeed;
		pInfo.truePiercing = truePiercing;
		pInfo.pierceNumber = pierceNumber;
        pInfo.bulletLifeTime = bulletLifeTime;
        pInfo.bulletSize = bulletSize;

	}

  public void MouseAimUpdate( Vector3 mouseIn, int i )
  {
    mousePos = camscript.ScreenToWorldPoint( mouseIn );
                             // Create Vector2 from the difference in position between mouse and player
    facingMouseVector = new Vector2(mousePos.x - Main.Player<Transform>(i).position.x,
                                    mousePos.y - Main.Player<Transform>(i).position.y);
    gunAngle = Main.GetAngle( Vector2.up, facingMouseVector );
    gunTrans.localRotation = Quaternion.Euler(0, 0, gunAngle);
  }

  public void KeyAimUpdate( Vector2 inVec )
  {
    if (inVec.x != 0 || inVec.y != 0){

        gunAngle = Main.GetAngle( Vector2.up, inVec );
        gunTrans.localRotation = Quaternion.Euler(0, 0, gunAngle); // Quaternion.Euler accepts regular angles (Grad)
    }
  }
}

using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

    Main main;
    GameObject cam;
    Camera camscript;


        // Public:
    public bool setPos      = true;         // Recoil
    public bool addForce    = false;
    public bool setVelocity = true;

    public bool truePiercing = false;
    public int pierceNumber;

    public int   weaponDamage =   1;         // Shooting
    public float accuracy     = 100.0F;
    public float fireRate     =  50.0F;
    public float knockbackPow = 300.0F;
    public float projectileSpeed = 10.0F;

    public GameObject bullets;
    public GameObject barrelEnd;
    public GameObject bulletParent;

    // Private:
    Transform gunTrans;

    float gunAngle;                             // Shooting
    float fire     = 0.0F;
    float cooldown = 0.0F;

    Vector2 inputVec  = new Vector2(0,0);       // Direction input Vector
    Vector2 recoilVec = new Vector2(0,0);       // Recoil vector
    Vector3 mousePos  = new Vector3(0,0,0);
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

      if( main.mouseOn ) {
        mouseAimUpdate();           // Update aim with mouse
      }                             //     or
      else {                        //
        keyAimUpdate();             // Update aim with keys
      }

      if (cooldown > 0) {
          cooldown--;
      }

      fire = Input.GetAxisRaw ("Fire");           // https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html

      if (fire > 0.4 && cooldown <= 0) {      // Scaling recoilVec with negative 1 to send the
                                              // player in opposite direction
          recoilVec = main.GetUnitVector2( (gunAngle+90) * Mathf.Deg2Rad ) * -1;

          if (setPos) {                       // Casting vec2 to vec3 before adding to position
              main.Player<Transform>(0).position += main.ToVector3(recoilVec * (knockbackPow / 300.0F));
          }

          if(addForce){
              main.Player<Rigidbody2D>(0).AddForce(recoilVec * (knockbackPow / 1));
          }

          if(setVelocity){
              main.Player<Rigidbody2D>(0).velocity += recoilVec * (knockbackPow / 10);
          }

          shoot();
          cooldown = 1/(fireRate/1000);
      }

      if (gunAngle > -100 && gunAngle < 110)
          main.Player<SpriteRenderer>(0, "Head").sprite = main.headBack;
      else
          main.Player<SpriteRenderer>(0, "Head").sprite = main.headFront;
  }

	// Creating a prefab, "bullets" set in the inspector. Created at the position of "barrelEnd", also set in the inspector. Then the bullet is parented to "bulletParent", also set in the inspector.
	// The velocity of the bullet is set in the direction of the barrel with a speed of "projectileSpeed", set in the inspector. The bullet spawns with the damage of "weaponDamage".
	void shoot(){
		ProjectileInfo pInfo;
		var bullet = Instantiate (bullets, new Vector3(barrelEnd.GetComponent<Transform>().position.x, barrelEnd.GetComponent<Transform>().position.y, 0), Quaternion.identity) as GameObject;
		bullet.transform.parent = bulletParent.transform;
		pInfo = bullet.GetComponent<ProjectileInfo> ();
		bullet.GetComponent<Rigidbody2D> ().velocity = recoilVec * -1 * projectileSpeed;
		pInfo.damage = weaponDamage;
		pInfo.projectileSpeed = projectileSpeed;
		pInfo.truePiercing = truePiercing;
		pInfo.pierceNumber = pierceNumber;
	}

  void mouseAimUpdate() {
    mousePos = camscript.ScreenToWorldPoint(Input.mousePosition); // Returns Vector
                                         // Create Vector2 from the difference in position between mouse and player
    facingMouseVector = new Vector2(mousePos.x - main.Player<Transform>(0).position.x,
                                    mousePos.y - main.Player<Transform>(0).position.y);
    gunAngle = main.GetAngle( Vector2.up, facingMouseVector );
    gunTrans.localRotation = Quaternion.Euler(0, 0, gunAngle);
  }

  void keyAimUpdate() {

    inputVec.x = Input.GetAxisRaw("AimAxisX");       // GetAxisRawMakes sure that input is not
    inputVec.y = Input.GetAxisRaw("AimAxisY");       // Keyboard buttons are either 1 and 0 smoothed
    fire = Input.GetAxisRaw ("Fire");           // https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html

    if (inputVec.x != 0 || inputVec.y != 0){

        gunAngle = main.GetAngle( Vector2.up, inputVec );
        gunTrans.localRotation = Quaternion.Euler(0, 0, gunAngle); // Quaternion.Euler accepts regular angles (Grad)
    }
  }
}

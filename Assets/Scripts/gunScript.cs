using UnityEngine;
using System.Collections;

public class gunScript : MonoBehaviour {
    
    Main main;  

        // Public:
    public bool setPos      = true;         // Recoil
    public bool addForce    = false;    
    public bool setVelocity = true;

    public float damage       =   1.0F;         // Shooting
    public float accuracy     = 100.0F;
    public float fireRate     =  50.0F;
    public float knockbackPow = 200.0F;
    // public GameObject bullets; (Jone): Unity complained that bullet wasn't defined. 
    public float rotationSpeed = 10.0F;         // Rotation

    // Private:                               
    Transform gunTrans;

    float gunAngle;                             // Shooting
    float fire     = 0.0F;
    float cooldown = 0.0F;

    Vector2 inputVec  = new Vector2(0,0);       // Direction input Vector
    Vector2 recoilVec = new Vector2(0,0);       // Recoil vector

    void Start ()
    {
        main = GameObject.Find("Camera").GetComponent<Main>();
        gunTrans = GetComponent<Transform>();
    }

    void FixedUpdate ()                             // Fixed update is frame-rate independent
    {              
        inputVec.x = Input.GetAxisRaw("AimAxisX");       // GetAxisRawMakes sure that input is not 
        inputVec.y = Input.GetAxisRaw("AimAxisY");       // Keyboard buttons are either 1 and 0 smoothed
        fire = Input.GetAxisRaw ("Fire");           // https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html

        if (inputVec.x != 0 || inputVec.y != 0){

            gunAngle = main.GetAngle(Vector2.up, inputVec);
            gunTrans.localRotation = Quaternion.Euler(0, 0, gunAngle); // Quaternion.Euler accepts regular angles (Grad)
        }

        if (cooldown > 0) {   
            cooldown--;    
        }


        if (fire > 0.4 && cooldown <= 0) {
            print ("Firing!"); 
                                                // Scaling recoilVec with negative 1 to send the 
                                                // player in opposite direction
            recoilVec = main.DiagonalCompensate( inputVec ) * -1.0F;

            if (setPos) {                       // Casting vec2 to vec3 before adding to position
                main.playerTrans.position += main.ToVector3(recoilVec * (knockbackPow / 300.0F));
            }

            if(addForce){
                main.playerRigi.AddForce(recoilVec * (knockbackPow / 1));
            }

            if(setVelocity){
                main.playerRigi.velocity = recoilVec * (knockbackPow / 10);
            }

            // Instantiate(bullets, new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y, 0), Quaternion.identity);
            // Missile clone = (Missile)Instantiate(missilePrefab(bullets, new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y, 0), Quaternion.identity);


            cooldown = 1/(fireRate/1000); 
        }
        print("Fire: " + fire + "Cooldown: " + cooldown);
    }
}

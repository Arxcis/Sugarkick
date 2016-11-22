using UnityEngine;
using System.Collections;

public class GunPickupScript : MonoBehaviour {

    public GameObject[ ] guns; // gun.length should be equal to gunDropChanse.length
    [Range( 0.0f, 100.0f )]
    public float[ ] gunDropChanse; //element "n+1" max should be dependent of element "n" max to get a % chanse for that gun to drop.

    // Use this for initializiation
    void Start( ) {

    }

    // Update is called once per frame
    void Update( ) {

    }
}
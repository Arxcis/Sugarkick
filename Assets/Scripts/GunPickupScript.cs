using UnityEngine;
using System.Collections;


/*
######################################################################################################     #######      #        ######     ######      |#|
######################################################################################################        #        # #      #          ##     #     |#| 
###                                                                                                ###        #       #   #     #  ###       #####      |#|
###    use the tag system to decide what gun the player gets from picking up this weapon pickup.   ###        #      #######    #    #     #     ##      _
###                                       yeaya!                                                   ###        #     #       #    #####      ######      |#|
######################################################################################################
######################################################################################################

*/

public class GunPickupScript : MonoBehaviour
{
    public int rotationSpeed = 3;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))              // if collided with a player.
        {

            PuppetManip mnIp = other.GetComponent<PuppetManip>();                        //gets the collided players puppit manip.


            for (int i = 0; i < mnIp.guns.Length; ++i)
            {
                    mnIp.guns[i].SetActive(false);                                      //sets all the guns in guns[] to inactive.
            }
            if      (gameObject.tag.Contains("Rifle")) mnIp.guns[0].SetActive(true);        //Rifle
            else if (gameObject.tag.Contains("Canon")) mnIp.guns[1].SetActive(true);        //Canon
            else if (gameObject.tag.Contains("Spraygun")) mnIp.guns[2].SetActive(true);     //Spraygun

            Destroy(gameObject);
        }
    }
}

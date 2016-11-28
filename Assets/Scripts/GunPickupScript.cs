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

    Acually, scratch that, letss use an int number INTstead XD.
*/

public class GunPickupScript : MonoBehaviour
{
    public int rotationSpeed = 3;
    public int weaponIndex = 0;         // 0 = not set, choose a random    //1 = rifle,    2 = canon,      3 = spraygun
    public int chanceOfSpawning = 7;    // a 0 to INT chance of weapondrop spawning. set to 0 in inspecor to always spawn.
    public Color[] dropColor;
    public Sprite[] dropSprite;

    SpriteRenderer dropRend;
    CircleCollider2D dropColl;
    
    void Start()
    {
        if (Random.Range(0, chanceOfSpawning + 1) == 0)     //plays the lottery. 
        {
            if (dropColor.Length < 3 || dropSprite.Length < 3) print("ERROR: The sprite or color array of this drop is not big enough"); //just in case.s
            if (weaponIndex == 0) weaponIndex = Random.Range(1, 3 + 1); // sets the weapon index to a random index from 1 to 3. Max is exclusive.

            dropRend = GetComponent<SpriteRenderer>();      //gets th spriterenderer in the drop.
            dropColl = GetComponent<CircleCollider2D>();
            dropRend.sprite = dropSprite[weaponIndex - 1];      //sets te rend's sprite.
            dropRend.color = dropColor[weaponIndex - 1];        //sets the rend's color.
            dropColl.enabled = true;                //disabled by defaly so the player cant collide in the same frame is spawns.
        }
        else Destroy(gameObject);       //destroys the game object if it didn't win the lottary.
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))                          // if collided with a player.
        {

            PuppetManip mnIp = other.GetComponent<PuppetManip>();               //gets the collided players puppit manip.
            for (int i = 0; i < mnIp.guns.Length; ++i)
            {
                    mnIp.guns[i].SetActive(false);                              //sets all the guns in guns[] to inactive.
            }
            mnIp.guns[weaponIndex -1].SetActive(true);                             //sets weaponindex as active weapon.
            Destroy(gameObject);
        }
    }
}

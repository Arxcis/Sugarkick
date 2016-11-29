using UnityEngine;
using System.Collections;

public class ProjectileInfo : MonoBehaviour {

	public bool truePiercing;
	public int damage;
	public float projectileSpeed;
	public int pierceNumber;
    public int bulletLifeTime;
    public float bulletSize;

    void Start()
    {
        transform.localScale = new Vector3(bulletSize, bulletSize);
    }

    void FixedUpdate()
    {
        if (bulletLifeTime <= 0) Destroy(gameObject);   // destroys bullet if it runns out of frames to live.

        bulletLifeTime--;                   // reduces the amount of frames the bullet has left to live.    
    }
}

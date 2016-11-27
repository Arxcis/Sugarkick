using UnityEngine;
using System.Collections;

public class GunPickupScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0,0,45)*Time.deltaTime);
	}


    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < Main.Player<PuppetManip>(0).guns.Length; ++i)
            {
                guns[i].SetActive(false);
            }
            switch (other.gameObject.name)
            {
                case "GunDrop1":
                    guns[1].SetActive(true);
                    break;
                case "GunDrop2":
                    guns[2].SetActive(true);
                    break;
                case "GunDrop3":
                    guns[3].SetActive(true);
                    break;
            }
            Destroy(other.gameObject);
            //GameObject.Find(other.gameObject.name).SetActive(true);
            //GameObject.GetChild("Gun").gameObject.setActive(false);
        }
    }

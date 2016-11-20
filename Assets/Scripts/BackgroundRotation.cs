using UnityEngine;
using System.Collections;

public class BackgroundRotation : MonoBehaviour {

    public Transform BackGround1;
    public Transform BackGround2;
    public Transform Particles;
    public float Background1Rotation = 0.2f;
    public float Background2Rotation = 0.1f;
    public float ParticleRotation = 0.15f;

    // Use this for initialization
    void Start () {
        BackGround1.GetComponent<Transform>();
        BackGround2.GetComponent<Transform>();
        Particles.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {

        BackGround1.Rotate(new Vector3(0, 0, Background1Rotation));
        BackGround2.Rotate(new Vector3(0, 0, Background2Rotation));
        Particles.Rotate(0, 0, ParticleRotation);
    }
}

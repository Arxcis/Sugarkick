using UnityEngine;
using System.Collections;

public class CameraScript_johannes : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public float deadZone = 0.0F;
	public float velocityFactor = 0.0F;

	Transform p1;
	Transform p2;
	Transform camT;
	public Vector2 desiredPos;
	public Vector2 distance;
	public Vector2 diff;
	public float dist;
	Camera cam;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		
		p1 = player1.GetComponent<Transform> ();
		p2 = player2.GetComponent<Transform> ();
		cam = gameObject.GetComponent<Camera> ();
		camT = gameObject.GetComponent<Transform> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		diff = new Vector2 (p1.position.x - p2.position.x, p1.position.y - p2.position.y);

		desiredPos = new Vector2 (p1.position.x - 0.5F*diff.x, p1.position.y - 0.5F*diff.y);
		distance = desiredPos - new Vector2(camT.position.x, camT.position.y);
		dist = distance.magnitude;

		cam.orthographicSize = 8 + 1.8F*Mathf.Pow(diff.magnitude, 0.6F);
		if (dist >= deadZone) {
			rb.velocity = new Vector3 ((desiredPos.x - camT.position.x)*dist*dist*velocityFactor, 
									   (desiredPos.y - camT.position.y)*dist*dist*velocityFactor, 0.0F);
			//camT.position = new Vector3 (desiredPos.x, desiredPos.y, camT.position.z);
		}
		else if(dist <= deadZone){
			rb.velocity = new Vector2 (0.0F, 0.0F);
		}
	}
}

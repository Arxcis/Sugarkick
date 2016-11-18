using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public float deadZone = 0.0F;

	Transform p1;
	Transform p2;
	Transform camT;
	public Vector2 desiredPos;
	public Vector2 distance;
	public Vector2 diff;
	public float dist;
	Camera cam;

	// Use this for initialization
	void Start () {
		p1 = player1.GetComponent<Transform> ();
		p2 = player2.GetComponent<Transform> ();
		cam = gameObject.GetComponent<Camera> ();
		camT = gameObject.GetComponent<Transform> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		diff = new Vector2 (p1.position.x - p2.position.x, p1.position.y - p2.position.y);

		desiredPos = new Vector2 (p1.position.x - 0.5F*diff.x, p1.position.y - 0.5F*diff.y);
		distance = new Vector2 (desiredPos.x - camT.position.x, desiredPos.y - camT.position.y);
		dist = Mathf.Sqrt(distance.x * distance.x + distance.y * distance.y);

		if(dist >= deadZone){
			//move ();

			camT.position = new Vector3 (desiredPos.x, desiredPos.y, camT.position.z);
			cam.orthographicSize = 10+0.4F*Mathf.Sqrt(diff.x*diff.x + diff.y*diff.y);
		}

	}

	/*void move(){
		int k = dist;
		for (int i = 0; i < k; i++) {
			
		}
	}*/
}

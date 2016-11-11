using UnityEngine;
using System.Collections;

public class gunRotation : MonoBehaviour {

    Transform gunTrans;
    private GameObject targetObj;
    float gunAngle;
    float xAxis;
    float yAxis;
    public float rotationSpeed = 10;

    // Use this for initialization
    void Start ()
    {
        gunTrans = GetComponent<Transform>();
        targetObj = new GameObject("Aim");
    }
	
	// Update is called once per frame
	void Update ()
    {
        yAxis = Input.GetAxis("Horizontal");
        xAxis = Input.GetAxis("Vertical");
        targetObj.transform.localPosition = new Vector2(xAxis, yAxis);

        gunTrans.LookAt(targetObj.transform);

        //gunAngle = Vector2.Angle(Vector2.right, new Vector2(xAxis, yAxis));
        //(0, 0, gunAngle * rotationSpeed);
        //(yAxis < 0)? new Vector3(0, 0, gunAngle) : new Vector3(0, 0, -gunAngle);
    }
}

using UnityEngine;
using System.Collections;

public class HoleCamera : MonoBehaviour {

    Camera cam;                 //camera of the hole
    // Transform holeTrans;        // the hole sprite's transform important for camera size. Needs be equal.
    SpriteRenderer holeRend;
    public Material holeMate;
    public Material preFabMat;
    public RenderTexture holeTex;

	// Use this for initialization
	void Start () {

        cam = GetComponent<Camera>();
        // holeTrans = GetComponentInParent<Transform>();          // gets the hole transform
        holeRend = GetComponentInParent<SpriteRenderer>();      //gets the hole sprite reneder
        holeMate = new Material(Shader.Find("Standard"));       //creates new material wiht standard shader
        holeTex = new RenderTexture(256, 256, 24);               //creates new texture


        holeRend.sharedMaterial = holeMate;                 // sets the material to the sprite reneder

        holeTex.name = "holeTexture";
        holeMate.name = "holeMaterial";
        holeMate.CopyPropertiesFromMaterial(preFabMat);         // copys values from the hole template prefab.

        cam.orthographicSize = 4f;          //holeTrans.lossyScale.x;      //sets camera size to same as hole scale. x and y must be equal



        cam.targetTexture = holeTex;                       //sets the camera to be the texture.("somehow idfk")

        holeMate.mainTexture =  holeTex;
        holeMate.SetTexture("_MainTex", holeTex);
        holeMate.SetTexture("_PARALLAXMAP", holeTex);            //sets height map.
        holeMate.SetTexture("_DETAIL_MUL2X", holeTex);           //sets emmision map.







	}

}

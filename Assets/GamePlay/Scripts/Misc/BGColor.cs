using UnityEngine;
using System.Collections;

public class BGColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var meshFilter = gameObject.GetComponent<MeshFilter>();
		meshFilter.sharedMesh.setVErtexColors(new Color(14,255,25,0),new Color(86,11,176,0),.5f);

	}
	
	// Update is called once per frame
	void Update () {

	}
}

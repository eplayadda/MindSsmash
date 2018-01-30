using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoClicked : MonoBehaviour
{
	public GameObject effect;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			CancelInvoke ("Hide");
			effect.transform.position = Input.mousePosition;
			Invoke ("Hide", .3f);
		}
	}

	void Hide ()
	{
		//InputHandler.instance.M ();
		Debug.Log ("___________________");
		effect.transform.position = new Vector3 (1020, 1200, 1200);
	}

}

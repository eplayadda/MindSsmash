using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

	private static UIController instance;

	public static UIController Instance {
		get {
			return instance;
		}
		set { 
			instance = value;
		}
	}

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else
			DestroyImmediate (this);
	}

	public void ActiveUI (GameObject obj)
	{
		obj.SetActive (true);
	}

	public void DeactiveUI (GameObject obj)
	{
		obj.SetActive (false);
	}
}

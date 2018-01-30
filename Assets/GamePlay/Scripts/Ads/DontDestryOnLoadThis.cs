using UnityEngine;
using System.Collections;

public class DontDestryOnLoadThis : MonoBehaviour {

	void Awake()
	{
		DontDestroyOnLoad (this);
	}
}

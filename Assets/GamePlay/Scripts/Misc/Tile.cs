using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	public enum eTileState
	{
		none,
		scaleUp,
		scaleDown
	}

	public eTileState currentTileState;
	public bool isJelly;
	public bool isMouseClicked;
	Vector3 scaleUpTarget = new Vector3 (.31f, .31f, 1f) ;
	Vector3 scaleDownTarget =new Vector3 (.08f, .08f, 1f);

	float speed = 10f;
	GameObject jelly;

	void Awake ()
	{
		go = transform.GetChild (0).gameObject;
		go1 = go.transform;
		jelly = go;

	}

	void Start ()
	{
		GameManager gm = GameManager.instance;
		int a = gm.currentLevel / 10;
		jelly.GetComponent<SpriteRenderer> ().sprite = gm.allJellyTexture [a % gm.allJellyTexture.Length];
		
	}

	void Update ()
	{
		switch (currentTileState) {
		case eTileState.scaleUp:
			{
				go1.localScale = Vector3.Lerp (go1.localScale, scaleUpTarget, speed * Time.deltaTime);
				if (go1.localScale.x >= scaleUpTarget.x)
					currentTileState = eTileState.none;
			}
			break;
		case eTileState.scaleDown:
			{
				go1.localScale = Vector3.Lerp (go1.localScale, scaleDownTarget, speed * Time.deltaTime);
				if (go1.localScale.x <= scaleDownTarget.x)
					currentTileState = eTileState.none;

			}
			break;

		
		}
	}

	GameObject go;
	Transform go1;

	public void Animate ()
	{
		
		go.SetActive (true);
		go1.localScale = scaleDownTarget;
//		jelly.GetComponent<Renderer>().material.mainTexture = GameManager.instance.jelly;
		currentTileState = eTileState.scaleUp;
		isJelly = true;

	}

	public void GoDown ()
	{
		GameObject go = transform.GetChild (0).gameObject;
		currentTileState = eTileState.scaleDown;
	}

	public void ShowJelly ()
	{
		float z = TileParent.instance.transform.localEulerAngles.z;
		transform.Rotate (0, 0, -z);
		jelly.transform.localScale = scaleDownTarget;
		currentTileState = eTileState.scaleUp;
		jelly.SetActive (true);
	}

	public void ShowDemon ()
	{
//		jelly.GetComponent<Renderer> ().material.mainTexture = GameManager.instance.demon;
//		jelly.transform.localScale = new Vector3 (.1f, .1f, 1f);
//		currentTileState = eTileState.scaleUp;
//		jelly.SetActive (true);
		transform.GetChild (1).gameObject.SetActive (true);
	}



}

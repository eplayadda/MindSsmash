using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
	public RectTransform hand;
	bool isHandMove;
	public RectTransform[] handTargets;
	public static Tutorial instance;
	public GameObject glow;
	List <int> jallyIndex = new List<int>{1,2};
	float speed = 10;
	Vector2 currTarget;
	int index;
	void Awake()
	{
		if (instance == null)
			instance = this;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (index > 2)
			return;
		if (isHandMove) {
			hand.position = Vector2.MoveTowards (hand.position,currTarget,speed);
			if (Vector2.Distance (hand.position, currTarget) <= .1f && index < 2) {
				isHandMove = false;
				Invoke ("AnimationTime",.6f);
				StartCoroutine ("Glow",.1f);
			}
		}
	}

	void AnimationTime()
	{
		currTarget = handTargets [index].position;
		isHandMove = true;
	}
	public void StartHandMove()
	{
		currTarget = handTargets [0].position;
		isHandMove = true;
		hand.gameObject.SetActive (true);
	}

	IEnumerator Glow()
	{
		glow.SetActive (true);
		yield return new WaitForSeconds (.5f);
		GameManager.instance.tileGo [jallyIndex[index++]].GetComponent<Tile> ().ShowJelly ();
		glow.SetActive (false);
		StopCoroutine ("Glow");
	}
}

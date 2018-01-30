//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//public class GamePlayTimerController : MonoBehaviour
//{
//	private static GamePlayTimerController instance;
//
//	public static GamePlayTimerController Instance {
//		get {
//			if (instance == null) {
//				instance = GameObject.FindObjectOfType<GamePlayTimerController> ();
//			}
//			return instance;
//		}
//		set {
//			instance = value;
//		}
//	}
//
//	// timer count in each level.
//	private float timerCount;
//	public Image timerImage;
//	float _time = 0.0f;
//	//public Text counterText;
//
//	void Awake ()
//	{
//		if (instance != null && instance != this) { 
//			Destroy (this.gameObject); 
//		} else { 
//			instance = this;
//			//DontDestroyOnLoad (this.gameObject);
//		} 
//	}
//	// Use this for initialization
//	void Start ()
//	{
//		
//	}
//
//	void OnEnable ()
//	{
//		
//	}
//
//	// set time for each level.
//	public void TimerControl ()
//	{
//		GameManager.instance.isReset = false;
//		Debug.Log ("Start Timer");
////		timerImage.fillAmount = 1;
//		if (LevelManager.instance != null) {
//			switch (LevelManager.instance.currentCross) {
//			case 2:
//				{
//					timerCount = GameConstants.COUNTER_LIMIT_L1;
//				}
//				break;
//			case 3:
//				{
//					timerCount = GameConstants.COUNTER_LIMIT_L2;
//				}
//				break;
//			case 4:
//				{
//					timerCount = GameConstants.COUNTER_LIMIT_L3;
//				}
//				break;
//			}
//			StopCoroutine ("TimerPlayer");
//			StartCoroutine ("TimerPlayer");
//		}
//	}
//
//	IEnumerator TimerPlayer ()
//	{
//
//		while (timerImage.fillAmount > 0) {
//			if (GameManager.instance.isReset)
//				yield break;
//			//Debug.Log ("In ????????????");
//			//counterText.text = timerCount.ToString ();
//			float fillAmt = Mathf.Lerp (1, 0, _time);
//			_time += (1 / timerCount) * Time.deltaTime;
//			timerImage.fillAmount = fillAmt;
//			yield return null;
//			//timerCount--;
//		}
//		if (timerImage.fillAmount <= 0) {
//			Debug.Log ("GameOver");
//			yield return new WaitForSeconds (0.1f);
//			GameManager.instance.GameOver ();
//		}
//	}
//}

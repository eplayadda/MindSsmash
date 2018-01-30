using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{
	public static InputHandler instance;
	RaycastHit mHit;
	private int inputCount;
	private int reviveConut = 0;
	//public GameObject clickObject;

	void Awake ()
	{
		if (instance == null)
			instance = this;
	}

	void Update ()
	{
		if (GameManager.instance.isTutorial)
			return;
		if (GameManager.instance.currentGameState == GameManager.eGameState.Play && Input.GetMouseButtonDown (0)) {
			if (GameManager.instance.currentGameState == GameManager.eGameState.Pause || GameManager.instance.currentGameState == GameManager.eGameState.GameOver || GameManager.instance.currentGameState == GameManager.eGameState.Exit)
				return;

			if (Physics.Raycast (GameManager.instance.gamePlayCamera.ScreenToWorldPoint (Input.mousePosition), Vector3.forward, out mHit, 10f) && inputCount < LevelManager.instance.currentJellyCount) {
				CheckMoveResult (mHit.collider.gameObject);
			}
		}
	}

	int scoreCount = 0;

	void CheckMoveResult (GameObject go)
	{
		Tile tile = go.GetComponent<Tile> ();
		bool isRight = tile.isJelly;
		//clickObject.transform.position = Input.mousePosition;
		//clickObject.SetActive (true);
		if (tile.isMouseClicked)
			return;
		tile.isMouseClicked = true;
		if (isRight) {
			ScoreHandler.instance.Score ();
			tile.ShowJelly ();
			AudioManager.Instance.PlaySound (AudioManager.SoundType.Success);
			if (GameManager.instance.jellyCount <= ++inputCount) {
				Invoke ("Reset", .2f);
			}
		} else {
			
			reviveConut++;
			int diff = Mathf.Abs (ScoreHandler.instance.GetScore () - scoreCount);
			//Debug.Log (scoreCount + " - " + ScoreHandler.instance.GetScore () + " diff " + diff);
			if (reviveConut % 3 == 0 || diff <= 10) {
				ShowGameOver ();
				reviveConut = 0;
				scoreCount = 0;
			} else {
				#if UNITY_ANDROID||UNITY_EDITOR
				scoreCount = ScoreHandler.instance.GetScore ();
				tile.ShowDemon ();
				GameManager.instance.currentGameState = GameManager.eGameState.Pause;
				RevivePopUp ();
				#else
				ShowGameOver ();
				#endif

			}
		}
	}

	void ShowGameOver ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.GameOver);
		GameManager.instance.GameOver ();
	}

	void RevivePopUp ()
	{
		GameManager.instance.myClock.PauseClock ();
		GameManager.instance.revivePanel.SetActive (true);
		UIAnimationController.Instance.ReviveEntryAnimation (GameManager.instance.revivePanel);

	}

	public void OnReviveClicked (bool isYes)
	{
		if (isYes) {
			Debug.Log ("Click Yes " + isYes);
			AdsHandler.Instance.ShowRewardedVideo ();
			GameManager.instance.revivePanel.SetActive (false);
			UIAnimationController.Instance.ResetRevivePosition (GameManager.instance.revivePanel);

		} else {
			GameManager.instance.revivePanel.SetActive (false);
			UIAnimationController.Instance.ResetRevivePosition (GameManager.instance.revivePanel);
			//GameManager.instance.demoText.text = "Button False";
			Invoke ("ShowGameOver", 0.2f);
		} 
	}

	public void GameStartAfterReward ()
	{
		GameManager.instance.currentGameState = GameManager.eGameState.Play;
		Invoke ("Reset", .1f);
	}

	//	void DisableClick ()
	//	{
	//		clickObject.SetActive (false);
	//	}

	void Reset ()
	{

		//		if (GameManager.instance.currentLevel >= 10) {
		//			AudioManager.Instance.PlaySound (AudioManager.SoundType.LevelComplete);
		//			GameManager.instance.currLevelSelected++;
		//			PlayerPrefs.SetInt ("Level", (GameManager.instance.currLevelSelected - 2));
		//			GameManager.instance.currentLevel = 0;
		//			Invoke ("ResetDelay", 5.0f);
		//		} else {
		GameManager.instance.ReSetDeta ();
		//		}
		inputCount = 0;
		GameManager.instance.currentGameState = GameManager.eGameState.LevelUp;
		GameManager.instance.myClock.ResetClock ();
	}

	void ResetDelay ()
	{
		GameManager.instance.ReSetDeta ();
	}

}

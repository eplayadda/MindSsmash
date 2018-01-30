using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationController : MonoBehaviour
{
	public static UIAnimationController Instance;

	// Exit Menu Animation Positions.
	public Transform startPoint;
	public Transform finlaPoint;


	//Pause Menu Animation Positions
	public Transform pauseUpFinalPos;
	public Transform pauseDownFinalPos;
	public Transform pauseUpStartPos;
	public Transform pauseDownStartPos;

	// GameOver Menu Animation Position.
	public Transform gameOverStartPos;
	public Transform gameOverFinalPos;

	// Tutorials Panel Animation Position.
	public Transform tutorialStartPoint;
	public Transform tutorialFinalPoint;

	//Setting Panel Animation positions.
	public Transform settingStartPoint;
	public Transform settingEndPoint;

	//Leaderboard Panel Animation Positions;
	public Transform leaderboardStartPos;
	public Transform leaderboardEndPos;

	//
	public Transform reviveFinalPos;
	public Transform reviveStartPos;

	public GameObject loadingImage;
	public GameObject loadingGameOverImage;

	private int gameOverCount = 0;
	public bool loading = false;
	public bool gameoverLoadning = false;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}

	public void OnClickYES (GameObject startPoint)
	{
		PlayerPrefs.SetInt ("start", 0);
		Debug.Log ("Quit Application");
		UIController.Instance.DeactiveUI (GameManager.instance.quitMenu);
		UIController.Instance.DeactiveUI (GameManager.instance.GameHUD);
		UIController.Instance.DeactiveUI (GameManager.instance.tileParent.gameObject);
		Application.Quit ();
	}

	public void OnClickNO (GameObject startPoint)
	{
//		GameManager.instance.currentGameState = GameManager.eGameState.Play;

		QuitMenuAnimationExit (GameManager.instance.quitMenu, startPoint.transform.position);
	}

	public void QuitMenuAnimationExit (GameObject obj, Vector3 pos)
	{
		Time.timeScale = 1;
		iTween.MoveTo (obj, iTween.Hash ("x", pos.x, "easetype", "easeInOutExpo", "time", 0.5f, "oncomplete", "OnCompleteExit", "onCompleteTarget", this.gameObject));
	}

	void OnCompleteExit ()
	{
		GameManager.instance.currentGameState = GameManager.eGameState.Play;
		Debug.Log ("OnCOmplete Exit");
		UIController.Instance.DeactiveUI (GameManager.instance.quitMenu);
		GameManager.instance.currentGameState = GameManager.instance.previousState;

	}

	public void QuitMenuAnimationEntery (GameObject obj)
	{
		iTween.MoveTo (obj, iTween.Hash ("x", finlaPoint.position.x, "easetype", "easeInOutExpo", "time", 0.5f, "oncomplete", "OnCompleteEntry", "onCompleteTarget", this.gameObject));
	}


	/// <summary>
	/// Call OnComplete QuitMenu Animation.
	/// </summary>
	void OnCompleteEntry ()
	{
		Debug.Log ("On Entry complete");
		GameManager.instance.currentGameState = GameManager.eGameState.Exit;
		Time.timeScale = 0;
	}

	public void PauseMenuAnimationEntry (GameObject objUp, GameObject objDown)
	{
		iTween.MoveTo (objUp, iTween.Hash ("y", pauseUpFinalPos.position.y, "easetype", "easeInOutExpo", "time", 0.25f, "oncomplete", "OnCompleteEntryPause", "onCompleteTarget", this.gameObject));
		iTween.MoveTo (objDown, iTween.Hash ("y", pauseDownFinalPos.position.y, "easetype", "easeInOutExpo", "time", 0.25f));

	}

	void OnCompleteEntryPause ()
	{
		Debug.Log ("On Complete Pause");
		UIController.Instance.DeactiveUI (GameManager.instance.GameHUD);
		Time.timeScale = 0;
	}

	public void PauseMenuAnimationExit (GameObject objUp, GameObject objDown)
	{
		iTween.MoveTo (objUp, iTween.Hash ("y", pauseUpStartPos.position.y, "easetype", "easeInOutExpo", "time", 0.25f, "oncomplete", "OnCompleteExitPause", "onCompleteTarget", this.gameObject));
		iTween.MoveTo (objDown, iTween.Hash ("y", pauseDownStartPos.position.y, "easetype", "easeInOutExpo", "time", 0.25f));
	}

	void OnCompleteExitPause ()
	{
		UIController.Instance.DeactiveUI (GameManager.instance.pauseMenu);
		UIController.Instance.ActiveUI (GameManager.instance.GameHUD);
	}

	public void GameOverAnimationEntry (GameObject obj)
	{
		iTween.MoveTo (obj, iTween.Hash ("x", gameOverFinalPos.position.x, "easetype", "easeInOutExpo", "time", 0.2f, "oncomplete", "OnCompleteGameOverEntry", "onCompleteTarget", this.gameObject));

	}

	public void LeaderboardAnimationEntry (GameObject obj)
	{
		//iTween.MoveTo (obj, iTween.Hash ("x", gameOverFinalPos.position.x, "easetype", "easeInOutExpo", "time", 0.5f, "oncomplete", "OnCompleteLeaderboardAnim", "onCompleteTarget", this.gameObject));
		iTween.MoveTo (obj, iTween.Hash ("y", leaderboardEndPos.position.y, "time", 0.3f, "easetype", "easeInOutExpo", "oncomplete", "OnCompleteLeaderboardAnim", "onCompleteTarget", this.gameObject));

	}

	void OnCompleteGameOverEntry ()
	{
		SocialManager.Instance.facebookManager.UpdateTopScorrer ();
		if (PlayerPrefs.GetInt ("GameOver") % 5 == 0) {
			PlayerPrefs.SetInt ("GameOver", 1);
			AdsHandler.Instance.ShowVideoAds ();
		} else if (PlayerPrefs.GetInt ("GameOver") % 3 == 0) {
			int count = PlayerPrefs.GetInt ("GameOver");
			count += 1;
			PlayerPrefs.SetInt ("GameOver", count);
			AdsHandler.Instance.ShowInterstitialAds ();
		} else if (PlayerPrefs.GetInt ("GameOver") % 4 == 0) {
			int count = PlayerPrefs.GetInt ("GameOver");
			count += 1;
			PlayerPrefs.SetInt ("GameOver", count);
			//			SocialManager.Instance.RateUs ();
			if (PlayerPrefs.GetInt ("RateUs") == 0)
				GameManager.instance.ShowRateUsPanel (true);

		} else {
			int count = PlayerPrefs.GetInt ("GameOver");
			count += 1;
			PlayerPrefs.SetInt ("GameOver", count);
		}
//		Time.timeScale = 0;
	}

	void OnCompleteLeaderboardAnim ()
	{
		Debug.Log ("OnCompleteLeaderboardAnim");
		SocialManager.Instance.facebookManager.GetScoreFB ();
	}

	public void TutorialPanelAnimationEntry (GameObject obj)
	{
		iTween.MoveTo (obj, iTween.Hash ("x", tutorialFinalPoint.position.x, "easetype", "easeInOutExpo", "time", 0.3f));

	}

	public void TutorialPanleReset (GameObject obj)
	{
		obj.transform.position = tutorialStartPoint.position;
	}

	public void SettingPanelAnimationEntry (GameObject obj)
	{
		iTween.MoveTo (obj, iTween.Hash ("x", settingEndPoint.position.x, "easetype", "easeInOutExpo", "time", 0.3f));
	}


	public void SettingPanelReset (GameObject obj)
	{
		obj.transform.position = settingStartPoint.position;
		Invoke ("ActiveSettingPanel", .1f);
	}

	void ActiveSettingPanel ()
	{
		UIController.Instance.ActiveUI (GameManager.instance.settingPanel);
		
	}

	public void ResetLeaderBoardPanel (GameObject obj)
	{
		obj.transform.position = leaderboardStartPos.position;
	}


	public void CreditPanelAnimation (GameObject obj)
	{
		iTween.ScaleTo (obj, iTween.Hash ("x", 1.0f, "y", 1.0f, "z", 1.0f, "time", 0.3f, "easeType", "easeInOutExpo"));
	}

	public void CreditPanelReset (GameObject obj)
	{
		iTween.ScaleTo (obj, iTween.Hash ("x", 0f, "y", 0f, "z", 0f, "time", 0.3f, "easeType", "easeInOutExpo", "oncomplete", "OnCompleteCreditPage", "onCompleteTarget", this.gameObject));
	}

	private void OnCompleteCreditPage ()
	{
		UIController.Instance.DeactiveUI (GameManager.instance.creditPanel);
	}

	void Update ()
	{
		if (loading) {
			loadingImage.transform.Rotate (Vector3.back * Time.deltaTime * 50);
		}
		if (gameoverLoadning) {
			loadingGameOverImage.transform.Rotate (Vector3.back * Time.deltaTime * 50);
		}
	}

	public void ReviveEntryAnimation (GameObject obj)
	{
		iTween.MoveTo (obj, iTween.Hash ("x", reviveFinalPos.position.x, "easetype", "easeInOutExpo", "time", 0.3f));
	}

	public void ResetRevivePosition (GameObject obj)
	{
		obj.transform.position = reviveStartPos.position;
	}

	public void ConnectionPanelAnimation (GameObject obj)
	{
		iTween.ScaleTo (obj, iTween.Hash ("x", 1.0f, "y", 1.0f, "z", 1.0f, "time", 1.0f, "easeType", "easeOutBounce"));
	}

	public void ConnectionPanelReset (GameObject obj)
	{
		obj.transform.localScale = Vector3.zero;
	}

}

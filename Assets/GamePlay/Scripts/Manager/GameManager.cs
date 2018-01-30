using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public LevelManager levelManager;

	public Text demoText;
	//	public TrackGameobject analyticsObject;
	public Texture2D demon;
	public Texture2D jelly;
	public SpriteRenderer jellyTexture;
	public GameObject tilePrefab;
	public GameObject bg;
	public Camera gamePlayCamera;
	public Transform tileParent;
	public Vector3 tileParentScale;
	public List<GameObject> tileGo = new List<GameObject> ();
	public List<int> sellectedJelly = new List<int> ();
	public List <int> totalJally = new List<int> ();
	public int jellyCount;
	public int currentLevel;
	public GameObject icon;
	public Clock myClock;
	int lastCross;
	public bool isTutorial;
	public int currLevelSelected;

	public enum eGameState
	{
		NONE,
		Play,
		Pause,
		LevelUp,
		GameOver,
		Exit
	}

	public GameObject playBtn;
	public GameObject GameHUD;
	public GameObject gameOver;
	public GameObject pauseMenu;
	public GameObject mainmenu;
	public GameObject splashLoading;
	public GameObject levelSelection;
	public GameObject rateUs;
	public Text bestScore;
	public GameObject CounterPanel;
	public GameObject quitMenu;
	public GameObject pauseUp;
	public GameObject pauseDown;
	public GameObject tutorialsPanel;
	public GameObject leaderBoard;
	public GameObject settingPanel;
	public GameObject creditPanel;
	public GameObject revivePanel;
	public GameObject connectionPanel;


	public eGameState currentGameState;
	public eGameState previousState;

	public bool isFailedToLoad;

	public Transform marker;
	private float pHeight;
	private float pWidth;
	public List<Button> allLevels;
	public Sprite[] allJellyTexture;

	void Awake ()
	{
		Time.timeScale = 1;
		if (instance == null)
			instance = this;
		//		PlayerPrefs.DeleteAll ();
	}

	void Start ()
	{
		//		PlayerPrefs.DeleteAll ();
		if (PlayerPrefs.GetInt ("start") == 0) {
			SplashLoading ();
			PlayerPrefs.SetInt ("start", 1);
		} else {
			GamePlayLoad ();
		}
	}

	void SplashLoading ()
	{
		splashLoading.SetActive (true);
		Invoke ("GamePlayLoad", 1f);
	}

	void GamePlayLoad ()
	{
		splashLoading.SetActive (false);
		mainmenu.SetActive (true);
		levelManager = LevelManager.instance;
		bestScore.text = "BEST SCORE :" + " " + PlayerPrefs.GetInt ("BestScore").ToString ();
		UIController.Instance.DeactiveUI (pauseMenu);
		AdsHandler.Instance.ShowBannerAdsMenuPage ();
		AudioManager.Instance.StartGamePlayAudio ();
	}

	void Update ()
	{
		// Back button popup android device.
		if (Input.GetKey (KeyCode.Escape) && currentGameState != eGameState.GameOver) {
			UIController.Instance.ActiveUI (quitMenu);
			previousState = currentGameState;
			UIAnimationController.Instance.QuitMenuAnimationEntery (quitMenu);
		}
		if (Input.GetKey (KeyCode.Escape) && currentGameState == eGameState.GameOver) {
			Debug.Log ("Exit Game from Gameover screen");
			Application.Quit ();
		}
	}


	/// <summary>
	/// Start GamePlay.
	/// </summary>
	public void OnPlayClicked ()
	{
		Analytics.CustomEvent ("StartGame", new Dictionary<string,object> { {"GameStart",
				"GameStarted@" + string.Format ("{0:g}", System.DateTime.Now)
			}
		});
		OnLockLevel ();
		UIController.Instance.ActiveUI (levelSelection);
		UIController.Instance.DeactiveUI (mainmenu);
		AudioManager.Instance.PlaySound (AudioManager.SoundType.GameStart);
	}

	public void OnLevelSelected (int curLevel)
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);
		currLevelSelected = curLevel;
		OnStartClicked ();
	}

	public void OnStartClicked ()
	{
		isTutorial = false;
		icon.SetActive (false);
		playBtn.SetActive (false);
		UIController.Instance.ActiveUI (GameHUD);
		UIController.Instance.DeactiveUI (levelSelection);
		//AdsHandler.Instance.HideBannerAdsMenuPage ();
		AdsHandler.Instance.HideBannerAdsPausePage ();
		GetLevelData ();

	}

	public void OnPauseBtnClicked ()
	{
		AdsHandler.Instance.HideBannerAdsMenuPage ();

		if (PlayerPrefs.GetInt ("Pause") % 3 == 0) {
			PlayerPrefs.SetInt ("Pause", 0);
			AdsHandler.Instance.ShowInterstitialAds ();
		} else {
			int count = PlayerPrefs.GetInt ("Pause");
			count += 1;
			PlayerPrefs.SetInt ("Pause", count);
			AdsHandler.Instance.ShowBannerAdsPausePage ();

		}
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);
		currentGameState = eGameState.Pause;
		UIController.Instance.ActiveUI (pauseMenu);
		UIAnimationController.Instance.PauseMenuAnimationEntry (pauseUp, pauseDown);
	}


	/// <summary>
	/// Resume button click on PauseMenu.
	/// </summary>
	public void OnResumeBtnClicked ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);
		currentGameState = eGameState.Play;
		Time.timeScale = 1;
		AdsHandler.Instance.ShowBannerAdsMenuPage ();
		//UIController.Instance.DeactiveUI (pauseMenu);
		AdsHandler.Instance.HideBannerAdsPausePage ();
		UIAnimationController.Instance.PauseMenuAnimationExit (pauseUp, pauseDown);
	}

	/// <summary>
	/// Exit button click In PauseMenu.
	/// </summary>
	public void OnExitButtonClick ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);

		Time.timeScale = 1;
		UIAnimationController.Instance.PauseMenuAnimationExit (pauseUp, pauseDown);
		UIController.Instance.ActiveUI (quitMenu);
		previousState = currentGameState;
		UIAnimationController.Instance.QuitMenuAnimationEntery (quitMenu);

	}

	public void DrawTile (int pDimention)
	{
		gamePlayCamera.orthographicSize = Screen.height / 200f;
		pHeight = Screen.height / 100f;
		pWidth = Screen.width / 100f;
		bg.transform.localScale = new Vector3 (pWidth, pHeight, .1f);
		float tileSize = pWidth / (pDimention + 2f);
		float gap = pWidth / 100f;
		for (int i = 0; i < pDimention; i++) {
			for (int j = 0; j < pDimention; j++) {
				GameObject temp = Instantiate (tilePrefab, new Vector3 (i * (tileSize + gap), j * (tileSize + gap), 5), Quaternion.identity) as GameObject;
				temp.transform.localScale = new Vector3 (tileSize, tileSize, .1f);
				tileGo.Add (temp);
				temp.name = "Tile" + i + "" + j;
			}
		}
		Vector3 pos = (tileGo [0].transform.position + tileGo [tileGo.Count - 1].transform.position) / 2f;
		pos = new Vector3 (pos.x, pos.y, 0);
		bg.transform.position = new Vector3 (pos.x, pos.y, 10);
		gamePlayCamera.transform.position = pos;
		tileParent.position = new Vector3 (pos.x, pos.y, 9);
		float w = Vector3.Distance (tileGo [0].transform.position, tileGo [pDimention - 1].transform.position);
		w = w + tileSize;
		w = w * Mathf.Sqrt (2);
		tileParent.localScale = new Vector3 (w, w, .1f);
		tileParentScale = tileParent.localScale;
		marker.position = tileGo [pDimention - 1].transform.position + new Vector3 (-1, 1, 1) * tileSize;
		marker.localScale = Vector3.one * tileSize / 10f;
		for (int i = 0; i < tileGo.Count; i++)
			tileGo [i].transform.parent = tileParent;
		//		GetLevelData();

	}

	public bool isReset = false;

	public void ReSetDeta ()
	{
		isReset = true;
		//		GamePlayTimerController.Instance.Invoke ("TimerControl", 0.1f);
		TileParent.instance.SetRotationState (false);
		for (int i = 0; i < tileGo.Count; i++) {
			tileGo [i].GetComponent<Tile> ().isJelly = false;
			tileGo [i].GetComponent<Tile> ().isMouseClicked = false;
			tileGo [i].transform.GetChild (0).gameObject.SetActive (false);
			tileGo [i].transform.GetChild (1).gameObject.SetActive (false);
		}
		sellectedJelly.Clear ();
		totalJally.Clear ();


	}

	public void GetLevelData ()
	{
		//		int lvl = currentLevel++ + 10 * (GameManager.instance.currLevelSelected - 2);
		levelManager.SetCurrentLevelData (currentLevel++);
		Analytics.CustomEvent ("LevelData", new Dictionary<string,object> { 
			{ "CurrentLevel",currentLevel }
		});
		if (lastCross != levelManager.currentCross) {
			//			TileParentGoDown();
			tileParent.GetComponent<TileParent> ().SetPingPongState (true);
		} else {
			TileParentGoDown ();
			TileParentGoUp ();
		}

	}

	public void TileParentGoDown ()
	{
		for (int i = 0; i < tileGo.Count; i++)
			Destroy (tileGo [i]);
		tileGo.Clear ();
		DrawTile (levelManager.currentCross);
	}

	public void TileParentGoUp ()
	{
		lastCross = levelManager.currentCross;
		jellyCount = levelManager.currentJellyCount;
		SellectJelly (levelManager.currentJellyCount);
	}

	public void CreateJelly ()
	{
		GetLevelData ();
	}

	public void SellectJelly (int count)
	{
		for (int i = 0; i < tileGo.Count; i++)
			totalJally.Add (i);
		for (int i = 0; i < count; i++) {

			int p = 0;
			if (currentLevel == 1)
				p = 1;
			else
				p = Random.Range (0, totalJally.Count);
			sellectedJelly.Add (totalJally [p]);
			totalJally.Remove (totalJally [p]);
		}
		StartCoroutine (ShowJelly ());
	}

	IEnumerator ShowJelly ()
	{
		yield return new WaitForSeconds (.2f);  // 3.75

		for (int i = 0; i < sellectedJelly.Count; i++) {
			GameObject go = tileGo [sellectedJelly [i]];
			go.GetComponent<Tile> ().Animate ();	
			yield return new WaitForSeconds (.35f);  // 3.75

		}

		for (int i = 0; i < sellectedJelly.Count; i++) {
			GameObject go = tileGo [sellectedJelly [i]];
			go.GetComponent<Tile> ().GoDown ();	
			yield return new WaitForSeconds (.1f);  // 3.75
			GameObject GoChild = go.transform.GetChild (0).gameObject;
			GoChild.SetActive (false);
			//			GoChild.GetComponent<iTween> ().enabled = false;

		}
		//		iTween.RotateBy(tileParent.gameObject, iTween.Hash("z", -.25, "easeType", "linear","loopType", "None", "delay", .1,"time",.3));
		TileParent.instance.SetRotationState (true);

	}

	//int gameOverCount = 0;

	public void GameOver ()
	{
		//analyticsObject.googleAnalytics.LogEvent ("GameOver", "GameOver", "GameOver", ScoreHandler.instance.GetCurrentScore ());
		int currentScore = ScoreHandler.instance.GetCurrentScore ();
		Debug.Log ("GameOver");
		Analytics.CustomEvent ("GameOver", new Dictionary<string,object> { 
			{ "score",currentScore }
		});
		Debug.Log (PlayerPrefs.GetInt ("GameOver"));

		if (PlayerPrefs.GetInt ("GameOver") % 5 == 0) {
			PlayerPrefs.SetInt ("GameOver", 1);
			AdsHandler.Instance.ShowVideoAds ();
		} else if (PlayerPrefs.GetInt ("GameOver") % 3 == 0) {
			int count = PlayerPrefs.GetInt ("GameOver");
			count += 1;
			PlayerPrefs.SetInt ("GameOver", count);
			AdsHandler.Instance.ShowInterstitialAds ();
		} else if (PlayerPrefs.GetInt ("GameOver") % 6 == 0) {
			int count = PlayerPrefs.GetInt ("GameOver");
			count += 1;
			PlayerPrefs.SetInt ("GameOver", count);
			//			SocialManager.Instance.RateUs ();
			if (PlayerPrefs.GetInt ("RateUs") == 0)
				ShowRateUsPanel (true);

		} else {
			int count = PlayerPrefs.GetInt ("GameOver");
			count += 1;
			PlayerPrefs.SetInt ("GameOver", count);
		}

		currentGameState = eGameState.GameOver;
		ScoreHandler.instance.SetBestScore ();
		UIController.Instance.ActiveUI (gameOver);
		UIAnimationController.Instance.GameOverAnimationEntry (gameOver);
		AudioManager.Instance.PlaySound (AudioManager.SoundType.GameOver);
		//UIController.Instance.DeactiveUI (GameHUD);
	}

	public void ShowRateUsPanel (bool isShow)
	{
		rateUs.SetActive (isShow);
	}

	public void OnReplay ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);

		//Application.LoadLevel (0);
		AdsHandler.Instance.OnRestartGame ();
		SceneManager.LoadScene (0);
	}

	public void OnTutorialsButtonClick ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);

		currLevelSelected = 2;
		isTutorial = true;
		UIController.Instance.ActiveUI (tutorialsPanel);
		UIAnimationController.Instance.TutorialPanelAnimationEntry (tutorialsPanel);


		icon.SetActive (false);
		playBtn.SetActive (false);
		AdsHandler.Instance.HideBannerAdsPausePage ();
		Invoke ("TutorialInvok", .1f);
		Tutorial.instance.Invoke ("StartHandMove", 2f);
	}

	void TutorialInvok ()
	{
		GetLevelData ();
	}

	public void CloseTutorial ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);
		SceneManager.LoadScene (0);
		//		UIAnimationController.Instance.TutorialPanleReset (tutorialsPanel);
		//		UIController.Instance.DeactiveUI (tutorialsPanel);

	}

	public void OnClickLeaderboard ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);

		UIController.Instance.ActiveUI (leaderBoard);
		UIAnimationController.Instance.LeaderboardAnimationEntry (leaderBoard);
	}

	public void OnCloseLeaderboard ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);

		UIController.Instance.DeactiveUI (leaderBoard);
		UIAnimationController.Instance.ResetLeaderBoardPanel (leaderBoard);
	}


	void OnApplicationQuit ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);
		PlayerPrefs.SetInt ("start", 0);
		Debug.Log ("OnApplication Quit");
		//	AdsDisplayHandler.Instance.CleanInterstitial ();
	}

	public void OnLockLevel ()
	{ 
		int curLvl = PlayerPrefs.GetInt ("Level");
		for (int i = 0; i < allLevels.Count; i++) {
			if (i <= curLvl) {
				allLevels [i].interactable = true;
				allLevels [i].transform.GetChild (0).gameObject.SetActive (true);
				allLevels [i].transform.GetChild (1).gameObject.SetActive (false);

			} else {
				allLevels [i].interactable = false;
				allLevels [i].transform.GetChild (0).gameObject.SetActive (false);
				allLevels [i].transform.GetChild (1).gameObject.SetActive (true);
			}
		}
	}

	public void OnSettingBtnClicked ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);

		UIController.Instance.ActiveUI (settingPanel);
		UIAnimationController.Instance.SettingPanelAnimationEntry (settingPanel);
	}

	public void OnCloseSetting ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);

		UIAnimationController.Instance.SettingPanelReset (settingPanel);
		UIController.Instance.DeactiveUI (settingPanel);
	}

	public void CraditClicked ()
	{
		//mainmenu.SetActive (false);
		UIController.Instance.ActiveUI (creditPanel);
		UIAnimationController.Instance.CreditPanelAnimation (creditPanel);
	}

	public void CraditClose ()
	{
		//mainmenu.SetActive (true);
		UIAnimationController.Instance.CreditPanelReset (creditPanel);
	}

	public void OnClickConnectionOK ()
	{
		UIController.Instance.DeactiveUI (GameManager.instance.connectionPanel);
		UIAnimationController.Instance.ConnectionPanelReset (GameManager.instance.connectionPanel);
		Invoke ("GameOver", 0.1f);
	}

}


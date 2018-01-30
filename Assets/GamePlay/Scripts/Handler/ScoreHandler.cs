using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Facebook.Unity;
using Assets.SimpleAndroidNotifications;
using System;

public class ScoreHandler : MonoBehaviour
{
	public static ScoreHandler instance;
	public Text scoreTxt;
	public Text bestScoreTxt;
	public Text gameOverScoreTxt;
	public Text frindName;
	public Text onlineMsg;
	public Text friendScore;
	public GameObject offLineTarget;
	public GameObject onlineTarget;
	public GameObject onTop;
	//public Text gameplayScore;

	int score;

	void Awake ()
	{
		if (instance == null)
			instance = this;
	}

	public void Score ()
	{
		score++;
		scoreTxt.text = score.ToString ();
	}

	public int GetScore ()
	{
		return score;
	}

	public int GetCurrentScore ()
	{
		return PlayerPrefs.GetInt ("BestScore");
	}

	public void CreateNotification (int type)
	{
		String str = "";
		if (type == 1) {
			str = PlayerPrefs.GetString ("FriendName");
			int score = PlayerPrefs.GetInt ("FriendScore");
			str = str + " sent challenge for you,his score is" + score;
		}
		if (type == 2) {
			str = "Click on leaderboard compare your score with  Facebook Friends";
		}
		if (type == 3) {
			str = "Congratulations!! You are the top of the table";
		}

		NotificationManager.CancelAll ();
		for (int i = 1; i < 30; i++) {
			NotificationManager.SendWithAppIcon (TimeSpan.FromSeconds (83000 * i), "MindSsmash", str, new Color (0, 0.6f, 1), NotificationIcon.Event);
		}
	}

	public void SetBestScore ()
	{
		if (PlayerPrefs.GetInt ("BestScore") < score) {
			PlayerPrefs.SetInt ("BestScore", score);
			if (SocialManager.Instance.facebookManager.IsFbLogedin ()) {
				SocialManager.Instance.facebookManager.SetScoreToFB (score.ToString ());
			}
		}
		gameOverScoreTxt.text = score.ToString ();
		bestScoreTxt.text = PlayerPrefs.GetInt ("BestScore").ToString ();
	}

	public void ScoreAI ()
	{
		if (PlayerPrefs.GetInt ("FriendScore") > PlayerPrefs.GetInt ("BestScore")) {
			onlineTarget.SetActive (true);
			offLineTarget.SetActive (false);
			onTop.SetActive (false);
			onlineMsg.text = "Your friend " + PlayerPrefs.GetString ("FriendName") + " have more score than you.";
			frindName.text = PlayerPrefs.GetString ("FriendName");
			friendScore.text = PlayerPrefs.GetInt ("FriendScore").ToString ();
			CreateNotification (1);
		} 
		if (PlayerPrefs.GetInt ("FriendScore") <= PlayerPrefs.GetInt ("BestScore")) {
			offLineTarget.SetActive (false);
			onlineTarget.SetActive (false);
			onTop.SetActive (true);
			CreateNotification (3);

		} 
		Debug.Log ("??? " + PlayerPrefs.GetInt ("FriendScore"));
		if (PlayerPrefs.GetInt ("FriendScore") < 0) {
			offLineTarget.SetActive (true);
			onlineTarget.SetActive (false);
			onTop.SetActive (false);
			CreateNotification (2);


		}

	}

}

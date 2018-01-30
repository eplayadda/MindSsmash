//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System;
//
//public class LeaderboardHandler : MonoBehaviour
//{
//	public static LeaderboardHandler Instance;
//
//	public const string APP_KEY = "f1fef478557edad508d64ffe372c19dd3ac582cfc691d9b80f4ff588366f27d5";
//	public const string SECRET_KEY = "80104c0f663cf35ae0f0105187bf55c84c4b8434c597a69e9fba33eaad329ffe";
//	public string GAME_NAME = "TEST_GAME";
//	//string decs = "A test game";
//	public string userName = "";
//	//	string userName2 = "User2";
//	//	string userName3 = "User3";
//	//	string userName4 = "User4";
//	public string scoreId;
//	public string gameName;
//	//public int scorelistCount;
//	public GameObject playerInfoPrefab;
//	public Transform parentProfile;
//	private List<GameObject> leaderBoardCount = new List<GameObject> ();
//
//	//GameService gameService;
//	//ScoreBoardService scoreBoardService;
//
//	void Awake ()
//	{
//		if (Instance == null)
//			Instance = this;
//		//App42API.Initialize (APP_KEY, SECRET_KEY);
//		///gameService = App42API.BuildGameService ();
//		//scoreBoardService = App42API.BuildScoreBoardService ();
//		//gameService.GetGameByName ("MindFlow", new UnityCallBack ());
//		gameName = "MindFlow";
//	}
//	// Use this for initialization
//	void Start ()
//	{
////		App42API.Initialize (APP_KEY, SECRET_KEY);
////		gameService = App42API.BuildGameService ();
////		scoreBoardService = App42API.BuildScoreBoardService ();
////		//gameService.GetGameByName ("MindFlow", new UnityCallBack ());
////		gameName = "MindFlow";
//	}
//
//	public void SaveUserScore (string Id, string userName, string score)
//	{
////		App42API.Initialize (APP_KEY, SECRET_KEY);
////		gameService = App42API.BuildGameService ();
////		scoreBoardService = App42API.BuildScoreBoardService ();
////		//gameService.GetGameByName ("MindFlow", new UnityCallBack ());
////		gameName = "MindFlow";
////		Debug.Log ("SaveUserScore " + userName + score);
////		this.userName = userName;
////		scoreBoardService.SaveUserScore (gameName, userName, double.Parse (score), new SaveScoreCallBack ());
////		scoreBoardService.SaveUserScore (gameName, userName2, 2000.0, new SaveScoreCallBack ());
////		scoreBoardService.SaveUserScore (gameName, userName3, 3000.0, new SaveScoreCallBack ());
////		scoreBoardService.SaveUserScore (gameName, userName4, 4000.0, new SaveScoreCallBack ());
////		scoreBoardService.SaveUserScore (gameName, "u4", 100.0, new SaveScoreCallBack ());
//		//Invoke ("UpdateUserScore", 5.0f);
//	}
//
//	public void UpdateUserScore (string score)
//	{
////		scoreBoardService.EditScoreValueById (scoreId, double.Parse (score), new SaveScoreCallBack ());
//	}
//
//	public void LeaderBoardDisplay ()
//	{
//		Debug.Log ("LeaderBoardDisplay");
////		scoreBoardService.GetTopNRankers (gameName, 50, new LeaderBoardDisplayCallBack ());
//	}
//
//	UserInfo info;
//	public string str;
//
//	public void CreateLeaderboardCell (int cellCount, Game game)
//	{
//		int avlCell = leaderBoardCount.Count;
//		Debug.Log (cellCount + " >> ");
//		for (int i = 0; i < (cellCount - avlCell); i++) {
//			GameObject profile = Instantiate (playerInfoPrefab) as GameObject;
//			profile.SetActive (true);
//			profile.transform.localScale = Vector3.one;
//			profile.transform.SetParent (parentProfile);
//			leaderBoardCount.Add (profile);
//			info = profile.GetComponent<UserInfo> ();
//			str = game.GetScoreList () [i].GetUserName () + "," + game.GetScoreList () [i].GetValue ().ToString ();
//			//info.UpdateUserInfo (game.GetScoreList () [i].GetUserName (), game.GetScoreList () [i].GetValue ().ToString ());
//		}
//	}
//
//	//	GUIStyle style = new GUIStyle ();
//	//
//	//	void OnGUI ()
//	//	{
//	//		style.fontSize = 50;
//	//		GUI.Label (new Rect (250, 450, 500, 200), str, style);
//	//
//	//	}
//}
//
//public class UnityCallBack : App42CallBack
//{
//	public void OnSuccess (object response)
//	{  
//		Game game = (Game)response;       
//		Debug.Log ("gameName is " + game.GetName ());   
//		Debug.Log ("gameDescription is " + game.GetDescription ());   
//		LeaderboardHandler.Instance.gameName = game.GetName ();
//	}
//
//	public void OnException (Exception e)
//	{  
//		App42Log.Console ("Exception : " + e);  
//	}
//}
//
//public class SaveScoreCallBack : App42CallBack
//{
//	public void OnSuccess (object response)
//	{  
//		Game game = (Game)response;       
//		App42Log.Console ("gameName is " + game.GetName ());   
//		for (int i = 0; i < game.GetScoreList ().Count; i++) {  
//			Debug.Log ("userName is : " + game.GetScoreList () [i].GetUserName ());  
//			Debug.Log ("score is : " + game.GetScoreList () [i].GetValue ());  
//			Debug.Log ("scoreId is : " + game.GetScoreList () [i].GetScoreId ());  
//			if (game.GetScoreList () [i].GetUserName ().Equals (LeaderboardHandler.Instance.userName))
//				LeaderboardHandler.Instance.scoreId = game.GetScoreList () [i].GetScoreId ();
//		} 
//		LeaderboardHandler.Instance.LeaderBoardDisplay ();
//	}
//
//	public void OnException (Exception e)
//	{  
//		App42Log.Console ("Exception : " + e);  
//	}
//}
//
//public class LeaderBoardDisplayCallBack : App42CallBack
//{
//	public void OnSuccess (object response)
//	{  
//		Game game = (Game)response;    
//		LeaderboardHandler.Instance.str += "LeaderBoardDisplayCallBack";
//		//LeaderboardHandler.Instance.scorelistCount = game.GetScoreList ().Count;
//		LeaderboardHandler.Instance.CreateLeaderboardCell (game.GetScoreList ().Count, game);
//		App42Log.Console ("gameName is " + game.GetName ());   
//		for (int i = 0; i < game.GetScoreList ().Count; i++) {  
//			Debug.Log ("userName is : " + game.GetScoreList () [i].GetUserName ());  
//			Debug.Log ("score is : " + game.GetScoreList () [i].GetValue ());  
//			//App42Log.Console ("scoreId is : " + game.GetScoreList () [i].GetScoreId ());  
//		}  
//	}
//
//	public void OnException (Exception e)
//	{  
//		App42Log.Console ("Exception : " + e);  
//	}
//}
//

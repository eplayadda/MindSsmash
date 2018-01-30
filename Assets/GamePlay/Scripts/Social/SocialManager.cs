using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System;

public class SocialManager : MonoBehaviour
{
	private static SocialManager mInstance;

	public static SocialManager Instance {
		get {
			if (mInstance == null)
				mInstance = FindObjectOfType<SocialManager> ();
			return mInstance;
		}
		set {
			mInstance = value;
		}
	}

	public GameObject facebookPanel;
	public FacebookHandler facebookManager;
	public bool hasInternetConnection;
	//	public RawImage profilePic;
	//	public Text userName;
	// Use this for initialization
	void Start ()
	{
		//Social.localUser.Authenticate (AuthenticakeCallback);
		//StartCoroutine (CheckInternetConnection (CheckInternetConnectionCallback));
	}

	private IEnumerator CheckInternetConnection (Action<bool> action)
	{
		while (true) {
			WWW www = new WWW ("https://www.google.com");
			yield return www;
			if (string.IsNullOrEmpty (www.error)) {
				action (true);
			} else {
				action (false);
			}
			yield return new WaitForSeconds (4);
		}
	}

	private void CheckInternetConnectionCallback (bool hasConnection)
	{
		if (hasConnection) {
			Debug.Log ("Connection Available");
			hasInternetConnection = true;
		} else {
			Debug.Log ("No Internet Connection");
			hasInternetConnection = false;
		}
		
	}

	public void OnClickFacebookIcon ()
	{
		facebookManager.OnFacebookLogin ();
	}

	public void OnClickFacebookShare ()
	{
		facebookManager.OnFacebookShare ();
	}

	public void RateUs ()
	{
		PlayerPrefs.SetInt ("RateUs", 1);
		Debug.Log ("RetUs");
		GameManager.instance.ShowRateUsPanel (false);
		Application.OpenURL ("market://details?id=com.eplayadda.mindssmash");
	}
	//	public void DisplayLeaderboardData (string userId, string UserName, string imageUrl)
	//	{
	//		Debug.Log (userId + " >> " + imageUrl);
	////		this.userName.text = UserName;
	//		//this.profilePic.texture = picTexture;
	//		LoadImgFromURL (imageUrl, delegate(Texture pictureTexture) {
	//			// Setup the User's profile picture
	//			if (pictureTexture != null) {
	////				this.profilePic.texture = pictureTexture;
	//			}
	//
	//		});
	//	}

	//	public void LoadImgFromURL (string imgURL, Action<Texture> callback)
	//	{
	//		// Need to use a Coroutine for the WWW call, using Coroutiner convenience class
	//		StartCoroutine (LoadImgEnumerator (imgURL, callback));
	//	}
	//
	//	public IEnumerator LoadImgEnumerator (string imgURL, Action<Texture> callback)
	//	{
	//		WWW www = new WWW (imgURL);
	//		yield return www;
	//
	//		if (www.error != null) {
	//			Debug.LogError (www.error);
	//			yield break;
	//		}
	//		callback (www.texture);
	//	}
}

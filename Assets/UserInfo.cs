using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
	public Text usernameText;
	public Text scoreText;

	public void UpdateUserInfo (string userName, string score)
	{
		usernameText.text = userName;
		scoreText.text = score;

	}

}

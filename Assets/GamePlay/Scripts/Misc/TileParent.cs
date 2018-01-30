using UnityEngine;
using System.Collections;

public class TileParent : MonoBehaviour {
	public static TileParent instance;
	public enum eRotationState
	{
		none,
		before,
		after,
		scalDown,
		scaleUp
	};

	public eRotationState currentRotationState;
	LevelManager levelManager;
	Vector3 to ;
	void Awake () {
		if(instance == null)
			instance = this;
	}

	void Start()
	{
		levelManager =LevelManager.instance;
	}
	void Update () 
	{
		switch(currentRotationState)
		{
			case eRotationState.before:
			{
				if (Mathf.Abs(transform.eulerAngles.z -to.z) <=  0.01f)
				{
					Debug.Log("BeforeDone");
					GameManager.instance.myClock.PlayClock ();
					currentRotationState = eRotationState.none;
					GameManager.instance.currentGameState = GameManager.eGameState.Play;
				}
			}
			break;
			case eRotationState.after:
			{
				if (Mathf.Abs(transform.eulerAngles.z ) <=  0.01f )
				{
					Debug.Log("AfterDone");
					currentRotationState = eRotationState.none;
					GameManager.instance.CreateJelly();
				}
			}
			break;
		case eRotationState.scaleUp:
			{
				transform.localScale = Vector3.Lerp (transform.localScale,GameManager.instance.tileParentScale,20*Time.deltaTime);

				if(transform.localScale.x+0.01f >= GameManager.instance.tileParentScale.x )
				{
					currentRotationState = eRotationState.none;
					GameManager.instance.TileParentGoUp();
					currentRotationState = eRotationState.none;
				}
			}
			break;
		case eRotationState.scalDown:
			{
				transform.localScale = Vector3.Lerp (transform.localScale,new Vector3(.1f,.1f,.1f),20*Time.deltaTime);
				if(transform.localScale.x <=.11f )
				{
					GameManager.instance.TileParentGoDown();
					currentRotationState = eRotationState.scaleUp;
					transform.localScale = new Vector3(.1f,.1f,.1f);

				}
			}
			break;
		}
	}

	public void SetRotationState(bool isBefore)
	{
		if(isBefore)
		{
			currentRotationState = eRotationState.before;
			iTween.RotateBy(gameObject, iTween.Hash("z", -levelManager.currentAngle*.01f, "easeType", "linear","loopType", "None", "delay", .1,"time",levelManager.currentRotateSpeed *.1f));
			SetTileParentRotateAngle();
		}
		else 
		{
			currentRotationState = eRotationState.after;
			iTween.RotateBy(gameObject, iTween.Hash("z", levelManager.currentAngle*.01f, "easeType", "linear","loopType", "None", "delay", .1,"time",levelManager.currentRotateSpeed *.1f));

		}
	}

	void SetTileParentRotateAngle()
	{

		switch (LevelManager.instance.currentAngle )
		{
			case 25:
			{
				to = new Vector3(0, 0, 270);
			}
			break;
			case 50:
			{
				to = new Vector3(0, 0, 180);
			}
			break;
			case 75:
			{
				to = new Vector3(0, 0, 90);
			}
			break;
			case -25:
			{
				to = new Vector3(0, 0, 90);
			}
			break;
			case -50:
			{
				to = new Vector3(0, 0, 180);
			}
			break;
			case -75:
			{
		 		to = new Vector3(0, 0, 270);
			}
			break;
		}

	}

	public void SetPingPongState(bool isDown)
	{
		if(isDown)
		{
			currentRotationState = eRotationState.scalDown;
		}
		else{
			currentRotationState = eRotationState.scaleUp;
		}
	}
}

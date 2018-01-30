using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class Level
{
	public int cross;
	public int angle;
	public int jellyCount;
	public float rotateSpeed;
	public float zoomSpeed;
}
public class LevelManager : MonoBehaviour {
	public static LevelManager instance;

	public Level []levels; 

	public int currentCross;
	public int currentAngle;
	public int currentJellyCount;
	public float currentRotateSpeed;
	public float currentZoomSpeed; 

	void Awake()
	{
		if(instance == null)
			instance = this;
	}
	public void SetCurrentLevelData(int pLevel)
	{
		currentCross = levels[pLevel].cross;
		currentAngle = levels[pLevel].angle;
		currentJellyCount = levels[pLevel].jellyCount;
		currentRotateSpeed = levels[pLevel].rotateSpeed;
		currentZoomSpeed = levels[pLevel].zoomSpeed;
	}
}

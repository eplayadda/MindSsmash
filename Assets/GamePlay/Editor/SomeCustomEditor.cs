using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SomeCustomObject))]
public class SomeCustomEditor : Editor 
{
	
	private Color topColor;
	private Color bottomColor;
	 SomeCustomObject obj;

	void OnEnable()
	{
		obj = (SomeCustomObject) target;

	}
	public override void OnInspectorGUI()
	{
		
		EditorGUILayout.LabelField("Vertex Colorizer");
		topColor = EditorGUILayout.ColorField("Color",topColor);
		bottomColor = EditorGUILayout.ColorField("Color",bottomColor);
		if(GUI.changed)
		{
			var customObject =target as SomeCustomObject;
			var meshFilter = customObject.GetComponent<MeshFilter>();
			meshFilter.sharedMesh.setVErtexColors(topColor,bottomColor,.75f);
			SceneView.RepaintAll();
		}
	}


}

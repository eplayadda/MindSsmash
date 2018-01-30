using UnityEngine;
using System.Collections.Generic;

public static class MeshExtensions  {
	public static void setVErtexColors(this Mesh mesh,Color topColor,Color bottomColor,float v)
	{
		var maxY = float.MinValue;
		var minY = float.MaxValue;
		for(var i = 0; i<mesh.vertices.Length;i++)
		{
			if(mesh.vertices[i].y > maxY)
				maxY =mesh.vertices[i].y;
			else if(mesh.vertices[i].y<minY)
				minY = mesh.vertices[i].y;

		}
		var meshHeight = minY + maxY;
		var vertColor = new List<Color>();
		for(var i =0;i<mesh.vertexCount;i++)
		{
			var t = mesh.vertices[i].y/meshHeight;
			vertColor.Add (Color.Lerp(topColor,bottomColor,t - v));
		}
		mesh.colors = vertColor.ToArray();
		mesh.RecalculateNormals();
	}
}
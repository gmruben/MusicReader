using UnityEngine;
using System.Collections;

public class ColorPalette
{	
	public static Color menuHighlight = hexToColor("FAE119");
	public static Color menuPressed = hexToColor("969696");

	public static void setMeshToColor(GameObject obj, Color color)
	{
		Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
		
		int length = mesh.vertices.Length;
		Color[] vcolor = new Color[length];
		
		for (int i = 0; i < length; i++)
		{
			vcolor[i] = color;
		}
		
		mesh.colors = vcolor;
	}
	
	public static void setHorizontalGradient(GameObject quad, Color color1, Color color2, float normalizedValue)
	{
		Color interpolateColor = (color1 * (1.0f - normalizedValue)) + (color2 * normalizedValue);
		
		Mesh mesh = quad.GetComponent<MeshFilter>().mesh;
		Color[] vcolor = new Color[4];
		
		vcolor[0] = color1;
		vcolor[1] = interpolateColor;
		vcolor[2] = interpolateColor;
		vcolor[3] = color1;
		
		mesh.colors = vcolor;
	}
	
	public static string colorToHex(Color color)
	{
		int rValue = Mathf.FloorToInt(color.r * 255.0f);
		int gValue = Mathf.FloorToInt(color.g * 255.0f);
		int bValue = Mathf.FloorToInt(color.b * 255.0f);
		
		string hex = rValue.ToString("X2") + gValue.ToString("X2") + bValue.ToString("X2");
		return hex;
	}
	
	public static Color hexToColor(string hex)
	{
		Color32 color;
		
		if (hex == "none")
		{
			color = new Color(0, 0, 0, 255);
		}
		else
		{
			byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
			
			color = new Color32(r, g, b, 255);
		}
		
		return color;
	}
}
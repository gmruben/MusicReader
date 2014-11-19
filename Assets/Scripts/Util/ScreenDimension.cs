using UnityEngine;
using System.Collections;

/// <summary>
/// Stores information about the screen size for UI elements
/// </summary>
public class ScreenDimension
{
	private const int sizeX = 1280;
	private const int sizeY = 720;
	
	private static float _screenSize = 0;
	
	public static float screenSize
	{
		get
		{
			if (_screenSize == 0)
			{
				float ratio = (float) sizeX / sizeY;
				_screenSize = UIManager.instance.camera.aspect / ratio;
			}
			
			return _screenSize;
		}
	}
	
	public static float screenTop
	{
		get { return Camera.main.orthographicSize / screenSize; }
	}
	
	public static float screenBottom
	{
		get { return -Camera.main.orthographicSize / screenSize; }
	}
	
	public static float screenLeft
	{
		get { return -(Camera.main.orthographicSize * Camera.main.aspect) / screenSize; }
	}
	
	public static float screenRight
	{
		get { return (Camera.main.orthographicSize * Camera.main.aspect) / screenSize; }
	}
}
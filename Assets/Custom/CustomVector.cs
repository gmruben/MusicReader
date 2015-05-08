using UnityEngine;

public static class CustomVector
{
	public static Vector3 setX (this Vector3 vector, float x)
	{
		return new Vector3(x, vector.y, vector.z);
	}

	public static Vector3 setY (this Vector3 vector, float y)
	{
		return new Vector3(vector.x, y, vector.z);
	}

	public static Vector3 setZ (this Vector3 vector, float z)
	{
		return new Vector3(vector.x, vector.y, z);
	}

	public static Vector3 V3 (this Vector2 vector)
	{
		return new Vector3(vector.x, vector.y, 0.0f);
	}

	public static Vector2 XY (this Vector3 vector)
	{
		return new Vector2(vector.x, vector.y);
	}

	public static Vector2 YZ (this Vector3 vector)
	{
		return new Vector2(vector.y, vector.z);
	}

	public static Vector2 XZ (this Vector3 vector)
	{
		return new Vector2(vector.x, vector.z);
	}
}
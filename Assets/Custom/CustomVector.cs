using UnityEngine;

public static class CustomVector
{
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
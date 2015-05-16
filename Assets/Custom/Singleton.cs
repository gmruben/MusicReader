using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	private static bool isInit;

	private static T _instance = null;
	public static T instance
	{
		get
		{
			if( _instance == null )
			{
				_instance = GameObject.FindObjectOfType(typeof(T)) as T;
				if( _instance == null ) Debug.LogError("Problem during the creation of " + typeof(T).ToString());

				if (!isInit)
				{
					isInit = true;
					_instance.Init();
				}
			}
			return _instance;
		}
	}

	public abstract void Init();
}
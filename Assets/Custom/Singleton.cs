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
			// Instance requiered for the first time, we look for it
			if( _instance == null )
			{
				_instance = GameObject.FindObjectOfType(typeof(T)) as T;
				
				// Object not found, we create a temporary one
				if( _instance == null )
				{
					Debug.LogError("Problem during the creation of " + typeof(T).ToString());
				}

				if (!isInit)
				{
					isInit = true;
					_instance.init();
				}
			}
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this as T;
		}
		else if (_instance != this)
		{
			Debug.LogError ("Another instance of " + GetType () + " is already exist! Destroying self...");
			DestroyImmediate(this);

			return;
		}

		if (!isInit)
		{
			isInit = true;
			_instance.init();
		}
	}
	
	
	/// <summary>
	/// This function is called when the instance is used the first time
	/// Put all the initializations you need here, as you would do in Awake
	/// </summary>
	public abstract void init();
}
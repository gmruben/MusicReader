using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Specialization of UIButton that stores data and returns it when clicked
/// </summary>
public class UIDataButton<T> : UIButton
{
	public event Action<T> onClick;
	
	private T id;
	
	public void setData(T id)
	{
		this.id = id;
	}
	
	public void onButtonClick()
	{
		if (onClick != null) onClick(id);
	}
}
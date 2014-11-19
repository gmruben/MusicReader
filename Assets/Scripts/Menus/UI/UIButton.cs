using UnityEngine;
using System;
using System.Collections;

public class UIButton : MonoBehaviour
{
	public event Action onClick;
	
	private TextMesh _text;
	
	private Color normalTextColor = Color.white;
	private Color highlightedTextColor = ColorPalette.menuHighlight;
	private Color pressedTextColor = ColorPalette.menuPressed;
	
	private bool hasText = true;
	
	public void setText(string text)
	{
		this.text.text = text;
	}
	
	public void onButtonClick()
	{
		if (onClick != null) onClick();
	}
	
	public void onMouseEnter()
	{
		if (text != null && hasText)
		{
			text.color = highlightedTextColor;
		}
	}
	
	public void onMouseLeave()
	{
		if (text != null && hasText)
		{
			text.color = normalTextColor;
		}
	}
	
	public void onMouseDown()
	{
		if (text != null && hasText)
		{
			text.color = pressedTextColor;
		}
	}
	
	public void onMouseUp()
	{
		if (text != null && hasText)
		{
			text.color = normalTextColor;
		}
	}
	
	private TextMesh text
	{
		get
		{
			if (_text == null && hasText) 
			{
				_text = GetComponentInChildren<TextMesh>();
				if (_text == null) hasText = false;
			}
			
			return _text;
		}
		
		set
		{
			if (_text == null && hasText)
			{
				_text = GetComponentInChildren<TextMesh>();
				if (_text == null) hasText = false;
			}
			
			_text = value;
		}
	}
}
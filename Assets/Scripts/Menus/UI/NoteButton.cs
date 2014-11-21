using UnityEngine;
using System;
using System.Collections;

public class NoteButton : MonoBehaviour
{
	public event Action<int, int> onClick;

	private UIButton uiButton;

	private int indX;
	private int indY;

	public void init(int indX, int indY)
	{
		this.indX = indX;
		this.indY = indY;

		uiButton = GetComponent<UIButton> ();
		uiButton.onClick += onUIButtonClick;
	}

	private void onUIButtonClick()
	{
		if (onClick != null) onClick (indX, indY);
	}
}
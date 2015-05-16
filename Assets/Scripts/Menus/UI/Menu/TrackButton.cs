using UnityEngine;
using System;
using System.Collections;

public class TrackButton : MonoBehaviour
{
	public event Action<TrackData> OnSelect;
	public event Action<TrackData> OnDelete;

	public UIButton selectButton;
	public UIButton deleteButton;

	private TrackData trackData;

	public void Init(TrackData trackData)
	{
		this.trackData = trackData;

		selectButton.setText(trackData.name);

		selectButton.onClick += OnSelectButtonClick;
		deleteButton.onClick += OnDeleteButtonClick;
	}

	private void OnSelectButtonClick()
	{
		if (OnSelect != null) OnSelect(trackData);
	}

	private void OnDeleteButtonClick()
	{
		if (OnDelete != null) OnDelete(trackData);
	}
}
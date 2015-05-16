using UnityEngine;
using UnityEngine.UI;
using System;

public class CreateNewTrackOverlay : UIMenu
{
	public event Action<TrackData> OnCreate;
	public event Action OnCancel;

	public InputField trackNameInput;

	public UIButton createButton;
	public UIButton cancelButton;

	public void Init()
	{
		createButton.onClick += OnCreateButtonClick;
		cancelButton.onClick += OnCancelButtonClick;
	}

	private void OnCreateButtonClick()
	{
		string trackName = trackNameInput.text;
		string trackId = trackName.Trim();

		TrackData trackData = new TrackData(trackId, trackName, InstrumentId.Guitar);
		if (OnCreate != null) OnCreate(trackData);
	}

	private void OnCancelButtonClick()
	{
		if (OnCancel != null) OnCancel();
	}
}
using UnityEngine;
using System;

public class TrackInstrumentList : MonoBehaviour
{
	public event Action<InstrumentId> OnSelect;

	public UIButton guitarButton; 
	public UIButton vocalsButton;

	public void Init()
	{
		guitarButton.onClick += OnGuitarButtonClick;
		vocalsButton.onClick += OnVocalsButtonClick;
	}

	private void OnGuitarButtonClick()
	{
		if (OnSelect != null) OnSelect(InstrumentId.Guitar);
	}

	private void OnVocalsButtonClick()
	{
		if (OnSelect != null) OnSelect(InstrumentId.Vocals);
	}
}
using UnityEngine;
using System.Collections;

public class EditorBar : MonoBehaviour
{
	private const int noteSize = 100;
	private const int lineSize = 50;

	private const int numNotes = 8;
	private const int numLines = 11;

	private Transform cachedTransform;

	private NoteButton[][] noteButtonList;
	private NoteData[] noteDataList;

	public void init()
	{
		cachedTransform = transform;

		noteButtonList = new NoteButton[numNotes][];
		noteDataList = new NoteData[numNotes];

		for (int i = 0; i < numNotes; i++)
		{
			noteButtonList[i] = new NoteButton[numLines];

			for (int j = 0; j < numLines; j++)
			{
				int posx = i * noteSize;
				int posy = -Mathf.FloorToInt((numLines / 2) * lineSize) + (j * lineSize);

				NoteButton noteButton = UICreator.createNoteButton();

				noteButton.transform.parent = cachedTransform;
				noteButton.transform.localPosition = new Vector3(posx, posy, 0);

				noteButton.init(i, j);
				noteButton.onClick += onNoteButtonClick;

				noteButtonList[i][j] = noteButton;
			}
		}
	}

	private void onNoteButtonClick(int indX, int indY)
	{
		Debug.Log ("Note: " + indX + " - " + indY);
		noteDataList[indX] = new NoteData(parseLineToNoteId(indY));
	}

	private string parseLineToNoteId(int lineIndex)
	{
		return NoteIds.notes [lineIndex];
	}
}
using UnityEngine;
using System.Collections;

public class MenuManager : Singleton<MenuManager>
{
	public GameObject changeInstrumentOverlayPrefab;

	public GameObject mainMenuPrefab;
	public GameObject songEditorMenuPrefab;
	public GameObject createNewSongMenuPrefab;
	public GameObject editorSelectSongMenuPrefab;
	public GameObject editSongMenuPrefab;

	public MainMenu instantiateMainMenu()
	{
		return (GameObject.Instantiate(mainMenuPrefab) as GameObject).GetComponent<MainMenu>();
	}

	public SongEditorMenu instantiateSongEditorMenu()
	{
		return (GameObject.Instantiate(songEditorMenuPrefab) as GameObject).GetComponent<SongEditorMenu>();
	}

	public CreateNewSongMenu instantiateCreateNewSongMenu()
	{
		return (GameObject.Instantiate(createNewSongMenuPrefab) as GameObject).GetComponent<CreateNewSongMenu>();
	}

	public EditorSelectSongMenu instantiateEditorSelectSongMenu()
	{
		return (GameObject.Instantiate(editorSelectSongMenuPrefab) as GameObject).GetComponent<EditorSelectSongMenu>();
	}

	public EditSongMenu instantiateEditSongMenu()
	{
		return (GameObject.Instantiate(editSongMenuPrefab) as GameObject).GetComponent<EditSongMenu>();
	}

	public override void Init() { }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class App : Singleton<App>
{
	private Stack<UIMenu> menus = new Stack<UIMenu>();
	
	void Start()
	{	
		ShowTitleMenu();
	}

	public override void Init() { }

	private void ShowTitleMenu()
	{
		MainMenu mainMenu = MenuManager.instance.instantiateMainMenu();
		mainMenu.Init ();
		
		menus.Push (mainMenu);
	}

	public void ShowSongEditorMenu()
	{
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);
		
		SongEditorMenu songEditorMenu = MenuManager.instance.instantiateSongEditorMenu();
		songEditorMenu.Init();
		
		menus.Push(songEditorMenu);
	}

	public void ShowCreateSongMenu()
	{
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);
		
		CreateNewSongMenu createNewSongMenu = MenuManager.instance.instantiateCreateNewSongMenu();
		createNewSongMenu.Init();
		
		menus.Push(createNewSongMenu);
	}

	public void ShowEditorSelectSongMenu()
	{
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);
		
		EditorSelectSongMenu editorSelectSongMenu = MenuManager.instance.instantiateEditorSelectSongMenu();
		editorSelectSongMenu.Init();
		
		menus.Push(editorSelectSongMenu);
	}

	public void ShowEditSongMenu(SongData songData, bool comeFromEditorMenu)
	{
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);
		
		EditSongMenu editSongMenu = MenuManager.instance.instantiateEditSongMenu();
		editSongMenu.Init(songData, comeFromEditorMenu);
		
		menus.Push(editSongMenu);
	}

	public void back()
	{
		back (1);
	}

	public void back(int numPages)
	{
		for (int i = 0; i < numPages; i++)
		{
			UIMenu menu = menus.Pop();
			GameObject.Destroy(menu.gameObject);
		}
		
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(true);
	}
}
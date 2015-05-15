using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EditorCursor : MonoBehaviour
{
	public Transform noteHandler;
	public SpriteRenderer noteSprite;

	public Transform cachedTransform { get; private set; }
	public float noteDuration { get; private set; }

	private Bar currentBar;

	void Start()
	{
		cachedTransform = transform;
		noteSprite = GetComponentInChildren<SpriteRenderer>();
	}

	void Update()
	{
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			updateCursor();
		}
	}

	public void changeSymbol(SymbolId symbolId)
	{
		noteDuration = SymbolData.retrieveDurationById(symbolId);
		noteSprite.sprite = Resources.Load<Sprite>("Sprites/Symbols/" + symbolId);
	}

	private void updateCursor()
	{
		Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).XY();
		transform.position = worldPosition.V3();
		
		bool hasFound = false;
		Collider2D[] colliderList = Physics2D.OverlapPointAll(worldPosition);
		for (int i = 0; i < colliderList.Length; i++)
		{
			currentBar = colliderList[i].GetComponent<Bar>();
			if (currentBar != null)
			{
				noteHandler.gameObject.SetActive(true);

				hasFound = true;
				currentBar.onEnter(this);
				
				break;
			}
		}
		
		if (!hasFound)
		{
			noteHandler.localPosition = Vector3.zero;
			noteHandler.gameObject.SetActive(false);

			if (currentBar != null) currentBar.onExit();
		}
		else
		{
			if (Input.GetMouseButtonDown(0)) currentBar.onClick(this);
		}
	}
}
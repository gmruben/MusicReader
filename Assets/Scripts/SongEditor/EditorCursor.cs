using UnityEngine;
using System.Collections;

public class EditorCursor : MonoBehaviour
{
	private SpriteRenderer noteSprite;

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
		Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).XY();
		transform.position = worldPosition.V3();

		bool hasFound = false;
		Collider2D[] colliderList = Physics2D.OverlapPointAll(worldPosition);
		for (int i = 0; i < colliderList.Length; i++)
		{
			currentBar = colliderList[i].GetComponent<Bar>();
			if (currentBar != null)
			{
				hasFound = true;
				currentBar.onEnter(this);

				break;
			}
		}

		if (!hasFound)
		{
			if (currentBar != null) currentBar.onExit();
		}
		else
		{
			if (Input.GetMouseButtonDown(0)) currentBar.onClick(this);
		}
	}

	public void changeSymbol(SymbolId symbolId)
	{
		noteDuration = SymbolData.retrieveDurationById(symbolId);
		noteSprite.sprite = Resources.Load<Sprite>("Sprites/Symbols/" + symbolId);
	}
}
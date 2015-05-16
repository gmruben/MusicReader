using UnityEngine;
using System.Collections;

public abstract class UIMenu : MonoBehaviour
{
	protected RectTransform rectTransform;
	
	void Start()
	{
		GetComponent<Canvas>().worldCamera = UIManager.instance.camera;
		
		rectTransform = GetComponent<RectTransform>();
		rectTransform.localScale = Vector3.one * ScreenDimension.screenSize;
	}

	public virtual void setActive(bool isActive)
	{
		gameObject.SetActive(isActive);
	}
	
	public virtual void setEnabled(bool isEnabled) { }
}
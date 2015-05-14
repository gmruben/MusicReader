using UnityEngine;
using System.Collections;

public class MultiplierOrb : MonoBehaviour
{
	public GameObject active;
	public GameObject inactive;

	public void SetOrbActive(bool isActive)
	{
		active.SetActive(isActive);
		inactive.SetActive(!isActive);
	}
}
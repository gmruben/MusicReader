using UnityEngine;
using System.Collections;

public class EntityManager : Singleton<EntityManager>
{
	public GameObject barPrefab;
	public GameObject gameBarPrefab;

	public GameObject notePrefab;
	public GameObject gameNotePrefab;

	public override void Init() { }
}
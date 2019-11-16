using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
	public GameObject[] ActivatorPrefabs;

	public GameObject[] ActivatorInstances;

	public bool LoadingCompleted = false;

	private void Start()
	{
		ActivatorInstances = new GameObject[ActivatorPrefabs.Length];

		foreach (GameObject prefab in ActivatorPrefabs)
		{
			GameObject inst = Instantiate(prefab);
			SkillActivator activator = inst.GetComponent<SkillActivator>();

			ActivatorInstances[(int)activator.Type] = inst;
			ActivatorInstances[(int)activator.Type].transform.position = transform.position;
			ActivatorInstances[(int)activator.Type].transform.parent = transform;
		}

		LoadingCompleted = true;
	}

	private void OnEnable()
	{
		foreach (GameObject inst in ActivatorInstances)
		{
			inst.GetComponent<SkillActivator>().enabled = true;
		}
	}
	private void OnDisable()
	{
		foreach (GameObject inst in ActivatorInstances)
		{
			inst.GetComponent<SkillActivator>().enabled = false;
		}
	}
}

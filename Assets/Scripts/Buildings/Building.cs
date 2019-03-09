using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
	public Vector2Int gridLoc;
	public BuildingType myType;
	public City city;
	public Faction faction;
	public List<float> resCount = new List<float>((int)ResourceType.Count);
	public int maxWorkers;
	public int workerCount;

	public virtual void Start()
	{
		for (int i = 0; i < (int)ResourceType.Count; i++)
			resCount.Add(0);
	}

	public void Work()
	{
		for (int i = 0; i < (int)ResourceType.Count; i++)
		{
			city.resCount[i] += resCount[i] * workerCount;
		}
	}

}

public enum BuildingType { Homes, Farm, Wall, Barracks, Count}

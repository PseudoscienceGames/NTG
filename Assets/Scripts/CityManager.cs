using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
	public int cityCount;
	public GameObject cityPrefab;

	private void Start()
	{
		Invoke("AddCities", 1);
	}

	void AddCities()
	{
		for (int i = 0; i < cityCount; i++)
		{
			Transform currentSettlement = (Instantiate(cityPrefab) as GameObject).transform;
			Vector2Int loc = Island.instance.RandomGridLoc();
			currentSettlement.transform.position = HexGrid.GridToWorld(loc, Island.instance.tiles[loc]);
			currentSettlement.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
		}
	}
}

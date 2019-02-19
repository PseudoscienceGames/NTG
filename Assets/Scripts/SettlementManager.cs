using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementManager : MonoBehaviour
{
	public int playerCount = 4;
	public GameObject settlementPrefab;
	public bool addHomes;

	void AddPlayers()
	{
		for (int i = 0; i < playerCount; i++)
		{
			Transform currentSettlement = (Instantiate(settlementPrefab) as GameObject).transform;
			Vector2Int loc = Island.instance.RandomGridLoc();
			currentSettlement.transform.position = HexGrid.GridToWorld(loc, Island.instance.tiles[loc]);
			currentSettlement.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
		}
	}
	private void Update()
	{
		if(addHomes)
		{
			AddPlayers();
			addHomes = false;
		}
	}
}

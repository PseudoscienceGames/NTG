using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionController : MonoBehaviour
{
	public int factionCount;
	public List<Transform> factions = new List<Transform>();

	private void Start()
	{
		Invoke("AddFactions", 0.1f);
	}

	void AddFactions()
	{
		List<Vector2Int> buildableLocs = new List<Vector2Int>();
		foreach(Vector2Int loc in Island.instance.tiles.Keys)
		{
			if (Island.instance.IsBuildable(loc))
				buildableLocs.Add(loc);
		}
		while(factions.Count < factionCount && buildableLocs.Count > 0)
		{
			Transform currentFaction = (Instantiate(Resources.Load("Faction")) as GameObject).transform;

			Vector2Int loc = buildableLocs[Random.Range(0, buildableLocs.Count)];
			currentFaction.GetComponent<Faction>().StartFaction(loc);
			factions.Add(currentFaction);
			currentFaction.transform.SetParent(transform);
			buildableLocs.Remove(loc);
			foreach(Vector2Int adj in HexGrid.FindWithinRadius(loc, 5))
			{
				if (buildableLocs.Contains(adj))
					buildableLocs.Remove(adj);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionController : MonoBehaviour
{
	public static FactionController Instance;
	public int factionCount;
	public List<Faction> factions = new List<Faction>();
	public int playerFaction;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		Invoke("AddFactions", 0.1f);
		InvokeRepeating("Work", 1, 1);
	}

	void AddFactions()
	{
		List<Vector2Int> buildableLocs = new List<Vector2Int>();
		foreach(Vector2Int loc in Island.instance.tiles.Keys)
		{
			if (Island.instance.IsBuildable(loc))
				buildableLocs.Add(loc);
		}
		Debug.Log(buildableLocs.Count);
		while (factions.Count < factionCount && buildableLocs.Count > 0)
		{
			Transform currentFaction = (Instantiate(Resources.Load("Faction")) as GameObject).transform;

			Vector2Int loc = buildableLocs[Random.Range(0, buildableLocs.Count)];
			currentFaction.GetComponent<Faction>().StartFaction(loc);
			factions.Add(currentFaction.GetComponent<Faction>());
			currentFaction.transform.SetParent(transform);
			buildableLocs.Remove(loc);
			foreach(Vector2Int adj in HexGrid.FindWithinRadius(loc, 3))
			{
				if (buildableLocs.Contains(adj))
					buildableLocs.Remove(adj);
			}
		}
		playerFaction = Random.Range(0, factions.Count);
		CameraControl.Instance.FocusCam(factions[playerFaction].gridLoc);
		WorldCursor.Instance.city = factions[playerFaction].cities[0];
	}

	public void Work()
	{
		foreach(Faction f in factions)
		{
			f.Work();
		}
	}
}

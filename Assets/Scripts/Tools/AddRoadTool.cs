using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoadTool : Tool
{

	public override void Click()
	{
		base.Click();
	}

	public override void Hold()
	{
		base.Hold();
	}

	public override void Release()
	{
		base.Release();
		if (startLoc != endLoc)
		{
			List<Vector2Int> toAdd = HexGrid.StraightPath(startLoc, endLoc);
			toAdd.Add(endLoc);
			for (int i = 0; i < toAdd.Count - 1; i++)
				RoadMesh.Instance.AddRoad(toAdd[i], toAdd[i + 1]);
			Faction pf = FactionController.Instance.factions[FactionController.Instance.playerFaction];
			foreach (Vector2Int loc in toAdd)
				pf.AddInfluence(loc);
		}
	}
}

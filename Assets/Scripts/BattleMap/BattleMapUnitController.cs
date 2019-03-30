using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapUnitController : MonoBehaviour
{
	public List<List<BattleAvatar>> teams = new List<List<BattleAvatar>>();
	public List<List<Vector2Int>> spawnLocs = new List<List<Vector2Int>>();
	public bool[] playerTeam;

	public int teamCount;
	public int unitCount;

	public void Start()
	{
		playerTeam = new bool[teamCount];
		playerTeam[0] = true;
		List<List<Unit>> t = new List<List<Unit>>();
		List<List<Vector2Int>> s = new List<List<Vector2Int>>();
		for (int i = 0; i < teamCount; i++)
		{
			List<Unit> units = new List<Unit>();
			for (int j = 0; j < unitCount; j++)
			{
				Unit unit = new Unit();
				unit.name = "bob" + i + "" + j;
				units.Add(unit);
			}
			t.Add(units);
			s.Add(HexGrid.FindAdjacentGridLocs(HexGrid.MoveTo(Vector2Int.zero, i, 12)));
			
		}
		SpawnUnits(t, s);
		GetComponent<Initiative>().SetInitiative();
	}
	public void SpawnUnits(List<List<Unit>> t, List<List<Vector2Int>> s)
	{
		spawnLocs = s;
		for (int i = 0; i < t.Count; i++)
		{
			teams.Add(new List<BattleAvatar>());
			for (int j = 0; j < t[i].Count; j++)
			{
				BattleAvatar currentAvi = (Instantiate(Resources.Load("UnitAvatar"), HexGrid.GridToWorld(spawnLocs[i][j]), Quaternion.identity) as GameObject).GetComponent<BattleAvatar>();
				currentAvi.unit = t[i][j];
				currentAvi.gridLoc = spawnLocs[i][j];
				teams[i].Add(currentAvi);
				if (playerTeam[i])
					currentAvi.isPlayer = true;

			}
		}
	}
}

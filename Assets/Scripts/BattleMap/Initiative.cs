using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initiative : MonoBehaviour
{
	public static Initiative Instance;
	void Awake() { Instance = this; }
	public List<BattleAvatar> units = new List<BattleAvatar>();

	public ActiveUnitController activeUnitMarker;

	public void SetInitiative()
	{
		List<BattleAvatar> tempUnits = new List<BattleAvatar>();
		BattleMapUnitController bmut = GetComponent<BattleMapUnitController>();
		for (int i = 0; i < bmut.teams.Count; i++)
		{
			tempUnits.AddRange(bmut.teams[i]);
		}
		while(tempUnits.Count > 0)
		{
			int i = Random.Range(0, tempUnits.Count);
			units.Add(tempUnits[i]);
			tempUnits.RemoveAt(i);
		}
		activeUnitMarker.SelectUnit(units[0]); 
		NextTurn();
	}

	public void NextTurn()
	{
		activeUnitMarker.SelectUnit(units[0]);
		BattleAvatar unit = units[0];
		units.Add(unit);
		units.RemoveAt(0);
		unit.Act();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyDisplay : MonoBehaviour
{
	public Army army;
	public GameObject squadPrefab;
	public LayerMask ground;
	public bool sort;

	private void Start()
	{
		if (army == null)
			army = new Army();
		foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
		{
			army.units.Add(unit.GetComponent<Unit>());
			unit.GetComponent<Unit>().squad = null;
		}
		SortUnits();
	}

	private void Update()
	{
		if (sort)
		{
			sort = false;
			SortUnits();
		}
	}

	private void SortUnits()
	{
		for (int i = 0; i < army.squads.Count; i++)
		{
			if (army.squads[i].units.Count > 0)
			{
				army.squads[i].transform.position = (new Vector3(-i / 4, 0, 4 - (i % 4)) * 4) + new Vector3(-2, 0, -1);
			}
			else
			{
				Destroy(army.squads[i].gameObject);
				army.squads.RemoveAt(i);
			}
		}
		int l = 0;
		foreach (Unit u in army.units)
		{
			if (u.squad == null)
			{
				u.transform.position = (new Vector3(l / 8, 0, 8 - (l % 8)) * 2) + new Vector3(1, 0, 0);
				l++;
			}
			else
			{
				int j = u.squad.units.IndexOf(u);
				u.squad.units[j].transform.position = u.squad.transform.position + new Vector3((j % 3) - 1, 1f / 3f, (j / 3) - 1);
			}
		}
	}

	public void ReleaseUnit(Unit unit)
	{
		int index = army.units.IndexOf(unit);
		if (unit.squad == null)
		{
			if (unit.transform.position.x > 0)
				SortUnits();
			else
			{
				RaycastHit hit;
				if (Physics.Raycast(unit.transform.position + (Vector3.up * 10), -Vector3.up, out hit, 100, ground))
				{
					if (hit.transform.CompareTag("SquadPlat"))
					{
						AddToSquad(hit.transform.root.GetComponent<Squad>(), unit);
					}
					else
					{
						AddSquad(unit);
					}
				}
			}
		}
		else
		{
			if (unit.transform.position.x > 0)
			{
				unit.squad.units.Remove(unit);
				unit.squad = null;
				SortUnits();
			}
			else
			{
				RaycastHit hit;
				if (Physics.Raycast(unit.transform.position + (Vector3.up * 10), -Vector3.up, out hit, 100, ground))
				{
					if (hit.transform.CompareTag("SquadPlat"))
					{
						if (hit.transform.root.GetComponent<Squad>() != unit.squad)
						{
							Squad squad = hit.transform.root.GetComponent<Squad>();
							unit.squad.units.Remove(unit);
							AddToSquad(squad, unit);
						}
					}
					else
					{
						unit.squad.units.Remove(unit);
						AddSquad(unit);
					}
				}
			}
		}
		SortUnits();
	}

	private void AddToSquad(Squad squad, Unit unit)
	{
		if (squad.units.Count < 6)
		{
			squad.units.Add(unit);
			unit.squad = squad;
		}
		SortUnits();
	}

	private void AddSquad(Unit unit)
	{
		Squad squad = (Instantiate(squadPrefab) as GameObject).GetComponent<Squad>();
		army.squads.Add(squad);
		AddToSquad(squad, unit);

	}
}

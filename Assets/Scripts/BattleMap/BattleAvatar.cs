using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAvatar : MonoBehaviour
{
	public Unit unit;

	public bool isPlayer;

	public void Act()
	{
		if (isPlayer)
			ActivatePlayerControls();
		else
			ActivateAIControls();
	}

	void ActivatePlayerControls()
	{
		Debug.Log(unit.name + " - Player");
	}
	void ActivateAIControls()
	{
		Debug.Log(unit.name + " - AI");
		Invoke("NextTurn", 0.1f);
	}

	void NextTurn()
	{
		Initiative.Instance.NextTurn();
	}
}

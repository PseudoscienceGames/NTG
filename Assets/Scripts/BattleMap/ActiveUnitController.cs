using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUnitController : MonoBehaviour
{
	public BattleAvatar unit;
	public Transform moveMarker;
	public void SelectUnit(BattleAvatar u)
	{
		unit = u;
		transform.position = u.transform.position;
	}
	private void Update()
	{
		if(Physics.Raycast())
		moveMarker.position = 
	}
}

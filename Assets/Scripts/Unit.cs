using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public Vector2Int gridLoc;
	public Vector2Int target = new Vector2Int(9999, 9999);
	public float speed = 1;

	private void Update()
	{
		if(target != new Vector2Int(9999, 9999))
		{
			transform.position += (HexGrid.GridToWorld(target) - transform.position).normalized * speed * Time.deltaTime;
		}
		gridLoc = HexGrid.RoundToGrid(transform.position);
		if (Vector3.Distance(transform.position, HexGrid.GridToWorld(target)) < 0.025f)
		{
			transform.position = HexGrid.GridToWorld(target);
			target = new Vector2Int(9999, 9999);
		}
	}
}

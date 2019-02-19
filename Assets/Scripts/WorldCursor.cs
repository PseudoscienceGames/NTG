using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCursor : MonoBehaviour
{
	public LayerMask mask;
	private void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, mask))
		{
			Vector2Int l = HexGrid.RoundToGrid(hit.point);
			transform.position = HexGrid.GridToWorld(l, Island.instance.tiles[l]);
		}
	}
}

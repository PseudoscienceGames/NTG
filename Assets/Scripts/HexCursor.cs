using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCursor : MonoBehaviour
{
	public Vector2Int gridLoc;
	public LayerMask mask;

	public static HexCursor Instance;
	private void Awake(){ Instance = this; }

	void Update()
    {
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000, mask))
		{
			gridLoc = HexGrid.RoundToGrid(hit.point);
			transform.position = HexGrid.GridToWorld(gridLoc, Mathf.RoundToInt(hit.point.y / 3f));

		}
	}
}

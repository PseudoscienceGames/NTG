using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControls : MonoBehaviour
{
	public LayerMask mask;
	public Transform selectedUnit;
	public Transform selectionMarker;
	Vector3 startPos = new Vector3();

	void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000, mask))
				SelectUnit(hit.transform);
			else
				DeselectUnit();
		}
		if(selectedUnit != null)
		{
			if (Input.GetMouseButtonDown(1))
			{
				startPos = Input.mousePosition;
			}
			if (Input.GetMouseButtonUp(1) && Vector3.Distance(startPos, Input.mousePosition) < 5)
			{
				selectedUnit.GetComponent<Unit>().target = Cursor.Instance.gridLoc;
			}
		}
    }

	public void SelectUnit(Transform unit)
	{
		selectedUnit = unit;
		CameraControl.Instance.FocusCam(HexGrid.RoundToGrid(unit.position));
		selectionMarker.SetParent(selectedUnit);
		selectionMarker.localPosition = Vector3.zero;
	}
	public void DeselectUnit()
	{
		selectedUnit = null;
		selectionMarker.SetParent(null);
		selectionMarker.position = -Vector3.up;
	}
}

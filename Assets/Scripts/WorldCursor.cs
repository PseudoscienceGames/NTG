using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCursor : MonoBehaviour
{
	public static WorldCursor Instance;
	public LayerMask mask;
	public List<Tool> tools = new List<Tool>();
	public Tool activeTool;
	public Vector2Int gridLoc;
	public bool overUI;

	public City city;
	public Mesh cursorMesh;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (!overUI)
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000, mask))
			{
				Vector2Int l = HexGrid.RoundToGrid(hit.point);
				gridLoc = l;
				transform.position = HexGrid.GridToWorld(l, Island.instance.tiles[l]);
			}
			if (activeTool != null)
			{
				if (Input.GetMouseButtonDown(0))
					activeTool.Click();
				if (Input.GetMouseButton(0))
					activeTool.Hold();
				if (Input.GetMouseButtonUp(0))
					activeTool.Release();
			}
			if (Input.GetMouseButtonUp(1))
			{
				DeactivateTool();
			}
		}

	}

	public void ActivateTool(int tool)
	{
		activeTool = tools[tool];
		GetComponent<MeshFilter>().mesh = tools[tool].mesh;
	}
	public void DeactivateTool()
	{
		activeTool = null;
		GetComponent<MeshFilter>().mesh = cursorMesh;
	}
	public void OverUI()
	{
		overUI = true;
	}
	public void NotOverUI()
	{
		overUI = false;
	}

}

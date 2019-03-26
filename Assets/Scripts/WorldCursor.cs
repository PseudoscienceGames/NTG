using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldCursor : MonoBehaviour
{
	public static WorldCursor Instance;
	public LayerMask mask;
	public List<Tool> tools = new List<Tool>();
	public Tool activeTool;
	public Vector2Int gridLoc;
	public bool overUI;
	public Tile selectedTile;

	public City city;
	public Mesh cursorMesh;
	public Vector2 startPos;

	public Text gridLocText;
	public Text heightText;
	public Text occupantsText;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (!overUI)
		{
			if(Input.GetMouseButtonDown(1))
			{
				startPos = Input.mousePosition;
			}
			if (Input.GetMouseButtonUp(1))
			{
				if(Vector2.Distance(startPos, Input.mousePosition) < 1)
					DeactivateTool();
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000, mask))
			{
				Vector2Int l = HexGrid.RoundToGrid(hit.point);
				gridLoc = l;
				transform.position = HexGrid.GridToWorld(l, Island.instance.tiles[l].height);
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
			else
			{
				if (Input.GetMouseButtonDown(0))
					SelectTile();
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
	public void SelectTile()
	{
		selectedTile = Island.instance.tiles[gridLoc];
		gridLocText.text = selectedTile.gridLoc.ToString();
		heightText.text = "Height: " + selectedTile.height;
		string occ = "Occupants:\n";
		foreach(GameObject o in selectedTile.occupants)
		{
			occ += o.name + "\n";
		}
		occupantsText.text = occ;
	}
}

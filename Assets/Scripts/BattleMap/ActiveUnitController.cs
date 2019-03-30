using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUnitController : MonoBehaviour
{
	public BattleAvatar unit;
	public Transform moveMarker;
	public Vector2Int markerGridLoc;
	Camera cam;
	public LayerMask terrain;

	private void Start()
	{
		cam = Camera.main;
	}
	public void SelectUnit(BattleAvatar u)
	{
		unit = u;
		transform.position = u.transform.position;
	}
	private void Update()
	{
		if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, terrain))
		{
			moveMarker.position = HexGrid.GridToWorld(HexGrid.RoundToGrid(hit.point));
		}
		if (HexGrid.RoundToGrid(moveMarker.position) != markerGridLoc)
		{
			markerGridLoc = HexGrid.RoundToGrid(moveMarker.position);
			DrawPath(Path(unit.gridLoc, markerGridLoc));
		}
	}

	public void DrawPath(List<Vector2Int> path)
	{
		for (int i = 0; i < path.Count - 1; i++)
		{
			Debug.DrawLine(HexGrid.GridToWorld(path[i]), HexGrid.GridToWorld(path[i + 1]), Color.red, 10);
		}
	}

	public List<Vector2Int> Path(Vector2Int start, Vector2Int goal)
	{
		List<Vector2Int> path = new List<Vector2Int>();
		List<Node> openSet = new List<Node>();//to check
		List<Node> closedSet = new List<Node>();//checked
		openSet.Add(new Node(start));
		Node node = openSet[0];
		while (openSet.Count > 0)
		{
			closedSet.Add(node);
			openSet.Remove(node);
			Vector2Int loc = node.gridLoc;
			List<Node> adjNodes = new List<Node>();
			foreach (Vector2Int adj in HexGrid.FindAdjacentGridLocs(loc))
			{
				if (BattleMapData.Instance.gridLocs.Contains(adj) && !openSet.Contains(node) && closedSet.Contains(node))
					adjNodes.Add(new Node(adj, node, node.gCost + 1, Vector3.Distance(HexGrid.GridToWorld(goal), HexGrid.GridToWorld(adj))));
			}
			foreach (Node adj in adjNodes)
			{
				if (adj.hCost < node.hCost)
					node = adj;
			}
		}
		f

		return path;
	}
}

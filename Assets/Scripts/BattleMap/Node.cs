using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public Vector2Int gridLoc;
	public float gCost;
	public float hCost;
	public float fCost;
	public Node parentNode;

	public Node(Vector2Int loc, Node p, float g, float h)
	{
		gridLoc = loc;
		parentNode = p;
		gCost = g;
		hCost = h;
		fCost = g + h;
	}

	public Node(Vector2Int loc)
	{
		gridLoc = loc;
	}
}

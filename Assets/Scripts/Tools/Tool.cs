using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
	public Vector2Int startLoc;
	public Vector2Int endLoc;
	public Mesh mesh;

    public virtual void Click()
	{
		startLoc = WorldCursor.Instance.gridLoc;
	}

	public virtual void Hold()
	{

	}

	public virtual void Release()
	{
		endLoc = WorldCursor.Instance.gridLoc;
	}
}

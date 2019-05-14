using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IslandData
{
	public List<Vector2Int> gridLocs = new List<Vector2Int>();
	public List<WorldTile> tiles = new List<WorldTile>();
	Dictionary<Vector2Int, List<Vector2Int>> connections = new Dictionary<Vector2Int, List<Vector2Int>>();
	List<List<Vector2Int>> blobs = new List<List<Vector2Int>>();
	Dictionary<Vector2Int, int> heights = new Dictionary<Vector2Int, int>();
	public int blobCount;

	void BuildConnections()
	{
		foreach(WorldTile w in tiles)
		{
			foreach(Vector3 v in w.verts)
			{
				Vector2Int k = new Vector2Int(Mathf.RoundToInt(v.x * 10), Mathf.RoundToInt(v.z * 10));
				if (!connections.ContainsKey(k))
				{
					connections.Add(k, new List<Vector2Int>());
				}
			}
		}
		foreach(Vector2Int c1 in connections.Keys)
		{
			foreach (Vector2Int c2 in connections.Keys)
			{
				if(!connections[c1].Contains(c2) && Vector2Int.Distance(c1, c2) < 6.5f && c1 != c2)
				{
					connections[c1].Add(c2);
				}
			}
		}
	}
	void FindEdge()
	{
		List<Vector2Int> blob = new List<Vector2Int>();
		foreach (Vector2Int v in connections.Keys)
		{

			if (connections[v].Count != 3)
			{
				blob.Add(v);
			}
		}
		int c = blob.Count;
		for (int i = 0; i < c; i++)
		{
			foreach (Vector2Int con in connections[blob[i]])
			{
				if (!blob.Contains(con))
					blob.Add(con);
			}
		}
		blobs.Add(blob);
	}
	void FindBlobs()
	{


		List<Vector2Int> left = new List<Vector2Int>();
		foreach (Vector2Int v in connections.Keys)
		{
			if (!blobs[0].Contains(v) && !left.Contains(v))
				left.Add(v);
		}
		if (left.Count > 0)
		{
			blobCount = connections.Count / 20;
			if (blobCount == 0)
				blobCount = 1;
			for (int i = 0; i < blobCount; i++)
			{
				List<Vector2Int> blob = new List<Vector2Int>();
				Vector2Int loc = left[Random.Range(0, left.Count)];
				blob.Add(loc);
				left.Remove(loc);
				blobs.Add(blob);
			}
			int y = 0;
			while (left.Count > 0 && y < 1000000)
			{
				y++;
				if (y == 1000000)
					Debug.Log("Y");
				int x = Random.Range(1, blobs.Count);
				//if (blobs.Count == 1)
				//	Debug.Log(blobs.Count + " " + x);

				List<Vector2Int> blob = blobs[x];
				Vector2Int loc = blob[Random.Range(0, blob.Count)];
				foreach (Vector2Int con in connections[loc])
				{
					if (left.Contains(con))
					{
						blob.Add(con);
						left.Remove(con);
					}
				}
			}
		}
	}
	public void CalcHeights()
	{
		BuildConnections();
		FindEdge();
		FindBlobs();
		//Debug.Log(connections.Count + " " + blobs[0].Count + "  " + blobs[1].Count);
		for (int i = 0; i < blobs.Count; i++)
		{
			int h = Random.Range(1, 4);
			foreach(Vector2Int v in blobs[i])
			{
				if(i == 0)
					heights.Add(v, 0);
				else
					heights.Add(v, h);
			}
		}
		foreach (WorldTile w in tiles)
		{
			for(int i = 0; i < w.heights.Count; i++)
			{
				w.heights[i] = heights[new Vector2Int(Mathf.RoundToInt(w.verts[i].x * 10), Mathf.RoundToInt(w.verts[i].z * 10))];
			}
			w.CalcTopo();
		}
	}
}

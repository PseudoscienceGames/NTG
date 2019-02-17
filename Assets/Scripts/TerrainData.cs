using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainData : MonoBehaviour
{
	public int size = 100;
	public int blobCount;
	public Dictionary<Vector2Int, Vector3> locs = new Dictionary<Vector2Int, Vector3>();
	public List<Blob> blobs = new List<Blob>();
	TerrainMesh mesh;

	private void Start()
	{
		mesh = GetComponent<TerrainMesh>();
		GenTerrain();
		List<Vector2Int> left = new List<Vector2Int>(locs.Keys);
		for (int i = 0; i < blobCount; i++)
		{
			Blob blob = new Blob();
			blobs.Add(blob);
			Vector2Int gridLoc = left[Random.Range(0, left.Count)];
			blob.gridLocs.Add(gridLoc);
			left.Remove(gridLoc);
		}
		for (int i = 0; i < left.Count; i++)
		{
			float dis = 100000;
			Blob closest = new Blob();
			foreach(Blob b in blobs)
			{
				if (Vector2Int.Distance(left[i], b.gridLocs[0]) < dis)
				{
					closest = b;
					dis = Vector2Int.Distance(left[i], b.gridLocs[0]);
				}
			}
			closest.gridLocs.Add(left[i]);
		}
		FindAdjBlobs();
		BlobHeights();
		foreach(Blob b in blobs)
		{
			int x = b.level;
			foreach (Vector2Int l in b.gridLocs)
			{
				locs[l] += new Vector3(0, x/2f, 0);
			}
		}
		mesh.GenMesh();
	}

	void GenTerrain()
	{
		size /= 2;
		for (int x = -size; x < size; x++)
		{
			for (int y = -size; y < size; y++)
			{
				Vector3 worldLoc = new Vector3();
				worldLoc.x = x;
				if (y % 2 != 0)
					worldLoc.x += 0.5f;
				worldLoc.z = y * (Mathf.Sqrt(3) * 0.5f);
				locs[new Vector2Int(x, y)] = worldLoc;
			}
		}
	}

	Blob FindBlob(Vector2Int loc)
	{
		foreach(Blob b in blobs)
		{
			if (b.gridLocs.Contains(loc))
				return b;
		}
		return null;
	}

	void FindAdjBlobs()
	{
		foreach(Blob blob in blobs)
		{
			foreach(Vector2Int loc in blob.gridLocs)
			{
				for (int i = 0; i < 6; i++)
				{
					Vector2Int adjLoc = HexGrid.Move(loc, i);
					if (locs.ContainsKey(adjLoc))
					{
						if (!blob.gridLocs.Contains(adjLoc))
						{
							Blob otherBlob = FindBlob(adjLoc);
							if (!blob.adjBlobs.Contains(otherBlob))
								blob.adjBlobs.Add(otherBlob);
						}
					}
					else blob.level = 1;
				}
			}
		}
	}
	void BlobHeights()
	{
		List<Blob> left = new List<Blob>(blobs);
		foreach(Blob b in blobs)
		{
			if (b.level == 1)
				left.Remove(b);
		}
		int x = 2;
		while(left.Count > 0)
		{
			List<Blob> ring = new List<Blob>();
			foreach(Blob b in left)
			{
				foreach(Blob adj in b.adjBlobs)
				{
					if (!left.Contains(adj) && !ring.Contains(b))
						ring.Add(b);
				}
			}
			foreach(Blob r in ring)
			{
				r.level = x;
				left.Remove(r);
			}
			x++;
		}
		foreach(Blob b in blobs)
		{
			if (Random.Range(0, 5) == 2 && b.level > 1)
				b.level--;
		}
	}
}

public class Blob
{
	public int level;
	public List<Vector2Int> gridLocs = new List<Vector2Int>();
	public List<Vector2Int> posLocs = new List<Vector2Int>();
	public List<Blob> adjBlobs = new List<Blob>();
}

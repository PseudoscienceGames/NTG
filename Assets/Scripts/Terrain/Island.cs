using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
	public static Island instance;
	void Awake() { instance = this; }

	public bool randomSeed = false;
	public int seed;
	public int islandSize;
	public int blobPerChunk;

	public List<Blob> blobs = new List<Blob>();
	public List<Vector2Int> chunks = new List<Vector2Int>();
	public List<ChunkData> chunkDatas = new List<ChunkData>();
	public Dictionary<Vector2Int, int> tiles = new Dictionary<Vector2Int, int>();
	public int tileCount;

	public GameObject treePrefab;

	public void Start()
	{
		if(randomSeed)
			seed = Random.Range(int.MinValue, int.MaxValue);
		if (islandSize < 7)
			islandSize = 7;
		Random.InitState(seed);
		GenIslandData();
		Debug.Log(Time.realtimeSinceStartup);
	}
	public void GenIslandData()
	{
		AddChunks();
		AddTiles();
		List<Vector2Int> left = new List<Vector2Int>(tiles.Keys);
	
		for (int i = 0; i < blobPerChunk * islandSize; i++)
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
			foreach (Blob b in blobs)
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
		foreach (Blob b in blobs)
		{
			int x = b.level;
			foreach (Vector2Int l in b.gridLocs)
			{
				tiles[l] = x;
			}
		}
		foreach (ChunkData cd in chunkDatas)
			cd.GetComponent<ChunkMesh>().GenMesh();
		tileCount = tiles.Count;

		AddForest();
	}

	private void AddChunks()
	{
		List<Vector2Int> possLocs = new List<Vector2Int>();
		chunks.Add(Vector2Int.zero);
		chunks.AddRange(ChunkMath.FindAdjacentChunks(Vector2Int.zero));
		foreach(Vector2Int chunk in chunks)
		{
			foreach(Vector2Int adj in ChunkMath.FindAdjacentChunks(chunk))
			{
				if (!chunks.Contains(adj) && !possLocs.Contains(adj))
					possLocs.Add(adj);
			}
		}
		while (chunks.Count < islandSize)
		{
			Vector2Int toAdd = possLocs[Mathf.RoundToInt(Random.Range(0, possLocs.Count - 1))];
			int adjLand = 0;
			foreach(Vector2Int adj in ChunkMath.FindAdjacentChunks(toAdd))
			{
				if (chunks.Contains(adj))
					adjLand++;
			}
			if (adjLand >= 2)
			{
				chunks.Add(toAdd);
				possLocs.Remove(toAdd);
				foreach (Vector2Int v in ChunkMath.FindAdjacentChunks(toAdd))
				{
					if (!chunks.Contains(v) && !possLocs.Contains(v))
						possLocs.Add(v);
				}
			}
			else
			{
				possLocs.Remove(toAdd);
			}
		}
		foreach (Vector2Int chunk in chunks)
		{
			GameObject chunkGO = Instantiate(Resources.Load("ChunkPrefab")) as GameObject;
			ChunkData cd = chunkGO.GetComponent<ChunkData>();
			chunkDatas.Add(cd);
			cd.gridLoc = chunk;
			cd.transform.position = HexGrid.GridToWorld(cd.gridLoc);
			cd.transform.SetParent(transform);
			cd.Initialize();
		}
	}
	private void AddTiles()
	{
		foreach (Vector2Int chunk in chunks)
		{
			foreach (Vector2Int gl in HexGrid.FindWithinRadius(chunk, ChunkMath.chunkRadius))
			{
				tiles.Add(gl, 0);
			}
		}
	}
	public int GetHeight(Vector2Int gridLoc)
	{
		if (tiles.ContainsKey(gridLoc))
			return tiles[gridLoc];
		else
			return -1;
	}
	public ChunkData FindChunk(Vector2Int gridLoc)
	{

		foreach(ChunkData cd in chunkDatas)
		{
			if(cd.tiles.Contains(gridLoc))
			{
				return cd;
			}
		}
		return null;
	}

	Blob FindBlob(Vector2Int loc)
	{
		foreach (Blob b in blobs)
		{
			if (b.gridLocs.Contains(loc))
				return b;
		}
		return null;
	}

	void FindAdjBlobs()
	{
		foreach (Blob blob in blobs)
		{
			foreach (Vector2Int loc in blob.gridLocs)
			{
				for (int i = 0; i < 6; i++)
				{
					Vector2Int adjLoc = HexGrid.MoveTo(loc, i);
					if (tiles.ContainsKey(adjLoc))
					{
						if (!blob.gridLocs.Contains(adjLoc))
						{
							Blob otherBlob = FindBlob(adjLoc);
							if (!blob.adjBlobs.Contains(otherBlob))
								blob.adjBlobs.Add(otherBlob);
						}
					}
					else blob.level = 0;
				}
			}
		}
	}
	void BlobHeights()
	{
		List<Blob> left = new List<Blob>(blobs);
		foreach (Blob b in blobs)
		{
			if (b.level == 0)
				left.Remove(b);
		}
		int x = 1;
		while (left.Count > 0)
		{
			List<Blob> ring = new List<Blob>();
			foreach (Blob b in left)
			{
				foreach (Blob adj in b.adjBlobs)
				{
					if (!left.Contains(adj) && !ring.Contains(b))
						ring.Add(b);
				}
			}
			foreach (Blob r in ring)
			{
				r.level = x;// + Random.Range(0, 2);
				left.Remove(r);
				if (Random.Range(0, 3) == 0)
					r.forest = true;
			}
			x++;
		}
	}
	void AddForest()
	{
		foreach(Blob b in blobs)
		{
			if(b.forest)
			{
				foreach(Vector2Int l in b.gridLocs)
				{
					GameObject currentTree = Instantiate(treePrefab, HexGrid.GridToWorld(l, tiles[l]), Quaternion.Euler(0, Random.Range(0, 360), 0)) as GameObject;
					currentTree.transform.localScale *= Random.Range(0.75f, 1.25f);
					currentTree.transform.SetParent(transform);
				}
			}
		}
	}
	public Vector2Int RandomGridLoc()
	{
		int i = Random.Range(0, tiles.Count);
		return new List<Vector2Int> (tiles.Keys)[i];
	}
	public bool IsBuildable(Vector2Int loc)
	{
		bool buildable = true;
		foreach(Vector2Int l in HexGrid.FindAdjacentGridLocs(loc))
		{
			if (tiles[loc] != tiles[l])
				buildable = false;
		}
		if(FindBlob(loc).forest)
			buildable = false;
		if(tiles[loc] <= 0)
			buildable = false;
		return buildable;
	}
}
public class Blob
{
	public int level = 1;
	public List<Vector2Int> gridLocs = new List<Vector2Int>();
	public List<Vector2Int> posLocs = new List<Vector2Int>();
	public List<Blob> adjBlobs = new List<Blob>();
	public bool forest = false;
}
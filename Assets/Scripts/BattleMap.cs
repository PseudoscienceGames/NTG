using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{
	public GameObject battleChunkPrefab;
	public List<BattleChunkData> chunks = new List<BattleChunkData>();
	public bool go = false;

	private void Update()
	{
		if (go)
		{
			List<WorldTile> wts = new List<WorldTile>();
			wts.Add(WorldData.Instance.tiles[Vector2Int.zero]);
			foreach(Vector2Int v in HexGrid.FindAdjacentGridLocs(Vector2Int.zero))
			{
				wts.Add(WorldData.Instance.tiles[v]);
			}

			GenBattleMap(wts);
			go = false;
		}
	}

	public void GenBattleMap(List<WorldTile> worldTiles)
	{
		foreach(WorldTile wt in worldTiles)
		{
			GenBattleChunk(wt);
		}
	}

	void GenBattleChunk(WorldTile worldTile)
	{
		BattleChunkData battleChunk = (Instantiate(battleChunkPrefab) as GameObject).GetComponent<BattleChunkData>();
		battleChunk.wt = worldTile;
		battleChunk.GenChunkData();
		chunks.Add(battleChunk);
	}
}

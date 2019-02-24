using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class InfluenceMesh : MonoBehaviour
{
	public float fill;
	public void DrawInfluence(List<Vector2Int> influence)
	{
		List<Vector3> verts = new List<Vector3>();
		List<int> tris = new List<int>();
		int triNum = 0;
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		List<Vector2Int> edge = new List<Vector2Int>();
		List<Vector2Int> inland = new List<Vector2Int>();
		foreach(Vector2Int i in influence)
		{
			bool e = false;
			foreach(Vector2Int adj in HexGrid.FindAdjacentGridLocs(i))
			{
				if (!influence.Contains(adj))
					e = true;
			}
			if (e)
				edge.Add(i);
			else
				inland.Add(i);
		}

		foreach(Vector2Int i in inland)
		{
			List<Vector2Int> adj = HexGrid.FindAdjacentGridLocs(i);
			for (int a = 0; a < 6; a++)
			{
				Vector2Int loc1 = adj[a];
				Vector2Int loc2 = adj[HexGrid.MoveDirFix(a + 1)];
				if (edge.Contains(loc1) && edge.Contains(loc2))
				{
					Vector3 worldLoc1 = Island.instance.GridToWorld(loc1) + (Vector3.up * 0.01f);
					Vector3 worldLoc2 = Island.instance.GridToWorld(loc2) + (Vector3.up * 0.01f);
					Vector3 worldLoc3 = Island.instance.GridToWorld(i) + (Vector3.up * 0.01f);

					verts.Add(worldLoc1);
					verts.Add(worldLoc2);
					verts.Add(Vector3.Lerp(worldLoc2, worldLoc3, fill));
					verts.Add(Vector3.Lerp(worldLoc1, worldLoc3, fill));
					tris.Add(triNum);
					tris.Add(triNum + 1);
					tris.Add(triNum + 2);
					tris.Add(triNum);
					tris.Add(triNum + 2);
					tris.Add(triNum + 3);
					triNum += 4;

				}
			}
		}
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
	}
}

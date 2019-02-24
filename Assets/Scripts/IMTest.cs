using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Invoke("DrawInfluenceMesh", 1);
    }
	void DrawInfluenceMesh()
	{
		List<Vector2Int> i = new List<Vector2Int>();
		i.Add(Vector2Int.zero);
		i.AddRange(HexGrid.FindWithinRadius(Vector2Int.zero, 4));
		GetComponent<InfluenceMesh>().DrawInfluence(i);
	}
}

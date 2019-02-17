using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCursor : MonoBehaviour
{
	public TerrainData td;
	public LayerMask ground;

	private void Update()
	{
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, ground))
		{
			transform.position = 
		}
	}
}

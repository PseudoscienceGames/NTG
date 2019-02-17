using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexGrid
{
	public static Vector2Int Move(Vector2Int loc, int dir)
	{
		Vector2Int moved = loc;
		switch(dir)
		{
			case 0:
				if (moved.y % 2 == 0)
				{
					moved.x--;
					moved.y++;
				}
				else
				{
					moved.y++;
				}
				break;
			case 1:
				if (moved.y % 2 == 0)
				{
					moved.y++;
				}
				else
				{
					moved.x++;
					moved.y++;
				}
				break;
			case 2:
				moved.x++;
				break;
			case 3:
				if (moved.y % 2 == 0)
				{
					moved.y--;
				}
				else
				{
					moved.x++;
					moved.y--;
				}
				break;
			case 4:
				if (moved.y % 2 == 0)
				{
					moved.x--;
					moved.y--;
				}
				else
				{
					moved.y--;
				}
				break;
			case 5:
				moved.x--;
				break;
		}
		return moved;
	}
}

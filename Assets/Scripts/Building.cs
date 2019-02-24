using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
	public Vector2Int gridLoc;
	public BuildingType myType;
	public City city;
	public Faction faction;
}

public enum BuildingType { Huts, Farm, Wall}

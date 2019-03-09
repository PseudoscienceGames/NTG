using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
	public void TrainSoldier()
	{
		city.pop--;
		faction.army.AddSoldier();
	}
}

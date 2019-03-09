using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
	public override void Start()
	{
		base.Start();
		resCount[(int)ResourceType.Food] = 5;
		maxWorkers = 5;
	}
}

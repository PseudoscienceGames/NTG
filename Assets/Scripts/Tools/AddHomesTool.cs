﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHomesTool : Tool
{
	public override void Click()
	{
		base.Click();
	}

	public override void Hold()
	{
		base.Hold();
	}

	public override void Release()
	{
		base.Release();
		WorldCursor.Instance.city.AddBuilding(endLoc, BuildingType.Homes);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public string myName;
	public string myClass;
	public string myLevel;
	public int[] stats;
	public Squad squad;

	private void Start()
	{
		string letters = "abcdefghijklmnopqrstuvwxyz";
		int length = Random.Range(3, 11);
		for (int i = 0; i < length; i++)
		{
			myName += letters[Random.Range(0, letters.Length)];
			myClass += letters[Random.Range(0, letters.Length)];
		}
		myLevel = Random.Range(0, 30).ToString();
	}
}

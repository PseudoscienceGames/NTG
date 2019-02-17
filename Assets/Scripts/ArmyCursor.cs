using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyCursor : MonoBehaviour
{
	public bool hasSelection = false;
	public Unit selection;
	public LayerMask ground;
	public LayerMask unit;

	private Vector3 lastPos;
	private Vector3 mouseDelta;
	private Camera cam;
	private float height;

	public GameObject unitInfo;
	public Text unitName;
	public Text unitClass;
	public Text unitLevel;

	public ArmyDisplay army;

	private void Start()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		mouseDelta = Input.mousePosition - lastPos;
		lastPos = Input.mousePosition;
		if (Input.GetMouseButtonDown(0) && !hasSelection)
		{
			RaycastHit hit;
			if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100, unit))
			{
				selection = hit.transform.GetComponent<Unit>();
				hasSelection = true;
				height = hit.point.y;

				unitInfo.SetActive(true);
				unitName.text = selection.myName;
				unitClass.text = selection.myClass;
				unitLevel.text = selection.myLevel;
			}
		}
		if(Input.GetMouseButton(0) && hasSelection)
		{
			RaycastHit hit;
			if (Physics.Raycast(cam.ScreenPointToRay(cam.WorldToScreenPoint(selection.transform.position) + mouseDelta), out hit, 100, ground))
			{
				selection.transform.position = hit.point;
			}
		}
		if(Input.GetMouseButtonUp(0) && hasSelection)
		{
			army.ReleaseUnit(selection.GetComponent<Unit>());
			selection = null;
			hasSelection = false;
			unitInfo.SetActive(false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
	public float speed;
	public float amt;
    // Update is called once per frame
    void Update()
    {
		Vector3 loc = Quaternion.Euler(0, Time.time * speed, 0) * Vector3.forward * amt;
		transform.position = new Vector3(loc.x, .2f, loc.z);
    }
}

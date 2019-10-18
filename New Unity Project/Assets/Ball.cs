using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public sphereColors colors;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Sphere") 
		{
			if (colors == other.gameObject.GetComponent<Sphere> ().sColor) {
				other.gameObject.GetComponent<Sphere> ().destroy = true;
			} else 
			{
				Destroy (other.gameObject);
			}

		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

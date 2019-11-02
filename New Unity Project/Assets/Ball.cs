using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public sphereColors colors;
	public Board board;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Sphere") 
		{
			if (colors == other.gameObject.GetComponent<Sphere> ().sColor) {
				other.gameObject.GetComponent<Sphere> ().destroy = true;
			} else 
			{
				other.gameObject.GetComponent<Sphere>().DestroySphere(other.gameObject);
			}
			//破壊した後空いたところの確認
			board.GetComponent<Board> ().checkMoveable ();
		}
	}
	// Use this for initialization
	void Start () {
		GameObject plane = GameObject.Find ("Plane");
		board = plane.GetComponent<Board> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

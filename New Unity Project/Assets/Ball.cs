using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public sphereColors colors;
	public Board board;
	public bool touch = false;
	public bool shoot = false;
	public Vector3 moveTarget;
	public float speed = 20.0f;

	public void moveToward(Vector3 pos)
	{
		moveTarget = (pos- transform.position).normalized;
		shoot = true;	
		Debug.Log ("shoot");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Sphere") 
		{
			if (!touch) 
			{
				if (colors == other.gameObject.GetComponent<Sphere> ().sColor) {
					other.gameObject.GetComponent<Sphere> ().destroy = true;
				} else 
				{
					other.gameObject.GetComponent<Sphere>().DestroySphere(other.gameObject);
				}
				//破壊した後空いたところの確認
				board.GetComponent<Board> ().checkMoveable ();
				touch = true;

			}
		}
	}
	// Use this for initialization
	void Start () {
		GameObject plane = GameObject.Find ("Plane");
		board = plane.GetComponent<Board> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shoot) 
		{
			//transform.position = Vector3.MoveTowards (transform.position, moveTarget, Time.deltaTime * speed);
			transform.position += moveTarget *speed * Time.deltaTime;
		}
		
	}
}

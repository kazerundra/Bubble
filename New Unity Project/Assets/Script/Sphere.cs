using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {
	public sphereColors sColor;
	public sphereColors currentColor;
	public List<GameObject> sameColor;
	public Material sMaterial;
	public bool destroy=false;
	public bool destroyCheck = false;
	public bool moving = false;
	public GameObject brd;
	public Board board;
	public GameObject sphereInfront;
	public Sprite Monkey, Rabbit, Chicken;
	public GameObject img;
	//同じ色範囲に入るとリストに入れて一緒に消すことできるように

	public void changeImages(){
		if (sColor == sphereColors.red) {
			img.GetComponent<SpriteRenderer> ().sprite = Rabbit;
		} else if(sColor == sphereColors.yellow) {
			img.GetComponent<SpriteRenderer> ().sprite = Monkey;
		} else if(sColor == sphereColors.blue) {
			img.GetComponent<SpriteRenderer> ().sprite = Chicken;
		} else {
			img.GetComponent<SpriteRenderer> ().sprite = Monkey;
		} 

	}

	private bool DistanceCheck(Transform a, Transform b)
	{		
		return Vector3.Distance (a.position, b.position) <= 1.03;
	}
	private void Rounding ()
	{
		Vector3 tmp = transform.localPosition;
		tmp.x = Mathf.Round (tmp.x);
		tmp.z = Mathf.Round (tmp.z);
		transform.localPosition = tmp;
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Sphere") {

			if (moving) {
				sameColor.Clear ();
				if ((other.gameObject.transform.localPosition.z - transform.localPosition.z) <= 0.2 && (other.gameObject.transform.localPosition.z - transform.localPosition.z) >= -0.2) {
					if (other.gameObject.transform.localPosition.x > transform.localPosition.x) {
						if (DistanceCheck (transform, other.transform)) {
							moving = false;		
							sphereInfront = other.gameObject;
							Rounding();
						} 
					} 
				}
			} else {
				if (!sameColor.Contains (other.gameObject) && other.gameObject.GetComponent<Sphere>().sColor == sColor) {
					if (DistanceCheck (other.transform, transform)) {
						if (other.transform.localPosition.z == transform.localPosition.z || other.transform.localPosition.x == transform.localPosition.x) {
							sameColor.Add (other.gameObject);
						}
					}
				}
			}
			
		}else if(other.tag == "Wall")
		{
            if (moving) {

				if (other.gameObject.transform.localPosition.x <= transform.localPosition.x) 
				{
                    
					moving = false;
					Rounding ();
				}
			}
		}

	}
    private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Sphere") 
		{
			if (moving) {
				if (other.gameObject.transform.localPosition.z == transform.localPosition.z) 
				{
					
				}
			} else 
			{
				if (sColor == other.gameObject.GetComponent<Sphere> ().sColor) 
				{
					if (other.gameObject.GetComponent<Sphere> ().moving == false) 
					{
						if (DistanceCheck(transform,other.gameObject.transform)) 
						{
							//sameColor.Add (other.gameObject);
						}
					}
				}
			}
		} 	}
	//右に移動他のスフィアとぶつかるまで
	public void FillEmpty(){
		moving = true;
	}
	//GAMEOBJECTのマテリアルを変更
	public void Initialize(sphereColors clr)
	{
		switch (clr) 
		{
		case sphereColors.blue:
			sColor = sphereColors.blue;
			sMaterial = GetComponentInParent<Board> ().blue;
			break;
		case sphereColors.red:
			sColor = sphereColors.red;
			sMaterial = GetComponentInParent<Board> ().red;
			break;
		case sphereColors.green:
			sColor = sphereColors.green;
			sMaterial = GetComponentInParent<Board> ().green;
			break;
		case sphereColors.yellow:
			sColor = sphereColors.yellow;
			sMaterial = GetComponentInParent<Board> ().yellow;
			break;
		default:
			sColor = sphereColors.red;
			sMaterial = GetComponentInParent<Board> ().red;
			break;			
		}
		GetComponent<MeshRenderer> ().material = sMaterial;
		changeImages ();
	}


	// Use this for initialization
	void Start () {
		sMaterial = GetComponent<MeshRenderer>().material;
		currentColor = sColor;
		brd=GameObject.Find ("Plane");
		board = brd.GetComponent<Board> ();

	}
	//リストに入れる
	public void InsertAllToList(GameObject target)
	{
		bool retry;
		restart:
		retry = false;
		if (sameColor.Count != 0) {
			foreach (GameObject x in target.GetComponent<Sphere>().sameColor) 
			{
				if (x != null) 
				{
					x.GetComponent<Sphere> ().destroy = true;
					if (!sameColor.Contains (x)&& x != gameObject) 
					{
						sameColor.Add (x);
						retry = true;
					}
				}
			
			}
		}
	
		if (retry) {
			goto restart;
		}
	}
	//同じ色全部消す
	public void DestroyAll()
	{
		for (int i = 0; i < sameColor.Count; i++) 
		{
			InsertAllToList( sameColor [i]);
		}
		// アニメーションここ
		foreach (GameObject x in sameColor) 
		{
			DestroySphere (x);
		}

		DestroySphere (gameObject);
	}
	public void DestroySphere(GameObject x)
	{
		board.sphereList.Remove (x);
		board.destroyNumber += 1;
//		Debug.Log (board.destroyNumber);
		Destroy (x);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (currentColor != sColor) {
			Initialize (sColor);
			currentColor = sColor;
		}
		if (destroy  && !destroyCheck) {
			destroyCheck = true;
			DestroyAll ();
		}
        if (sphereInfront == null)
        { moving = true; }
        if (gameObject.transform.localPosition.x == board.GetComponent<Board>().wall.transform.localPosition.x)
        {
            sphereInfront = board.GetComponent<Board>().wall;
        }
		if (moving) 
		{
			transform.Translate (Vector3.right *( Time.deltaTime * 2));
			if (!board.GetComponent<Board> ().canShoot) {
				board.GetComponent<Board> ().timer= 0f;
			}
		}
	}
}

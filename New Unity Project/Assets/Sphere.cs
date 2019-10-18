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
	//同じ色範囲に入るとリストに入れて一緒に消すことできるように

	private bool DistanceCheck(Transform a, Transform b)
	{
		
		return Vector3.Distance (a.position, b.position) <= 1.1;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Sphere") 
		{
			if (sColor == other.gameObject.GetComponent<Sphere> ().sColor) 
			{
				if (DistanceCheck(transform,other.gameObject.transform)) 
				{
					sameColor.Add (other.gameObject);
				}
			}

		}
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
		
	}


	// Use this for initialization
	void Start () {
		sMaterial = GetComponent<MeshRenderer>().material;
		currentColor = sColor;

	}
	//リストに入れる
	public void InsertAllToList(GameObject target)
	{
		bool retry;
		restart:
		retry = false;
		foreach (GameObject x in target.GetComponent<Sphere>().sameColor) 
		{
			x.GetComponent<Sphere> ().destroy = true;
			if (!sameColor.Contains (x)&& x != gameObject) 
			{
				sameColor.Add (x);
				retry = true;
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
			Destroy (x);
		}

		Destroy (gameObject);
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
	}
}

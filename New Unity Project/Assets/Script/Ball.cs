﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public sphereColors colors;
	public Board board;
	public bool touch = false;
	public bool shoot = false;
	public Vector3 moveTarget;
	public Material sMaterial;
	public float speed = 20.0f;
	public GameObject img;
    public Sprite Banana, Chocolate, Corn, Carrot;
	/// <summary>
	/// Changes the images. red= carrot, yellow banana, blue corn
	/// </summary>
	public void changeImages(){
		if (colors == sphereColors.red) {
			img.GetComponent<SpriteRenderer> ().sprite = Carrot;
		} else if(colors == sphereColors.yellow) {
			img.GetComponent<SpriteRenderer> ().sprite = Banana;
		} else if(colors == sphereColors.blue) {
			img.GetComponent<SpriteRenderer> ().sprite = Corn;
        } else if (colors == sphereColors.chocolate)
        {
            img.GetComponent<SpriteRenderer>().sprite = Chocolate;
        }
        else
        {
			img.GetComponent<SpriteRenderer> ().sprite = Banana;
		} 

	}

	/// <summary>
	/// 方向に飛ぶ
	/// </summary>
	/// <param name="pos">Position.</param>
	public void moveToward(Vector3 pos)
	{
		moveTarget = (pos- transform.position).normalized;
		shoot = true;	
		Debug.Log ("shoot");
	}
	/// <summary>
	/// 色をランダムにする
	/// </summary>
	public void randomColor(){
		int random = Random.Range (0, 4);
        changeColor(random);
	}

    /// <summary>
    /// カラーチェンジ
    /// </summary>
    /// <param name="number">0.carrot,1.corn,2.Banana,</param>
    public void changeColor(int number)
    {
        if (number == 0)
        {
            colors = sphereColors.red;
            sMaterial = GetComponentInParent<Board>().red;
        }
        else if (number == 1)
        {
            colors = sphereColors.blue;
            sMaterial = GetComponentInParent<Board>().blue;
        }
        else if (number == 2)
        {
            colors = sphereColors.yellow;
            sMaterial = GetComponentInParent<Board>().yellow;
        }
        else
        {
            colors = sphereColors.chocolate;
            sMaterial = GetComponentInParent<Board>().yellow;
        }
        GetComponent<MeshRenderer>().material = sMaterial;
        changeImages();
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
				Destroy (gameObject);

			}
        }
        if (other.tag == "TrashBin") 
        {
            if (!touch)
            {
                touch = true;
                if (colors != sphereColors.chocolate)
                {
                    board.TrashScore(false);
                }
                else
                {
                    board.TrashScore(true);
                }
                Destroy(gameObject);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum sphereColors {red,blue,yellow,green,none};

public class Board : MonoBehaviour {
	
	public GameObject spherePrefab;
	public Material red;
	public Material blue;
	public Material green;
	public Material yellow;
	public bool throwingBall =false;
	public sphereColors [,] boardColor;
	public int maxCollumn=5;
	public int maxRow= 10;
	public int boardFill =6;
	public int score = 0;
	public int randomPercent = 50;
	public float time;

	// to do list
	// repair the destroy system make shoot


	public void setThrowing(bool condition)
	{
		throwingBall = condition;
	}
	//ボードを初期化
	public void initializeBoard(int row, int collumn, float percent)
	{
		boardColor = new sphereColors[row,collumn];
		randomBoard (percent);
		spawnStage ();
	}
	//ボードにSpawn
	public void spawnStage()
	{
		
		Vector3 spawnLocation = new Vector3 (-4, 0, 4);
		Vector3 startLocation = spawnLocation;
		for (int j = 0; j < boardFill; j++) 
		{
			spawnLocation.z = spawnLocation.z - 1;
			for(int i=0; i<maxCollumn;i++)
			{
				var go= Instantiate (spherePrefab) as GameObject;
				go.transform.parent = transform;
				go.GetComponent<Sphere> ().Initialize (boardColor[j,i]);
				go.transform.localPosition = spawnLocation;
				spawnLocation.x = spawnLocation.x +1;

			}
			spawnLocation.x = startLocation.x;
		}

	}
	//ランダムステージを作る
	public void randomBoard (float randomPercent )
	{
		int currentCollumn= 0;
		int currentRow=0;
		int rndm = Random.Range(1,4);

		for (int i = 0; i <= boardFill; ) 
		{
			switch (rndm) 
			{
			case 1:
				boardColor [currentRow, currentCollumn] = sphereColors.red;
				break;
			case 2:
				boardColor [currentRow, currentCollumn] = sphereColors.blue;
				break;
			case 3:
				boardColor [currentRow, currentCollumn] = sphereColors.yellow;
				break;
			}
			if (currentCollumn >= (maxCollumn-1)) {
				i++;
				currentRow += 1;
				currentCollumn = 0;
			} else {
				currentCollumn += 1;
			}
			// randomPercentによって違う色を作る
			if (Random.Range (0, 100) <= randomPercent) {
				int curentrandom = rndm;
				int newRandom =rndm;
				while (newRandom == curentrandom) 
				{
					newRandom = Random.Range (1, 4);
				}
				rndm = newRandom;

			}
		}
	}
		
	void Start () {
		initializeBoard (maxRow, maxCollumn ,randomPercent);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

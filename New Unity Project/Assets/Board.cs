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
	public int maxCollumn=10;
	public int maxRow= 8;
	public int boardFill =6;
	public int score = 0;
	public int randomPercent = 50;
	public float time;
	public List<GameObject> sphereList;

	// to do list
	// repair the destroy system make shoot

	// spawn一覧
	public void spawnOne(){

		Vector3 spawnLocation = new Vector3 (-4, 0, 4);
		Vector3 startLocation = spawnLocation;
		randomBoard (randomPercent);
		for(int i=0; i<maxRow;i++)
		{
			var go= Instantiate (spherePrefab) as GameObject;
			go.transform.parent = transform;
			go.transform.localPosition = spawnLocation;
			spawnLocation.z = spawnLocation.z -1;
			sphereList.Add (go);
			//	Debug.Log (sphereList.Count);
		}
	}
	public void fill(){
		Vector3 tempPos = new Vector3 (0, 0, 0);
		foreach (GameObject go in sphereList) 
		{
			tempPos = go.transform.localPosition;
			tempPos.x += 1;
			go.transform.localPosition = tempPos;
		}
	}

	//全スフィア一覧移動
	public void moveAll()
	{
		Vector3 tempPos = new Vector3 (0, 0, 0);
		foreach (GameObject x in sphereList) 
		{
			
			float temp = x.transform.localPosition.x;

			temp += 1;
			tempPos.x = temp;
			tempPos.z = x.transform.localPosition.z;
			Debug.Log ("x" + tempPos.x + "y" + tempPos.y + "z" + tempPos.z);
			x.transform.localPosition = tempPos;
		}
	}
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
		for (int j = 0; j <boardFill ; j++) 
		{
			spawnLocation.x = spawnLocation.x + 1;
			for(int i=0; i<maxRow;i++)
			{
				var go= Instantiate (spherePrefab) as GameObject;
				go.transform.parent = transform;
				go.GetComponent<Sphere> ().Initialize (boardColor[i,j]);
				go.transform.localPosition = spawnLocation;
				spawnLocation.z = spawnLocation.z -1;
				sphereList.Add (go);
			//	Debug.Log (sphereList.Count);
			}
			spawnLocation.z = startLocation.z;
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
			if (currentRow >= (maxRow-1)) {
				i++;
				currentCollumn += 1;
				currentRow= 0;
			} else {
				currentRow += 1;
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

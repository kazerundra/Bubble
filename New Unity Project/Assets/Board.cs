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
	public int destroyNumber= 0;
	public int currentWallPosition;
	public GameObject wall;
	public bool shooting= true;
	public GameObject currentBall;
	public GameObject nextBall;
	public GameObject ballSpawnLocation;
	public GameObject nextBallLocation;
	public bool startGame =false;
	public GameObject ballPrefab;
	private float timer = 0f;
	private float reinforcementTimer = 0f;
	public float reinforcement =10f;
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
			go.GetComponent<Sphere> ().Initialize (boardColor[i,0]);
			spawnLocation.z = spawnLocation.z -1;
			go.GetComponent<Sphere> ().FillEmpty ();
			sphereList.Add (go);

			//	Debug.Log (sphereList.Count);
		}
	}
	//transform local position をラウンドアップ
	public int checkDistance(GameObject go)
	{
		Vector3 temppos = go.transform.localPosition;
		return Mathf.RoundToInt (temppos.x);
	}
	public void checkMoveable(){
		StartCoroutine (checkMoveAll());
	}

	//COllumnに分けてスフィアを行ずつに動かす
	IEnumerator checkMoveAll()
	{

		List<GameObject> sphereList1 = new List<GameObject>();
		List<GameObject> sphereList2= new List<GameObject>();
		List<GameObject> sphereList3= new List<GameObject>();
		List<GameObject> sphereList4= new List<GameObject>();
		List<GameObject> sphereList5= new List<GameObject>();
		List<GameObject> sphereList6= new List<GameObject>();
		List<GameObject> sphereList7= new List<GameObject>();
		List<GameObject> sphereList8= new List<GameObject>();
		List<GameObject> sphereList9= new List<GameObject>();
		List<GameObject> sphereList10= new List<GameObject>();
		//距離によってリストに入れる
		foreach (GameObject go in sphereList) 
		{
			switch (checkDistance (go)) 
			{
			case -3:
				sphereList10.Add (go);
				break;
			case -2:
				sphereList9.Add (go);
				break;
			case -1:
				sphereList8.Add (go);
				break;
			case 0:
				sphereList7.Add (go);
				break;
			case 1:
				sphereList6.Add (go);
				break;
			case 2:
				sphereList5.Add (go);
				break;
			case 3:
				sphereList4.Add (go);
				break;
			case 4:
				sphereList3.Add (go);
				break;
			case 5:
				sphereList2.Add (go);
				break;
			case 6:
				sphereList1.Add (go);
				break;
			default:
				sphereList10.Add (go);
				break;
			}
		}
		List<GameObject> tempList;
		//動かす
		for (int i = 0; i <= 10; i++) 
		{
			if (i == 0) {
				tempList = sphereList1;
			} else if (i == 1) 
			{
				tempList = sphereList2;
			}else if (i == 2) 
			{
				tempList = sphereList3;
			}else if (i == 3) 
			{
				tempList = sphereList4;
			}else if (i == 4) 
			{
				tempList = sphereList5;
			}else if (i == 5) 
			{
				tempList = sphereList6;
			}else if (i == 6) 
			{
				tempList = sphereList7;
			}else if (i == 7) 
			{
				tempList = sphereList8;
			}else if (i == 8) 
			{
				tempList = sphereList9;
			}else  
			{
				tempList = sphereList10;
			}

			foreach (GameObject go in tempList) 
			{
				if (go != null) {
					go.GetComponent<Sphere> ().moving = true;
				}
			}
			yield return new WaitForSeconds(0.05f);
			
		} 
	}
	//壁の位置を設置
	public void setWall(float position)
	{
		Vector3 tempPos = wall.transform.localPosition;
		tempPos.x = position-4;
		wall.transform.localPosition = tempPos;
	}
	//全スフィアを右に動かす
	public void fill(){
		Vector3 tempPos = new Vector3 (0, 0, 0);
		foreach (GameObject go in sphereList) 
		{
			tempPos = go.transform.localPosition;
			tempPos.x += 1;
			go.transform.localPosition = tempPos;
		}

		currentWallPosition += 1;
		setWall (currentWallPosition);
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
	public void spawnBall()
	{
		if (!startGame) {
			GameObject go = Instantiate (ballPrefab) as GameObject;
			go.transform.position = ballSpawnLocation.transform.position;
			startGame = true;
			go.transform.parent = transform;
			currentBall = go;
			currentBall.GetComponent<Ball> ().randomColor ();
		} else {
			currentBall = nextBall;
			currentBall.transform.position = ballSpawnLocation.transform.position;
		}
		GameObject go1 = Instantiate (ballPrefab) as GameObject;
		go1.transform.position = nextBallLocation.transform.position;
		go1.transform.parent = transform;
		nextBall = go1;
		nextBall.GetComponent<Ball> ().randomColor ();
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
		setWall (boardFill);
		currentWallPosition = boardFill;

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
		spawnBall ();
		
	}
	
	// Update is called once per frame
	void Update () {
		reinforcementTimer += Time.deltaTime;
		if (reinforcementTimer >= reinforcement) {
			reinforcementTimer = 0;
			spawnOne ();
		}
		if (shooting) {
			timer = 0f;
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) {
					Vector3 x = hit.point;
					x.z = 0;
					currentBall.GetComponent<Ball> ().moveToward (x);
				}
				shooting = false;
			}
			if (Input.GetKeyUp ("space")) {
				spawnBall ();
			}
		} else {
			
			timer += Time.deltaTime;
			if (timer >= 2) {
				shooting = true;
				spawnBall ();
			}

		}
		
	}
}

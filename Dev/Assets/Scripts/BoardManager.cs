﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 

public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count {
		public int minimum; 			//Minimum value for our Count class.
		public int maximum; 			//Maximum value for our Count class.

		public Count(int min, int max)	{
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 15; 										//Number of columns in our game board.
	public int rows = 15;											//Number of rows in our game board.
	//public Count wallCount = new Count(9, 18);						//Lower and upper limit for our random number of walls per level.
	//public GameObject exit;											//Prefab to spawn for exit.
	public GameObject[] floorTiles;									//Array of floor prefabs.
	public GameObject[] doorTiles;									//Array of food prefabs.
	public GameObject[] enemyTiles;									//Array of enemy prefabs.
	public GameObject[] outerWallTiles;								//Array of outer tile prefabs.

	private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3>();	//A list of possible locations to place tiles.

	void InitialiseList(){
		gridPositions.Clear();
		for(int x = 1; x < columns-1; x++){
			for(int y = 1; y < rows-1; y++)	{
				gridPositions.Add(new Vector3(x, y, 0f));
			}
		}
	}

	void BoardSetup(){
		boardHolder = new GameObject ("Board").transform;
		for(int x = -1; x < columns + 1; x++){
			for(int y = -1; y < rows + 1; y++){
				GameObject toInstantiate = floorTiles[Random.Range(0,floorTiles.Length)];
				if(x == -1 || x == columns || y == -1 || y == rows)
					if((x == (rows/2) && (y == -1 || y == rows) || y == (rows/2) && (x == -1 || x == columns)))
						toInstantiate = doorTiles[Random.Range(0, doorTiles.Length)];
					else
						toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
				GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}
	}

	Vector3 RandomPosition(){
		int randomIndex = Random.Range(0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex);
		return randomPosition;
	}

	void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum){
		int objectCount = Random.Range(minimum, maximum+1);
		for(int i = 0; i < objectCount; i++){
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
			Instantiate(tileChoice, randomPosition, Quaternion.identity);
		}
	}

	public void SetupScene (int level){
		BoardSetup ();
		InitialiseList ();
		//LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
		//LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
		int enemyCount = (int)Mathf.Log(level+2, 2f);
		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
		//Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Global_tracker : MonoBehaviour {
	public bool exit_path = false;
    public bool makeKey = false;

	public int num_rooms = 9;
	public int columns = 15;
	public int rows = 9;

	public GameObject[] l_floor;
	public GameObject[] l_door;
	public GameObject[] l_wall;

	public GameObject[] m_floor;
	public GameObject[] m_door;
	public GameObject[] m_wall;

	public GameObject[] h_floor;
	public GameObject[] h_door;
	public GameObject[] h_wall;

	public GameObject[] room_array;
    public GameObject prefabKey;
	public int room_counter = 0;
	public int YeOldCurrentX = 0;
	public int YeOldCurrentY = 0;
    private Transform boardHolder;
    public GameObject startRoom;
    public GameObject exitRoom;
	public GameObject raycaster;

    public char Rez;

    public float firstFrame;

    public int counter;

    public void BuildTileMap()
    {
        room_array = new GameObject[num_rooms];
        //while (exit_path != true)
        //{
        //counter++;
        int resSelect = Random.Range(0, 3);
        for (int i = 0; i < num_rooms; i++)
        {
            Generate_Board(resSelect);
        }

        room_counter = 0;
        YeOldCurrentX = 0;
        YeOldCurrentY = 0;

        startRoom = room_array[Random.Range(0, num_rooms)];
        startRoom.transform.name = "startRoom";
        //Debug.Log("CALLING THE FUNCTION");

        exitRoom = room_array[Random.Range(0, num_rooms)];
        exitRoom.transform.name = "exitRoom";
    }

	void Start () {
        BuildTileMap();
        room_counter = 0;
        YeOldCurrentX = 0;
        YeOldCurrentY = 0;
    }

	void FixedUpdate () {

    }
		
	void InstantiateRooms(GameObject[] floorRes, GameObject[] doorRes, GameObject[] wallRes){
        GameObject toInstantiate = null;
        for (int x = -1; x < columns + 1; x++){
			for (int y = -1; y < rows + 1; y++) {
                if (floorRes.Length != 0 || doorRes.Length != 0 || wallRes.Length != 0) {
                    toInstantiate = floorRes[Random.Range(0, floorRes.Length)];
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        if ((y == (rows / 2) && !(x == -1 && YeOldCurrentX == 0) && !(YeOldCurrentX == (Mathf.Sqrt(num_rooms) - 1) && x == columns)) ||
                            (x == (columns / 2) && !(y == -1 && YeOldCurrentY == 0) && !(YeOldCurrentY == (Mathf.Sqrt(num_rooms) - 1) && y == rows)))
                        {
                            int rando = Random.Range(0, 100);
                            if (rando <= 85)
                                toInstantiate = doorRes[Random.Range(0, doorRes.Length)];
                            else
                                toInstantiate = wallRes[Random.Range(0, wallRes.Length)];
                        }
                        else
                            toInstantiate = wallRes[Random.Range(0, wallRes.Length)];
                    }
                }
				//else
				//	toInstantiate = wallRes[Random.Range(0, wallRes.Length)];
				GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                
                makeKey = false;
                instance.transform.SetParent(boardHolder);
			}
		}
	}

	void SelectRes(int rando){
		switch(rando){
			case 0:
				InstantiateRooms(l_floor,l_door,l_wall);
                Rez = 'L';
				break;
			case 1:
                InstantiateRooms(m_floor,m_door,m_wall);
                Rez = 'M';
                break;
			case 2:
                InstantiateRooms(h_floor,h_door,h_wall);
                Rez = 'H';
                break;
			default:
				Debug.Log ("None");
				break;
		}
	}

	void Generate_Board(int resSelect){
        //if(room_counter >= 9)
        //{
        //   room_counter = 0;
        //}
        makeKey = true;
        boardHolder = new GameObject("Room").transform;
        boardHolder.tag = "Room";
        boardHolder.gameObject.transform.position = new Vector3(columns / 2, rows / 2, 0);
        boardHolder.gameObject.AddComponent<BoxCollider>();
        boardHolder.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        boardHolder.gameObject.GetComponent<BoxCollider>().size = new Vector3(columns + 1, rows + 1, 1);

        boardHolder.gameObject.AddComponent<roomCollider>();
        boardHolder.gameObject.GetComponent<roomCollider>().thisSucks = .14f;
        boardHolder.gameObject.GetComponent<roomCollider>().characterTransSpeedX = .0015f;
        boardHolder.gameObject.GetComponent<roomCollider>().characterTransSpeedY = .003f;
        boardHolder.gameObject.GetComponent<roomCollider>().thereAreEnemiesHere = true;
        boardHolder.gameObject.GetComponent<roomCollider>().thereAreTrapsHere = true;

        if (makeKey == true)
        {
            int keyChance = Random.Range(0, 3);
            if (keyChance == 2)
            {
                Vector3 newPos = new Vector3(boardHolder.transform.position.x +(YeOldCurrentX * (columns + 2)) + Random.insideUnitCircle.x * 5, boardHolder.transform.position.y + (YeOldCurrentY * (rows + 2)) + Random.insideUnitCircle.y * 5f, boardHolder.transform.position.z - .1f);
                Instantiate(prefabKey, newPos, transform.rotation);
            }
            makeKey = false;
        }

        boardHolder.gameObject.layer = LayerMask.GetMask("TransparentFX");
        //Vector3 tempPosition = new Vector3(boardHolder.transform.position.x + 4f, boardHolder.transform.position.y + 6.5f, boardHolder.transform.position.z);
        GameObject temp = Instantiate(GameObject.Find ("Xexit"), boardHolder.transform.position, boardHolder.transform.rotation, boardHolder.transform);
		SelectRes(resSelect);

		boardHolder.transform.position = new Vector3(boardHolder.transform.position.x + (YeOldCurrentX * (columns + 2)), boardHolder.transform.position.y + (YeOldCurrentY * (rows + 2)), 0);
		YeOldCurrentX++;
		if (YeOldCurrentX == Mathf.Sqrt(num_rooms)){
			YeOldCurrentX = 0;
			YeOldCurrentY++;
		}
        Debug.Log(room_counter);
		room_array[room_counter] = boardHolder.gameObject;
        room_counter++;
        /*if (room_counter >= 9)
        {
          room_counter = 0;
        }*/
        //this makes the raycaster
        Instantiate(raycaster, boardHolder.transform.position, raycaster.transform.rotation, boardHolder.transform);
	}
}

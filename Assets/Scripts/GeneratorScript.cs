using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour {

    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;
    public GameObject[] availableItems;
    public List<GameObject> currentItems;
    public GameObject[] availableCoins;
    public List<GameObject> currentCoins;

    public float screenWidthInPoints;

	void Start () {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
	}
	
    void FixedUpdate()
    {
        GenerateRoomIfRequired();
    }

    void AddRoom(float farhtestRoomEndX)
    {
        int randomRoomIndex = Random.Range(0, availableRooms.Length);
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);
        float roomWidth = room.transform.Find("floor").localScale.x;
        float roomCenter = farhtestRoomEndX + roomWidth * 0.5f;
        room.transform.position =   new Vector3(roomCenter, 0, 0);
        currentRooms.Add(room);

    }

    void GenerateRoomIfRequired()
    {
        List<GameObject> roomsToRemove = new List<GameObject>();
        List<GameObject> itemsToRemove = new List<GameObject>();
        List<GameObject> coinsToRemove = new List<GameObject>();
        bool addRooms = true;
        float playerX = transform.position.x;
        float removeRoomX = playerX - screenWidthInPoints;
        float addRoomX = playerX + screenWidthInPoints;
        float farhtestRoomEndX = 0;
        float roomStartX=0;
        float roomWidth=0;
        foreach(var room in currentRooms)
        {
            roomWidth = room.transform.Find("floor").localScale.x;
            roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;
            if (roomStartX > addRoomX)
            {
                addRooms = false;
            }
            if (roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }
            farhtestRoomEndX = Mathf.Max(farhtestRoomEndX, roomEndX);
        }
        foreach(var item in currentItems)
        {
            float itemEndX = item.transform.position.x;
            if (itemEndX < playerX-5)
            {
                itemsToRemove.Add(item);
            }
            
        }
        foreach(var coin in currentCoins)
        {
            float coinEndX = coin.transform.position.x;
            if(coinEndX < playerX - 5)
            {
                coinsToRemove.Add(coin);
            }
        }
        foreach(var coin in coinsToRemove)
        {
            currentCoins.Remove(coin);
            Destroy(coin);
        }
        foreach (var item in itemsToRemove)
        {
            currentItems.Remove(item);
            Destroy(item);
        }
        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }
        if (addRooms)
        {
            AddRoom(farhtestRoomEndX);
            for(int i=0; i<Random.Range(0,3); i++)
            {
                AddItem(farhtestRoomEndX + Random.Range(0f, roomWidth));
            }
            for(int i=0; i < Random.Range(0, 2); i++)
            {
                AddPackOfCoins(farhtestRoomEndX + Random.Range(0f, roomWidth));
            }
            
        }
    }
    

    void AddItem(float xPos)
    {
        int randomItemIndex = Random.Range(0, availableItems.Length);
        GameObject item = (GameObject)Instantiate(availableItems[randomItemIndex]);
        item.transform.position = new Vector3(xPos, Random.Range(-2, 2), 0);
        currentItems.Add(item);
    }

    void AddPackOfCoins(float xPos)
    {
        int randomItemIndex = Random.Range(0, availableCoins.Length);
        GameObject coin = (GameObject)Instantiate(availableCoins[randomItemIndex]);
        coin.transform.position = new Vector3(xPos, Random.Range(-2.5f, 3f), 0);
        currentCoins.Add(coin);
    }

}

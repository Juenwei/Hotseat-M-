using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnRoom : MonoBehaviour
{
    [Header("Room Type")]
    [SerializeField] private List<GameObject> basicRoomPrefab;
    [SerializeField] private GameObject startRoomPrefab, endRoomPrefab;
    [SerializeField] private List<GameObject> cornerUnitPrefabsList;

    [Header("Room setup")]
    private GameObject roomToBeSpawn;
    [SerializeField] private int maxRoomAmount, maxCornerUnitAmount;
    [SerializeField] private List<GameObject> roomList = new List<GameObject>();
    private GameObject previousRoom, previousCorridor;
    private GameObject NextRoomType;
    private int currentCornerUnitAmount = 0;


    [Header("Corridor Setup")]
    private Vector3 corridorSpawnPoint;
    [SerializeField] private List<GameObject> corridorPrefab;
    //[SerializeField] private List<GameObject> startPointList, endPointList;
    [SerializeField] private int pointNum;
    private Transform tempNextRoomPoint;


    [Header("Position setup")]
    [SerializeField] private Transform generatePoint;
    //[SerializeField] private float xOffset, yOffset, minZOffset, maxZOffset;
    //[SerializeField] private float minThreshold, maxThreshold;


    [Header("Rotation setup")]
    private Quaternion roomQuaternion = Quaternion.identity;
    private bool isCornerUnit = false;
    [SerializeField]private int[] AngleRange = { -80, -60, -40, -20, -10, 10, 20, 40, 60, 80 };
    private int minReading, maxReading, indexAdder;

    [Header("Level Difficulty setup")]
    private int[] roomLimit=new int[3];
    private int[] currentRoomAmount = new int[3];


    [Header("Element 0 for easy , Element 1 for normal , Element 2 for hard")]
    [SerializeField] private int hardRoom, easyRoom;
    [SerializeField] private int[] difficultSetup;
    
    private Transform startRoomCheckPoint;


    void Awake()
    {
        //Intiallized
        difficultSetup = GameManager.instance.GetDifficlutlySetup();
        SetRoomDificulty(difficultSetup);
        RoomDifficultyLimitSetup();
        SetReadingRange();
        //Generate Start point
        for (int i = 0; i < maxRoomAmount; i++)
        {
            //1) Generate room //Change to generate room only
            previousRoom = GenerateRoom(i);
            //1.5) Change the rotation of generate point
            ChangeGeneratePointRotation(previousRoom.GetComponent<PrefabRoomSetupInfo>().GetEndTransform());
            //2) Get the end point position and set the value as generate point posistion(Set Corridor generate posisition)
            ChangeGeneratePosition(previousRoom.GetComponent<PrefabRoomSetupInfo>().GetEndTransform());
            //3) Generate Corridor
            previousCorridor = GenerateCorridorType();
            //4) Get the end point position from corridor and set the value as new generate point posistion(Set Room generate posisition)
            ChangeGeneratePosition(previousCorridor.GetComponent<PrefabRoomSetupInfo>().GetEndTransform());
            //5) Change Direction and set the type of room
            SetRoomType();

            #region Old Method
            //1) Generate room(1)
            //rooms.Add(Instantiate(roomPrefab[Random.Range(0, roomPrefab.Count)], generatePoint.position, Quaternion.identity));

            //2) Move spawnpoint to the posistion based on the offset (For Generate corridor)
            //ChangeGeneratePosition(CalculateOffsetBetweenRoom(rooms[i]));

            //3) Generate a anchor point (Turning point) for corridor
            //corridorContainerObject = Instantiate(CorridorPrefabs, generatePoint.position, Quaternion.identity);

            //4) Random rotation ,Random direction
            //ChangeGenerateDirection();

            //5) Move spawnpoint to the posistion based on the offset and follow the direction from random rotation
            //ChangeGeneratePosition(Random.Range(minZOffset, maxZOffset));
            //tempNextRoomPoint = generatePoint.transform;

            //Generate Corridor //havent decide the condition statements
            //corridorContainerObject.GetComponent<GenerateCorridor>().SpawnJumpCorridor(endPointList[i].transform, tempNextRoomPoint);
            //corridorContainerObject.GetComponent<GenerateCorridor>().SpawnPlatformCorridor(endPointList[i].transform, tempNextRoomPoint);
            #endregion




        }
        //Need to be in runtime
        roomList.Add(Instantiate(endRoomPrefab, generatePoint.position, Quaternion.identity));

        startRoomCheckPoint = roomList[0].GetComponent<PrefabRoomSetupInfo>().GetCheckPointTransform();
        //Debug.Log("Start room Check point posisition : " + startRoomCheckPoint.transform.position);
        
    }

    private void Start()
    {
        GameManager.instance.UpdateSpawnPoint(startRoomCheckPoint);
        GameManager.instance.SpawnPlayer();
    }

    public void ChangeGeneratePosition(Transform lastEndpointPosition)
    {
        generatePoint.position = lastEndpointPosition.transform.position;
    }
  
    
    private void SetReadingRange()
    {
         //[SerializeField] private int[] AngleRange = { -80, -60, -40, -20, -10, 10, 20, 40, 60, 80 };
        int readSequence = Random.Range(0, 2);
        if(readSequence==0)
        {
            //Read from negative/left hand side to right handside
            minReading = 0;
            maxReading = (AngleRange.Length / 2) - 1;//4
            indexAdder = 1;
        }
        else
        {
            //Read from positive/right hand side to left handside
            minReading = (AngleRange.Length / 2) - 1;
            maxReading = AngleRange.Length-1;
            indexAdder = -1;
        }
        
    }
        
    private void SetRoomType()
    {
        int randomRoomType = Random.Range(1, 3);
        int randomCornerUnit = Random.Range(0, cornerUnitPrefabsList.Count);
        int randomIndex;
        //Set room type
        if (randomRoomType ==1 && currentCornerUnitAmount < maxCornerUnitAmount)
        {
            //Random different corner unit
            //NextRoomType = cornerUnitPrefabsList[randomCornerUnit];
            NextRoomType = SelectDifficultyRoom(cornerUnitPrefabsList);
            currentCornerUnitAmount++;
            //Set Quartenion
            roomQuaternion = Quaternion.identity;
            isCornerUnit = true;
        }
        else
        {
            //Do random generate rotation
            randomIndex = Random.Range(minReading, maxReading + 1);
            //Manipulate range
            //if (minReading >= 0 && maxReading < AngleRange.Length)

            ChangeGeneratePointRotation(AngleRange[randomIndex]);
            //Debug.Log("Angle Calling : " + AngleRange[randomIndex] + "  Range : From " + minReading + " to " + maxReading);

            //Reading next move
            if (minReading + indexAdder < 0)
            {
                //Debug.Log("Min Index exceed change from " + indexAdder + "  to " + (-indexAdder));
                indexAdder *= -1;
            }

            if (maxReading + indexAdder >= AngleRange.Length)
            {
                //Debug.Log("Max Index exceed change from " + indexAdder + "  to " + (-indexAdder));
                indexAdder *= -1;
            }

            minReading += indexAdder;
            maxReading += indexAdder;
            //Debug.Log("New Reading range from " + minReading + "  to " + maxReading);

            //Basic unit //Here do the difficulty check
            int basicRoomIndex = Random.Range(0, basicRoomPrefab.Count);

            NextRoomType = SelectDifficultyRoom(basicRoomPrefab);
            //NextRoomType = basicRoomPrefab[basicRoomIndex];
            //Set Quartenion
            roomQuaternion = generatePoint.localRotation;
            isCornerUnit = false;

        }
    }

    //Later Change the function type to GameObject type // and change the parameter
    private GameObject SelectDifficultyRoom(List<GameObject> roomList)
    {
        GameObject selectedRoom;

        int RandomDifficultyIndex, currentRoomDifficultIndex;
        bool canBeGenerate=false;
        int selectedIndex = 0;
        List<GameObject> temporRoomList = new List<GameObject>();
        //Pick Difficulty index

        //Control the amount and the limit of each difficulty room
        do
        {
            //Random generate a number
            RandomDifficultyIndex = Random.Range(0, 3);//roll up 0,1,2
            //Debug.Log("Random Difficulty index before : " + RandomDifficultyIndex);
            //If the current room amount still within the limit
            int currentAmount = currentRoomAmount[RandomDifficultyIndex];
            int roomLimitAmount = roomLimit[RandomDifficultyIndex];
            if (currentAmount < roomLimitAmount)
            {
                //Pass
                //Debug.Log("Room still within limit , Room index  : " + RandomDifficultyIndex);
                canBeGenerate = true;
                selectedIndex = RandomDifficultyIndex;

                //Add the amount for current room amount
                currentRoomAmount[RandomDifficultyIndex] += 1;
            }
            else
            {
                //Re-run
                //Debug.Log("Room out of limit , Room index  : " + RandomDifficultyIndex);
                canBeGenerate = false;
            }

            //Check Again
            //currentAmount = currentRoomAmount[RandomDifficultyIndex];
        } while (!canBeGenerate);

        int recordMatch=0;
        //Read all room type from the list
        for (int i = 0; i < roomList.Count; i++)
        {
            //Do get compenent for comparing
            currentRoomDifficultIndex = roomList[i].GetComponent<PrefabRoomSetupInfo>().GetRoomDifficulty();
            if (currentRoomDifficultIndex == selectedIndex)
            {
                //If difficulty match add it to the tempor List
                recordMatch++;
                temporRoomList.Add(roomList[i]);
            }

        }
        //Debug.Log("Tempor List count : " + temporRoomList.Count);
        selectedRoom = temporRoomList[Random.Range(0, temporRoomList.Count)];
        temporRoomList.Clear();
        return selectedRoom;
    }

    private GameObject GenerateRoom(float sequence)
    {
        GameObject roomBeGenerated,roomReference;
        
        //First room
        if(sequence==0)
        {
            roomBeGenerated = startRoomPrefab;

        }
        //Rest of room
        else
        {
            //Here select the type of room 
            
            //GetRoomType
            roomBeGenerated = NextRoomType;
        }
        //if is corner , dont care about rotation , else if basic room , change the quaternion to generatePoint rotation
        roomReference = Instantiate(roomBeGenerated, generatePoint.position, roomQuaternion);
        roomList.Add(roomReference);

        return roomReference;
    }

    // Must be call after set room type method and after generate room method
    private void ChangeGeneratePointRotation(Transform lastRoomEndPoint)
    {
        if(isCornerUnit)
        {
            
            //Debug.Log("Generate point Rotation : " + lastRoomEndPoint.transform.localEulerAngles);
            generatePoint.transform.eulerAngles = lastRoomEndPoint.transform.localEulerAngles;
            isCornerUnit = false;

        }
    }

    private void ChangeGeneratePointRotation(float angleChange)
    {
        
        generatePoint.transform.eulerAngles = new Vector3(0, angleChange, 0);
        
    }

    private GameObject GenerateCorridorType()
    {
        GameObject corridorObject;
        int randomCorridor = Random.Range(0, corridorPrefab.Count);
        //Decide the type of corridor , 
        //Debug.Log("No of randomCorridor index : " + randomCorridor);
        corridorObject = Instantiate(corridorPrefab[randomCorridor], generatePoint.position, generatePoint.localRotation);
        return corridorObject;
    }

    private void RoomDifficultyLimitSetup()
    {
        roomLimit[0] = easyRoom;
        roomLimit[2] = hardRoom;
        roomLimit[1] = maxRoomAmount - (hardRoom + easyRoom);

        for (int i = 0; i < currentRoomAmount.Length; i++)
        {
            currentRoomAmount[i] = 0;
        }
    }

    private void SetRoomDificulty(int[] targetArray)
    {
        maxRoomAmount = targetArray[0];
        maxCornerUnitAmount = targetArray[1];
        hardRoom = targetArray[2];
        easyRoom = targetArray[3];
    }

    //private float CalculateOffsetBetweenRoom(GameObject lastRoom)//(GameObject lastRoom , GameObject NextRoom)
    //{
    //    //Maybe we can set the size as a variable of room , currently using get child is unsafe

    //    float totalOffset;
    //    //float objectSizeInZ = lastRoom.transform.GetChild(0).localScale.z / 2 + NextRoom.transform.GetChild(0).localScale.z / 2;
    //    //float objectSizeInZ = lastRoom.GetComponentInChildren<GeneratePoint>().GetCubeSize();
    //    totalOffset = Random.Range(minZOffset,maxZOffset) + objectSizeInZ;

    //    return totalOffset;
    //}

    //public void AssignStartPoint(GameObject startPoint)
    //{
    //    startPointList.Add(startPoint);
    //}

    //public void AssignEndPoint(GameObject endPoint)
    //{
    //    endPointList.Add(endPoint);
    //}

}

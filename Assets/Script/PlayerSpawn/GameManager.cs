using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]private enum Difficulty { Easy, Normal, Hard };
    [SerializeField]private static Difficulty selectedDifficulty;

    public static GameManager instance;
    [SerializeField] private GameObject objectPoolerPrefab;
    [SerializeField] private Transform cam;
    //[SerializeField]private Transform oldCheckPoint;
    private static Transform currentSpawnPoint;

    private int totalRoomAmount, hardDifficultyAmount, easyDifficultyAmount, totalCornerUnit;
    private static int[] difficultySetup;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
    }


    public void SpawnPooler()
    {
        Instantiate(objectPoolerPrefab, transform.position, Quaternion.identity);
    }

    public Transform getCam()
    {
        return cam;
    }

    public Transform getCheckPoint()
    {
        return currentSpawnPoint;
    }

    public void PlayerIsDeath(GameObject oldPlayer)
    {
        SpawnPlayer();
        oldPlayer.GetComponent<PlayerMovementFirst>().ResetPlayerControl();
        oldPlayer.GetComponent<AnimController>().InvokeAnimation();
        PlayerObjectPooler.instance.ReturnPool(oldPlayer);
        
    }

    public void SpawnPlayer()
    {
        //Debug.Log("SpawnCalled");
        GameObject newPlayer;
        //call the object pool spwan new player
        newPlayer = PlayerObjectPooler.instance.GetPlayerFromPool();
        //Set to the check point
        newPlayer.transform.position = currentSpawnPoint.position;
        //Set the camera focus new player
        Transform newFollowHead = newPlayer.GetComponent<PlayerManager>().PassFollowHead();
        //Debug.Log("Current Follow head posistion : " + newFollowHead.position);
        CameraFollow.instance.ChangeTarget(newFollowHead); 
    }

    public void UpdateSpawnPoint(Transform newSpawnPoint)
    {
        currentSpawnPoint = newSpawnPoint;
    }

    public void PickDifficulty(int index)
    {
        selectedDifficulty = (Difficulty)index;

        switch(selectedDifficulty)
        {
            case Difficulty.Easy:
                
                totalRoomAmount = 7;
                hardDifficultyAmount = 1;
                easyDifficultyAmount = 2;
                totalCornerUnit = 3;
                break;
                
            case Difficulty.Normal:
                
                totalRoomAmount = 8;
                hardDifficultyAmount = 2;
                easyDifficultyAmount = 2;
                totalCornerUnit = 4;
                break;

            case Difficulty.Hard:
                
                totalRoomAmount = 10;
                hardDifficultyAmount = 4;
                easyDifficultyAmount = 3;
                totalCornerUnit = 6;
                break;

        }
        difficultySetup = new int[4] { totalRoomAmount, totalCornerUnit, hardDifficultyAmount, easyDifficultyAmount };
        //Debug.Log("Setting up the difficulty setup , totalRoom: " + totalRoomAmount + "  totalCorner :" + totalCornerUnit + "  hardAmount : " + hardDifficultyAmount + "  EasyAmount : " + easyDifficultyAmount);
        GetInPlayScene();

    }

    public void GetInPlayScene()
    {
        SceneManager.LoadScene(1);
    }

    public void GetBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public int[] GetDifficlutlySetup()
    {
        return difficultySetup;
    }
}

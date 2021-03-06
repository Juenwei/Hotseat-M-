using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectPooler : MonoBehaviour
{
    public static PlayerObjectPooler instance;
    [SerializeField]private GameObject playerPrefabs;
    public int pooledAmount;
    [SerializeField]private Queue<GameObject> playerQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        FillPool();
    }

    public void FillPool()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(playerPrefabs);
            ReturnPool(obj);

        }
    }

    public GameObject GetPlayerFromPool()
    {
        if (playerQueue.Count == 0)
        {
           
            FillPool();
        }
        
        GameObject obj = playerQueue.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnPool(GameObject obj)
    {
        obj.SetActive(false);
        playerQueue.Enqueue(obj);
    }
}

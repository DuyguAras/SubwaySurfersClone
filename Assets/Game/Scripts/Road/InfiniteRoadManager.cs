using System.Collections.Generic;
using CoreGames.GameName;
using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfiniteRoadManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int poolSize;
    [SerializeField] private float spawnInterval;

    private List<GameObject> objectPool = new();
    private float currentTime;

    private int objectCounter;
    private int distance;
    private bool canSpawn;
    private bool isCountable;
    private bool isFirstPrepare;

    private void OnEnable()
    {
        EventBus<GamePrepareEvent>.AddListener(ResetLevel);
        EventBus<GameStartEvent>.AddListener(StartCounter);
    }

    private void OnDisable()
    {
        EventBus<GamePrepareEvent>.RemoveListener(ResetLevel);
        EventBus<GameStartEvent>.RemoveListener(StartCounter);
    }

    private void Start()
    {
        InitializeObjectPool();

        canSpawn = true;
        isCountable = false;
        isFirstPrepare = true;
        currentTime = spawnInterval;
    }

    private void Update()
    {
        if (isCountable)
        {
            currentTime -= Time.deltaTime;

            if (spawnInterval > 7)
            {
                spawnInterval -= 0.15f * Time.deltaTime;
            }

            if (currentTime <= 0)
            {
                if (!canSpawn)
                    SortPrefab();
           
                currentTime = spawnInterval;
            }
        }
    }

    private void InitializeObjectPool()
    {
        List<int> selectedPrefabIndices = new List<int>();

        for (int i = 0; i < poolSize; i++)
        {
            int randomPrefabIndex;

            do
            {
                randomPrefabIndex = Random.Range(0, prefabs.Length);
            } while (selectedPrefabIndices.Contains(randomPrefabIndex));

            selectedPrefabIndices.Add(randomPrefabIndex);

            GameObject obj = Instantiate(prefabs[randomPrefabIndex], transform.position, Quaternion.identity);
            obj.SetActive(true);
            objectPool.Add(obj);
        }

        SpawnPrefab();
    }

    private void SortPrefab()
    {
        objectPool[0].transform.position = objectPool[objectPool.Count - 1].transform.position + new Vector3(0, 0f, 75f);
        objectPool[0].gameObject.GetComponent<GoldManager>().ResetGolds();
        objectPool.Sort(SortByZPosition);
    }

    private void SpawnPrefab()
    {
        foreach (GameObject obj in objectPool)
        {
            obj.transform.position += new Vector3(0, 0f, 0 + distance);
            distance += 75;

            objectCounter++;

            if (objectCounter == 5)
                canSpawn = false;
        }
    }

    private int SortByZPosition(GameObject obj1, GameObject obj2)
    {
        return obj1.transform.position.z.CompareTo(obj2.transform.position.z);
    }

    private void ResetLevel(object sender, GamePrepareEvent e)
    {
        if (!isFirstPrepare)
        {
            int distance = 0;

            foreach (GameObject obj in objectPool)
            {
                obj.transform.position = new Vector3(-6, 0, 5 + distance);
                distance += 75;
            }
        }

        isCountable = false;
        spawnInterval = 15;
        currentTime = spawnInterval;
        isFirstPrepare = false;
    }

    private void StartCounter(object sender, GameStartEvent e)
    {
        if (!isFirstPrepare)
        {
            isCountable = true;
            canSpawn = false;
            distance = 0;
        }
    }
}
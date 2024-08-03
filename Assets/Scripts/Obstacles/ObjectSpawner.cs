using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float spawnInterval = 2f;
    private PlayerController controller;

    [SerializeField] private PoolManager poolManager;
    [SerializeField] private string[] poolTags;


    private void OnEnable()
    {
        controller = FindAnyObjectByType<PlayerController>();
    }

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 2f, spawnInterval);
    }

    void SpawnObstacle()
    {
        string tag = poolTags[Random.Range(0, poolTags.Length)];
        GameObject obstacle = poolManager.GetPoolObject(tag);

        if (obstacle != null)
        {
            obstacle.transform.position = transform.position;
            obstacle.transform.rotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("No object available in pool with tag: " + tag);
        }
    }

    private void Update()
    {
        if (controller.isGameOver == true)
        {
            CancelInvoke();
        }
    }


}

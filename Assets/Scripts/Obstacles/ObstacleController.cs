using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float speed = 5f;
    private PlayerController controller;


    private void OnEnable()
    {
        controller = FindAnyObjectByType<PlayerController>();
    }

    void Update()
    {
        if (controller.isGameOver == false)
        {
            transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
        }

        if (transform.position.z < -10)
        {
            gameObject.SetActive(false);
        }
    }
}

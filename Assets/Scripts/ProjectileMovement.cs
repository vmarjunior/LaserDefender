using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float yOffset = 0f;
    [SerializeField] bool playerShooting = false;

    Vector2 projectileDirection;

    void Awake()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + yOffset);
    }

    void Start()
    {
        if (playerShooting)
            projectileDirection = new Vector2(transform.position.x, transform.position.y + 20);
        else
            projectileDirection = new Vector2(transform.position.x, transform.position.y - 20);
    }

    void Update()
    {
        float moveSpeed = projectileSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, projectileDirection, moveSpeed);
    }
}

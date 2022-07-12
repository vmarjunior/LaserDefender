using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [Header("General")]
    [SerializeField] RawImage backgroundImage;
    [SerializeField] float xPos, yPos;

    [Header("Randomness")]
    [SerializeField] bool randomSpawn;
    [SerializeField] float spawnSeconds = 5f;
    [SerializeField] float percentageSpawnChance = 50f;

    float elapsedTime = 0f;
    Vector2 initialPosition;

    void Awake()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        UpdateImage();
    }

    void UpdateImage()
    {
        if (!randomSpawn)
            backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(xPos, yPos) * Time.deltaTime, backgroundImage.uvRect.size);
        else
        {
            if (elapsedTime >= spawnSeconds)
            {
                var spawnChance = UnityEngine.Random.Range(0, 100);
                if (spawnChance <= percentageSpawnChance)
                {
                    transform.position = initialPosition;
                }

                elapsedTime = 0f - Random.Range(0, 2);
            }
            else
            {
                transform.position = (transform.position + new Vector3(xPos * Time.deltaTime, yPos * Time.deltaTime, 0));
                elapsedTime += Time.deltaTime;
            }
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileLifetime = 3f;
    [SerializeField] float firingRate = 0.5f;
    
    [Header("AI")]
    [SerializeField] bool useAI = false;

    [HideInInspector] public bool isFiring = false;
    public Coroutine FiringCoroutine;
    Health objectHealth;
    AudioPlayer audioPlayer;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        objectHealth = FindObjectOfType<Health>();

        if (useAI)
            isFiring = true;
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring)
        {
            if (FiringCoroutine == null)
                FiringCoroutine = StartCoroutine(FireContinuosly());
        }
        else if (FiringCoroutine != null)
        {
            StopCoroutine(FiringCoroutine);
            FiringCoroutine = null;
        }
    }

    IEnumerator FireContinuosly()
    {
        while(true)
        {
            if (objectHealth.GetHealth() > 0)
            {
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Destroy(projectile, projectileLifetime);

                if (gameObject.tag == "Player")
                    audioPlayer.PlayShootingClip();
            }

            if (useAI)
            {
                yield return new WaitForSecondsRealtime(firingRate + UnityEngine.Random.Range(0, 0.5f));
            }
            else
                yield return new WaitForSecondsRealtime(firingRate);
        }
    }
}

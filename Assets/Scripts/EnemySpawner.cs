using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 4f;
    [SerializeField] bool isLooping = true;
    WaveConfigSO currentWave;

    void Start() 
    {
        StartCoroutine(SpawnEnemieWaves());
    }

    IEnumerator SpawnEnemieWaves()
    {
        //Fixed amount for the background music
        yield return new WaitForSecondsRealtime(10);

        do
        {
            foreach (var wave in waveConfigs)
            {
                currentWave = wave;

                for(int j = 0; j < currentWave.GetEnemyCount(); j++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(j), 
                                currentWave.GetStartingWaypoint().position,
                                Quaternion.identity,
                                transform);

                    yield return new WaitForSecondsRealtime(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSecondsRealtime(timeBetweenWaves * Time.deltaTime);
            }
        } while (isLooping);
    }


    public WaveConfigSO GetCurrentWave() => currentWave;
}

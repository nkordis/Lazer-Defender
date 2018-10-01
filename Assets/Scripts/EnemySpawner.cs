using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {


    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField]int startingWave = 0;
    [SerializeField] bool looping = false;


    // Loop all enemy waves
    IEnumerator Start() {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
        
	}

    //It spawns all the waves
    private IEnumerator SpawnAllWaves()
    {
        for(int wave = startingWave; wave < waveConfigs.Count; wave++)
        {
            var currentWave = waveConfigs[wave]; 
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    // It spawns all the enemies in a wave
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {

        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                waveConfig.GetWayPoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    
}

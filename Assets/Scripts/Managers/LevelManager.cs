using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform map;
    [HideInInspector] public Bounds mapBounds;
    WaveManager waveManager;
    [SerializeField] Wave currentWave;
    int currentWaveIndex;

    void Awake()
    {
        mapBounds.center = map.position;
        mapBounds.extents = map.localScale / 2;
    }

    void Start()
    {
        waveManager = GameManager.instance.enemyManager.GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave.GetEnemyCount() == 0) // Y que todos los enemigos estan muertos 
        {
            // Init Wave -> - UI Wave x -
            // Timer 5
            currentWave = waveManager.GetWave(currentWaveIndex);
            currentWaveIndex += 1;
        }
    }

    public int GetCurrentWaveIndex()
    {
        return currentWaveIndex;
    }

    public Wave GetCurrentWave()
    {
        return currentWave;
    }
}

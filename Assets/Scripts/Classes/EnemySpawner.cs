using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Header("Show Gizmos")]
    [SerializeField] bool showGizmos;

    [Header("Bounding Plane - Takes First 2 elements of list")]
    [SerializeField] List<float> circleRadii = new List<float>();
    [SerializeField] float cooldown;
    float temp, angle, dist;

    [SerializeField] Transform player, spawnLooker;



    // Start is called before the first frame update
    void Start()
    {
        temp = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawnEnemy();
    }

    GameObject CheckSpawnEnemy()
    {
        if (cooldown <= 0)
        {
            angle = Random.Range(0, 360);
            dist = Random.Range(circleRadii.Min(), circleRadii.Max());

            spawnLooker.eulerAngles = new Vector3(0, 0, angle);

            cooldown = temp; // Capaz joder aca para cuanto mas rondas mas rapido spawnean bichos

            Vector3 spawnPoint = player.position + spawnLooker.up * dist;

            if (GameManager.instance.levelManager.mapBounds.Contains(spawnPoint) && GameManager.instance.enemyManager.canSpawn)
            {
                GameObject original = GameManager.instance.enemyManager.GetEnemy();
                if (original == null)
                {
                    return null;
                }

                GameObject enemy = Instantiate(original, transform);
                enemy.transform.position = spawnPoint;
                GameManager.instance.enemyManager.dictEnemiesVivos.Add(GameManager.instance.enemyManager.enemyCounter, enemy);
                enemy.GetComponent<EnemyController>().id = GameManager.instance.enemyManager.enemyCounter;
                GameManager.instance.enemyManager.enemyCounter += 1;
                return enemy;
            }

        }
        else
            cooldown -= Time.deltaTime;

        return null;
    }

    void OnDrawGizmos()
    {
        if (!showGizmos)
            return;


        Gizmos.color = Color.red;

        foreach (float radius in circleRadii)
        {
            Gizmos.DrawWireSphere(player.position, radius);
        }

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(player.position, player.position + spawnLooker.up * dist);
    }
}

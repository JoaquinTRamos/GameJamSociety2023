using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        GameManager.instance.enemyManager.listaEnemigos.Add(CheckSpawnEnemy());
    }

    GameObject CheckSpawnEnemy()
    {
        if (cooldown <= 0)
        {
            angle = Random.Range(0, 360);
            dist = Random.Range(circleRadii.Min(), circleRadii.Max());

            spawnLooker.eulerAngles = new Vector3(0, 0, angle);

            cooldown = temp;

            Vector3 spawnPoint = player.position + spawnLooker.up * dist;

            if (GameManager.instance.levelManager.mapBounds.Contains(spawnPoint))
            {
                GameObject enemy = Instantiate(GameManager.instance.enemyManager.GetEnemy(), transform);
                enemy.transform.position = spawnPoint;
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

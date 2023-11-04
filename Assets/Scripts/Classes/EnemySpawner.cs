using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Header("Bounding Plane - Takes First 2 elements of list")]
    [SerializeField] List<float> circleRadii = new List<float>();
    [SerializeField] float angle, dist;
    [SerializeField] float cooldown;
    float temp;

    [SerializeField] Transform player, spawnLooker, map;
    Bounds mapBounds;
    [SerializeField] GameObject enemyGO;
    List<GameObject> listaEnemigos = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        temp = cooldown;

        mapBounds.center = map.position;
        mapBounds.extents = map.localScale / 2;
        print(mapBounds.size);
    }

    // Update is called once per frame
    void Update()
    {
        listaEnemigos.Add(CheckSpawnEnemy());
    }

    GameObject CheckSpawnEnemy()
    {
        if (cooldown <= 0)
        {
            angle = Random.Range(0, 360);
            dist = Random.Range(circleRadii.Min(), circleRadii.Max());

            spawnLooker.eulerAngles = new Vector3(0, 0, angle);

            cooldown = temp;

            Vector3 spawnPoint = (player.position + spawnLooker.up * dist);

            if (mapBounds.Contains(spawnPoint))
            {
                print("This point is within the map bounds  Vector3:" + spawnPoint);
                GameObject enemy = Instantiate(enemyGO, transform);
                enemy.transform.position = spawnPoint;
                return enemy;
            }
            else
            {
                print("This point is not within the map bounds Vector3:" + spawnPoint);
            }
        }
        else
            cooldown -= Time.deltaTime;

        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (float radius in circleRadii)
        {
            Gizmos.DrawWireSphere(player.position, radius);
        }

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(player.position, (player.position + spawnLooker.up * dist));

    }
}

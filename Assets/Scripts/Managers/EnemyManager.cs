using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> listaEnemigos = new List<GameObject>();
    [SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetEnemy(Element element)
    {
        foreach (GameObject enemy in enemyPrefabs)
        {
            if (enemy.GetComponent<EnemyController>().enemyData.elementType == element)
                return enemy;
        }

        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    public GameObject GetEnemy()
    {
        Wave currWave = GameManager.instance.levelManager.GetCurrentWave();
        List<Element> validElements = currWave.GetValidElements();

        int randomIndex = Random.Range(0, validElements.Count - 1);

        foreach (GameObject enemy in enemyPrefabs)
        {
            if (enemy.GetComponent<EnemyController>().enemyData.elementType == validElements[randomIndex])
            {
                currWave.SubtractFromElement(validElements[randomIndex]);
                return enemy;
            }
        }

        return null;
    }
}

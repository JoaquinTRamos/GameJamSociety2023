using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [HideInInspector] public Dictionary<int, GameObject> dictEnemiesVivos = new Dictionary<int, GameObject>();
        [SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();
        [HideInInspector] public int enemyCounter;
        [HideInInspector] public bool canSpawn;



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
}

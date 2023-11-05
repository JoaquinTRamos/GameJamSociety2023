using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class WaveManager : MonoBehaviour
    {
        public List<Wave> waves = new List<Wave>();
        [SerializeField] int baseEnemyQuantity, lastEnemyQuantity;
        [SerializeField] float scalarEnemyQuantity;

        // Start is called before the first frame update
        void Start()
        {
            lastEnemyQuantity = baseEnemyQuantity;
        }

        public Wave GetWave(int index)
        {
            if (waves.Count - index < 3)
            {
                GenerateRandomWave(Mathf.FloorToInt(lastEnemyQuantity * (1f + scalarEnemyQuantity)));
                lastEnemyQuantity = Mathf.FloorToInt(lastEnemyQuantity * (1f + scalarEnemyQuantity));
            }

            return waves[index];
        }

        void GenerateRandomWave(int enemyQuantity)
        {
            Wave wave = new Wave();

            List<float> randFloats = new List<float>();

            for (int i = 0; i < 4; i++)
            {

                if (i == 0)
                {
                    randFloats.Add(UnityEngine.Random.Range(0.01f, 0.6f));
                    continue;
                }

                if (randFloats[i - 1] == 1f)
                    break;

                randFloats.Add(UnityEngine.Random.Range(randFloats[i - 1], 1f));
            }

            List<Element> elements = new List<Element>
            {
                Element.Fire,
                Element.Water,
                Element.Earth,
                Element.Wind,
                Element.Lightning
            };

            elements = elements.OrderBy(x => UnityEngine.Random.value).ToList();
            int j = 0;

            foreach (float f in randFloats)
            {
                if (j == 0)
                {
                    wave.AssignElementQuantity(elements[j], Mathf.FloorToInt(f * enemyQuantity));
                    j++;
                    continue;
                }

                wave.AssignElementQuantity(elements[j], Mathf.RoundToInt((f - randFloats[j - 1]) * enemyQuantity));
                j++;

            }

            waves.Add(wave);


        }
    }
}

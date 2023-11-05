using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public GameObject playerObject;
    public EnemyManager enemyManager;
    public LevelManager levelManager;

    void Awake()
    {
        // Chequea que sea la unica instancia del GameManager.
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

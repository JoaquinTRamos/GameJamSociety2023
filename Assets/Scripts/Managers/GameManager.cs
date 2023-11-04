using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public GameObject playerObject;

    public List<MonoBehaviour> managers = new List<MonoBehaviour>(); // Fijarse en poner un interface de managers para el futuro.

    void Start()
    {
        // Chequea que sea la unica instancia del GameManager.
        if (this != instance)
        {
            Destroy(instance);
            instance = this;
            DontDestroyOnLoad(this);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

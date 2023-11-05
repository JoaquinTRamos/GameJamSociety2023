using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform map;
    public Bounds mapBounds;

    void Awake()
    {
        mapBounds.center = map.position;
        mapBounds.extents = map.localScale / 2;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

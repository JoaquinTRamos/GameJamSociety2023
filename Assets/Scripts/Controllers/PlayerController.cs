using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float horMove, verMove;
    [SerializeField] float speedMod;

    [SerializeField] Transform map;

    Bounds mapBounds;

    // Start is called before the first frame update
    void Start()
    {
        mapBounds.center = map.position;
        mapBounds.extents = map.localScale / 2;
        print(mapBounds.size);
    }

    // Update is called once per frame
    void Update()
    {
        horMove = Input.GetAxisRaw("Horizontal");
        verMove = Input.GetAxisRaw("Vertical");

        if (inBoundingBox())
            transform.Translate(new Vector3(horMove, verMove, 0) * speedMod * Time.deltaTime); // De alguna manera hacer que rebote el pibe

        if (Input.GetMouseButtonDown(0))
        {
            print("An attempt to attack has been made");
        }

        if (Input.GetMouseButtonDown(1))
        {
            print("An attempt to grab has been made");
        }

    }

    bool inBoundingBox()
    {
        if (mapBounds.Contains(transform.position))
            return true;

        return false;
    }
}

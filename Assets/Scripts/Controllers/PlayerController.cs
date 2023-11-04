using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float horMove, verMove;
    [SerializeField] float speedMod;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horMove = Input.GetAxisRaw("Horizontal");
        verMove = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(horMove, verMove, 0) * speedMod * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            print("An attempt to attack has been made");
        }

        if (Input.GetMouseButtonDown(1))
        {
            print("An attempt to grab has been made");
        }

    }
}

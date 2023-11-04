using System;
using System.Collections;
using System.Collections.Generic;
using Guns;
using UnityEngine;


// Asignar por inspector el arma que tiene que ser el enemigo
public class PlayerController : MonoBehaviour
{
    float horMove, verMove;
    [SerializeField] float speedMod;
    private GunModel currGun;
    EnemyController closestEnemy = null;

    [SerializeField] Transform map;

    Bounds mapBounds;

    [SerializeField] private float detectionRadius = 3f;
    private List<EnemyController> EnemiesInRange = new List<EnemyController>();


    // Start is called before the first frame update
    void Start()
    {
        mapBounds.center = map.position;
        mapBounds.extents = map.localScale / 2;
        print(mapBounds.size);
        StartCoroutine(CheckDistances());

    }
//TODO: Mergear al colo
    private void TakeEnemy(EnemyController enemy)
    {
        print(enemy);
    }

    public void SetGun(GunModel gun) => currGun = gun;

    private void Shoot()
    {
        currGun.Shoot(Vector2.right);
    }

    private void Throw()
    {
        currGun.Throw();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject parentObject = other.transform.parent.gameObject;
        EnemyController enemy = parentObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            EnemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject parentObject = other.transform.parent.gameObject;
        EnemyController enemy = parentObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            EnemiesInRange.Remove(enemy);
        }
    }

    private IEnumerator CheckDistances()
    {
        while (true)
        {
            closestEnemy = null;
            float closestDistance = detectionRadius;
            foreach (EnemyController enemy in EnemiesInRange)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestEnemy = enemy;
                    closestDistance = distance;
                }
            }

            if (closestEnemy != null)
            {
                closestEnemy.Highlight();
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        horMove = Input.GetAxisRaw("Horizontal");
        verMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.F) && closestEnemy != null)
        {
            TakeEnemy(closestEnemy);
        }
        if (inBoundingBox())
            transform.Translate(new Vector3(horMove, verMove, 0) * speedMod * Time.deltaTime); // De alguna manera hacer que rebote el pibe

        if (Input.GetMouseButtonDown(0))
        {
            print("An attempt to attack has been made");
        }

        if (Input.GetMouseButtonDown(1))
        {
            print("An attempt to throw has been made");
        }

    }

    bool inBoundingBox()
    {
        if (mapBounds.Contains(transform.position))
            return true;

        return false;
    }
}


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

    private void TakeEnemy(EnemyController enemy)
    {
        SetGun(enemy.GetGun());
        Destroy(enemy.gameObject);
        currGun.transform.position = transform.position;
        currGun.transform.SetParent(transform);

        print(currGun);
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
        //print(horMove + " " + verMove);
        if (Input.GetKeyDown(KeyCode.F) && closestEnemy != null)
        {
            print(closestEnemy.name);
            TakeEnemy(closestEnemy);
        }
        if (inBoundingBox())
            transform.Translate(new Vector3(horMove, verMove, 0) * speedMod * Time.deltaTime); // De alguna manera hacer que rebote el pibe

        if (Input.GetMouseButtonDown(0))
        {
            print("An attempt to shoot has been made");
            if(currGun != null)
                Shoot();
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


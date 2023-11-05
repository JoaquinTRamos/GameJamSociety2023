using System;
using System.Collections;
using System.Collections.Generic;
using Guns;
using UnityEditor.XR;
using UnityEngine;


// Asignar por inspector el arma que tiene que ser el enemigo
public class PlayerController : MonoBehaviour
{
    float horMove, verMove;
    [SerializeField] float speedMod;
    private GunModel currGun;
    bool isThrowing = false;
    private Coroutine throwCoroutine;
    EnemyController closestEnemy = null;

    private float throwSpeed = 0f;
    public const float throwAcceleration = 2f; // The speed at which the throw speed increases

    public const float cooldown = 0.5f;


    [SerializeField] private float detectionRadius = 3f;
    private List<EnemyController> EnemiesInRange = new List<EnemyController>();


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckDistances());
    }

    private void TakeEnemy(EnemyController enemy)
    {
        if (currGun != null)
            Destroy(currGun.gameObject);
        throwSpeed = 0f;
        GunModel gun = enemy.GetGun();
        currGun = Instantiate(gun, transform);
        Instantiate(gun.GetData().gunSkin, currGun.transform);
        //move currgun a bit down and left to the player
        currGun.transform.localPosition = new Vector2(0.5f, 0.25f);

        //currGun.transform.localPosition = new Vector2(0, -10);
        enemy.OnDie();
        //currGun.transform.SetParent(transform);
        //currGun.transform.position = new Vector2();

        // print(currGun);
    }

    //public void SetGun(GunModel gun) => currGun = gun;

    private void Shoot()
    {
        //Shoot in the direction of the mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - transform.position).normalized;
        currGun.Shoot(dir);
    }

    private void Throw()
    {
        currGun.transform.SetParent(null);
        GunModel newGun = currGun;
        Instantiate(newGun.GetData());
        currGun = null;
        Rigidbody2D rb = newGun.GetComponent<Rigidbody2D>();
        rb.velocity = newGun.transform.up * throwSpeed;

    }

    private IEnumerator Throwing()
    {
        while (isThrowing)
        {
            if (currGun.transform.parent != null){
                currGun.transform.RotateAround(transform.position, Vector3.forward, (360+throwSpeed*20) * Time.deltaTime);
                if (throwSpeed < 30f)
                    throwSpeed += throwAcceleration * Time.deltaTime;
                Debug.Log(throwSpeed);
            }
            yield return null;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.gameObject.layer != 11)
            return;

        //GameObject parentObject = other.transform.parent.gameObject;
        if (other.TryGetComponent<EnemyController>(out var enemy))
        {
            EnemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != 11)
            return;

        //GameObject parentObject = other.transform.parent.gameObject;
        if (other.TryGetComponent<EnemyController>(out var enemy))
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
            // print(closestEnemy.name);
            TakeEnemy(closestEnemy);
        }
        if (inBoundingBox())
            transform.Translate(new Vector3(horMove, verMove, 0) * speedMod * Time.deltaTime); // De alguna manera hacer que rebote el pibe

        if (Input.GetMouseButton(0))
        {
            if (currGun != null)
                Shoot();
        }


        if (currGun != null)
            if (Input.GetMouseButton(1) && !isThrowing)
            {
                isThrowing = true;
                throwCoroutine = StartCoroutine(Throwing());
            }

        // If 1 key is released and the gun is being thrown, stop the throw
        if (Input.GetMouseButtonUp(1) && isThrowing)
        {
            if (throwCoroutine != null)
            {
                isThrowing = false;
                StopCoroutine(throwCoroutine);

                Throw();
                
            }
        }


    }

    bool inBoundingBox()
    {
        if (GameManager.instance.levelManager.mapBounds.Contains(transform.position))
            return true;

        return false;
    }
}

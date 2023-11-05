using System.Collections;
using System.Collections.Generic;
using Guns;
using Managers;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemy, IDamageable
{
    public EnemyData enemyData;

    Transform highlighteablePart;

    private HealthController healthController;
    public int id;
    public ParticleSystem fireParticles;
    public ParticleSystem waterParticles;
    public ParticleSystem windParticles;
    public ParticleSystem earthParticles;
    public ParticleSystem lightningParticles;
    private bool isStunned = false;
    private bool isDelayed = false;
    private float originalSpeed;


    public GunModel gunModel;

    GameObject skinInstance;

    public GunModel GetGun() => gunModel;


    // Start is called before the first frame update
    void Start()
    {
        healthController = GetComponent<HealthController>();
        healthController.Initialize(enemyData.health);
        healthController.OnChangeHealth += OnTakeDamage;
        healthController.OnDie += OnDie;
        // print(gunModel);

        GameObject skin = enemyData.skin;
        skinInstance = Instantiate(skin, transform);
        skin.tag = "Enemy";

        AssignHighlightablePart();

    }

    void AssignHighlightablePart()
    {
        Element element = enemyData.elementType;

        if (element == Element.Fire)
            highlighteablePart = skinInstance.transform.GetChild(0);

        if (element == Element.Water)
            highlighteablePart = skinInstance.transform.GetChild(0).GetChild(0);

        if (element == Element.Earth)
            highlighteablePart = skinInstance.transform.GetChild(0).GetChild(0);

        if (element == Element.Lightning)
            highlighteablePart = skinInstance.transform.GetChild(0).GetChild(0);

        if (element == Element.Wind)
            highlighteablePart = skinInstance.transform.GetChild(0);
    }

    public void Highlight()
    {
        print("Enemy Highlighted");
        highlighteablePart.GetComponent<SpriteRenderer>().material.SetInt("_HighlightBool", 1);
        StartCoroutine(RemoveHighlightAfterSeconds(0.2f)); // highlight will be removed after 3 seconds
    }

    IEnumerator RemoveHighlightAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        highlighteablePart.GetComponent<SpriteRenderer>().material.SetInt("_HighlightBool", 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPosition = GameManager.instance.playerObject.transform.position;
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
        Vector3 movement;
        if (isStunned)
            movement = Time.deltaTime * 0 * directionToPlayer;

        else if (isDelayed)
            movement = Time.deltaTime * enemyData.speedMod * directionToPlayer / 2f;
        else
            movement = Time.deltaTime * enemyData.speedMod * directionToPlayer;

        transform.Translate(movement);
    }

    public void Damage(int damage, Element element)
    {
        Debug.Log(damage);
        //if element is the same as my element, increase size
        if (element == enemyData.elementType)
        {

            GameObject skin = transform.GetChild(2).gameObject;

            damage = damage / 2;
            if (skin.transform.localScale.x < 2)
                skin.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        }
        Debug.Log(damage);
        healthController.TakeDamage(damage);
        switch (element)
        {
            case Element.Fire:
                Debug.Log("DANO FUEGO fshh" + transform.childCount);

                if (transform.childCount == 3)
                {
                    GameObject temp = Instantiate(fireParticles, transform).gameObject;
                    temp.GetComponent<ParticleSystem>().Play();
                    StartCoroutine(TakeDamageOverTime(5, 5f));
                    Destroy(temp, 5f);
                }
                return;
            case Element.Water:
                Debug.Log("DANO AGUA SPLASH");

                if (transform.childCount == 3)
                {
                    GameObject temp = Instantiate(waterParticles, transform).gameObject;
                    StartCoroutine(delayCoroutine(5f));
                    Destroy(temp, 5f);
                }

                return;
            case Element.Wind:
                Debug.Log("DANO AIRE WOOSH");
                if (transform.childCount == 3)
                {
                    GameObject temp = Instantiate(windParticles, transform).gameObject;
                    temp.GetComponent<ParticleSystem>().Play();
                    StartCoroutine(PushBack(10f, 1f));
                    Destroy(temp, 5f);
                }

                

                return;
            case Element.Lightning:
                Debug.Log("DANO ELECTRICIDAD ZAP");
                if (transform.childCount == 3)
                {
                    GameObject temp = Instantiate(lightningParticles, transform).gameObject;
                    temp.GetComponent<ParticleSystem>().Play();
                    Stun(3f);

                    Destroy(temp, 1f);
                }

                return;
            case Element.Earth:
                Debug.Log("DANO TIERRA CRACK");
                if (transform.childCount == 3)
                {
                    GameObject temp = Instantiate(earthParticles, transform).gameObject;
                    temp.GetComponent<ParticleSystem>().Play();
                    Destroy(temp, 5f);
                }

                

                return;
        }







    }
    private IEnumerator PushBack(float distance, float duration)
    {
        Stun(duration);
        Vector3 directionToPlayer = (GameManager.instance.playerObject.transform.position - transform.position).normalized;
        Vector3 targetPosition = transform.position - directionToPlayer * distance;

        float timer = 0;
        Vector3 startPosition = transform.position;

        while (timer < duration)
        {
            float t = timer / duration; // Normalized time between 0 and 1
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the enemy ends up at the exact target position
        transform.position = targetPosition;
    }
    private IEnumerator TakeDamageOverTime(int damage, float duration)
    {
        float timer = 0;

        while (timer < duration)
        {
            healthController.TakeDamage(damage);
            timer += 1f;
            yield return new WaitForSeconds(1f);
        }
    }
    void Stun(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(duration));
        }
    }
    private IEnumerator StunCoroutine(float n)
    {
        isStunned = true;


        yield return new WaitForSeconds(n);

        isStunned = false;
    }
    private IEnumerator delayCoroutine(float n)
    {
        isDelayed = true;


        yield return new WaitForSeconds(n);

        isDelayed = false;
    }
    public void OnDie()
    {
        GameManager.instance.enemyManager.dictEnemiesVivos.Remove(id);
        Destroy(gameObject);
    }


    private void OnTakeDamage(int max, int current)
    {
        Transform totalHealthBar = transform.GetChild(1);
        Transform remainingHealthBar = totalHealthBar.GetChild(0);

        Vector3 scale = remainingHealthBar.localScale;
        Vector3 position = remainingHealthBar.localPosition;

        scale.x = (float)current / (float)max;
        remainingHealthBar.localScale = scale;

        Vector3 lossyRemaining = remainingHealthBar.lossyScale;
        Vector3 lossyTotal = totalHealthBar.lossyScale;

        position.x = -((lossyTotal.x - lossyRemaining.x) / 2f);
        remainingHealthBar.localPosition = position;
    }


}

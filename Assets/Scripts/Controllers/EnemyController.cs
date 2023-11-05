using System.Collections;
using System.Collections.Generic;
using Guns;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemy, IDamageable
{
    public EnemyData enemyData;
    private HealthController healthController;
    public int id;
    public GunModel gunModel;

    public GunModel GetGun() => gunModel;


    // Start is called before the first frame update
    void Start()
    {
        healthController = GetComponent<HealthController>();
        healthController.Initialize(enemyData.health);
        healthController.OnDie += OnDie;
        healthController.OnChangeHealth += OnTakeDamage;

        ChangeColorOnElement();
    }
    public void Highlight()
    {
        //print("Enemy Highlighted");
        //gameObject.GetComponent<SpriteRenderer>().material.SetInt("_HighlightBool", 1);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(RemoveHighlightAfterSeconds(0.2f)); // highlight will be removed after 3 seconds
    }

    IEnumerator RemoveHighlightAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //gameObject.GetComponent<SpriteRenderer>().material.SetInt("_HighlightBool", 0);
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPosition = GameManager.instance.playerObject.transform.position;
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
        Vector3 movement = Time.deltaTime * enemyData.speedMod * directionToPlayer;

        transform.Translate(movement);
    }


    void ChangeColorOnElement()
    {
        if (enemyData.elementType == Element.Fire)
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        else if (enemyData.elementType == Element.Wind)
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        else if (enemyData.elementType == Element.Earth)
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        else if (enemyData.elementType == Element.Lightning)
            gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        else if (enemyData.elementType == Element.Water)
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        else
            print("No element assigned!");
    }


    public void Damage(int damage, Element element)
    {
        healthController.TakeDamage(damage);

        switch (element)
        {
            case Element.Fire:
                return;
            case Element.Water:
                return;
            case Element.Wind:
                return;
            case Element.Lightning:
                return;
            case Element.Earth:
                return;
        }
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

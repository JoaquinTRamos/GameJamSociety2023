using System.Collections;
using System.Collections.Generic;
using Guns;
using UnityEngine;




public class EnemyController : MonoBehaviour, IEnemy, IDamageable
{
    public EnemyData enemyData;
    private HealthController healthController;

    public GunModel gunModel;

    public GunModel GetGun() => gunModel;


    // Start is called before the first frame update
    void Start()
    {
        /* healthController = GetComponent<HealthController>();
        healthController.Initialize(enemyData.health); */
        // print(gunModel);

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


    public void Damage(int damage, Element element, System.Numerics.Vector3 direction)
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
}

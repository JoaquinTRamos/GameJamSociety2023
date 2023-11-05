using System.Collections;
using System.Collections.Generic;
using Guns;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemy, IDamageable
{
    public EnemyData enemyData;
    private HealthController healthController;
    public int id;    public ParticleSystem fireParticles;
    public ParticleSystem waterParticles;
    public ParticleSystem windParticles;
    public ParticleSystem earthParticles;
    public ParticleSystem lightningParticles;

    public GunModel gunModel;

    public GunModel GetGun() => gunModel;


    // Start is called before the first frame update
    void Start()
    {
        healthController = GetComponent<HealthController>();
        healthController.Initialize(enemyData.health);
        healthController.OnChangeHealth += OnTakeDamage;
        healthController.OnDie += OnDie;
        // print(gunModel);

        GameObject skin =enemyData.skin;
        Instantiate(skin,transform);
        skin.tag="Enemy";

    }
    public void Highlight()
    {
        //print("Enemy Highlighted");
        //gameObject.GetComponent<SpriteRenderer>().material.SetInt("_HighlightBool", 1);
        //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(RemoveHighlightAfterSeconds(0.2f)); // highlight will be removed after 3 seconds
    }

    IEnumerator RemoveHighlightAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //gameObject.GetComponent<SpriteRenderer>().material.SetInt("_HighlightBool", 0);
        //gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPosition = GameManager.instance.playerObject.transform.position;
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
        Vector3 movement = Time.deltaTime * enemyData.speedMod * directionToPlayer;

        transform.Translate(movement);
    }


    /* void ChangeColorOnElement()
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
    } */


    public void Damage(int damage, Element element)
    {
        healthController.TakeDamage(damage);
        Debug.Log(damage);
         //if element is the same as my element, increase size
        if (element == enemyData.elementType)
        {   

            GameObject skin = transform.GetChild(2).gameObject;
            
            Debug.Log(skin.name);
            if (skin.transform.localScale.x < 2)
                skin.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        }

        switch (element)
        {
            case Element.Fire:
                Debug.Log("DANO FUEGO fshh"+transform.childCount);
                
                if(transform.childCount == 3){
                    GameObject temp = Instantiate(fireParticles,transform).gameObject;
                    temp.GetComponent<ParticleSystem>().Play();
                    Destroy(temp, 5f);
                    }
                

                
                return;
            case Element.Water:
                Debug.Log("DANO AGUA SPLASH");

                if(transform.childCount == 3){
                    GameObject temp = Instantiate(waterParticles,transform).gameObject;
                    temp.GetComponent<ParticleSystem>().Play();
                    Destroy(temp, 5f);
                    }
                
                return;
            case Element.Wind:
                Debug.Log("DANO AIRE WOOSH");
                if(transform.childCount == 3){
                    GameObject temp = Instantiate(windParticles,transform).gameObject;
                    temp.GetComponent<ParticleSystem>().Play();
                    Destroy(temp, 5f);
                    }
                
                return;
            case Element.Lightning:
                Debug.Log("DANO ELECTRICIDAD ZAP");
                if(transform.childCount == 3){
                    GameObject temp = Instantiate(lightningParticles,transform).gameObject;
                                    temp.GetComponent<ParticleSystem>().Play();
                    Destroy(temp, 5f);
                    }

                return;
            case Element.Earth:
                Debug.Log("DANO TIERRA CRACK");
                if(transform.childCount == 3){
                    GameObject temp = Instantiate(earthParticles,transform).gameObject;
                    temp.GetComponent<ParticleSystem>().Play();
                    Destroy(temp, 5f);
                    }
                
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

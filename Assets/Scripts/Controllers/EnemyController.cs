using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour //Tambien crear aca un interface para que el EnemyManager pueda llamar correctamente todos los metodos correctos
{
    public EnemyData enemyData;

    // Start is called before the first frame update
    void Start()
    {
        ChangeColorOnElement();
    }
    public void Highlight()
    {
        print("Enemy Highlighted");
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
    void Update()
    {
        Vector3 playerPos = GameManager.instance.playerObject.transform.position;

        Vector3 directionVector = playerPos - transform.position;

        transform.Translate(directionVector.normalized * enemyData.speedMod * Time.deltaTime);
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
}

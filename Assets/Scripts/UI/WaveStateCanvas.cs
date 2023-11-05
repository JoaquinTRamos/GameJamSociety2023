using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStateCanvas : MonoBehaviour
{

    void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("StartWave");
    }

    public void Disable()
    {
        StartCoroutine(WaitForSeconds(2.0f));
    }

    IEnumerator WaitForSeconds(float t)
    {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
    }
}

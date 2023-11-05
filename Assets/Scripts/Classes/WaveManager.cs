using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();

    // Start is called before the first frame update
    public Wave GetWave(int index)
    {
        return waves[index];
    }

    public void GenerateWave()
    {

    }
}

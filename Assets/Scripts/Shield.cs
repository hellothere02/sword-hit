using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] GameObject helmet;
    [SerializeField] GameObject[] swords;

    private void Awake()
    {
        int randH = Random.Range(0, 4);
        int randS = Random.Range(1, 4);
        if (randH >= 3)
        {
            helmet.SetActive(true);
        }
        for (int i = 0; i < randS; i++)
        {
            swords[i].SetActive(true);
        }
    }
}

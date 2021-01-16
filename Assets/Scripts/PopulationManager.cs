using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulationManager : MonoBehaviour
{
    [SerializeField] TMP_Text populationText = null;
    [SerializeField] public int currentPopulation = 0;

    void Start()
    {
        
    }

    void Update()
    {
        populationText.text = $"Population {currentPopulation}";
    }
}

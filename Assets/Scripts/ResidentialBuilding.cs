using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentialBuilding : MonoBehaviour
{
    [SerializeField] private int buildingCost = 10;
    [SerializeField] private int residents = 3;

    private MoneyManager moneyManager;
    private PopulationManager populationManager;

    public bool isPlaced = false;

    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        populationManager = FindObjectOfType<PopulationManager>();

        if (isPlaced)
        {
            populationManager.currentPopulation += residents;
        }
    }

    void Update()
    {

    }

    
}

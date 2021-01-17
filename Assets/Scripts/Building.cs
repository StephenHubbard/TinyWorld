using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int buildingCost = 10;
    [SerializeField] private int generateDollarsPerSecond = 1;
    [SerializeField] private int residents = 0;
    [SerializeField] private bool isCommericial = false;
    [SerializeField] private int polutionLevel = 0;
    [SerializeField] private int workersNeeded = 0;

    private MoneyManager moneyManager;
    private PopulationManager populationManager;

    public bool isPlaced = false;

    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        populationManager = FindObjectOfType<PopulationManager>();

        if (isPlaced && isCommericial)
        {
            StartCoroutine(generateMoney());
        }
        else if (isPlaced && !isCommericial)
        {
            populationManager.currentPopulation += residents;
        }
    }

    public int getCost()
    {
        return buildingCost;
    }

    public int getWorkersNeeded()
    {
        return workersNeeded;
    }

    private IEnumerator generateMoney()
    {
        yield return new WaitForSeconds(1f);
        moneyManager.currentDollars += generateDollarsPerSecond;
        StartCoroutine(generateMoney());
    }
}

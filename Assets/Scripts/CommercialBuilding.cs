using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommercialBuilding : MonoBehaviour
{
    [SerializeField] private int buildingCost = 10;
    [SerializeField] private int generateDollarsPerSecond = 1;

    private MoneyManager moneyManager;

    public bool isPlaced = false;


    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();

        if (isPlaced)
        {
            StartCoroutine(generateMoney());
        }
    }

    void Update()
    {
        
    }

    private IEnumerator generateMoney()
    {
        yield return new WaitForSeconds(1f);
        moneyManager.currentDollars += generateDollarsPerSecond;
        StartCoroutine(generateMoney());
    }
}

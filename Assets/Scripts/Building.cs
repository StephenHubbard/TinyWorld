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
    [SerializeField] private GameObject citizenPrefab = null;
    [SerializeField] private Transform residentSpawnPoint = null;
    [SerializeField] public GameObject sphereDetection = null;
    [SerializeField] private int detectionRadius = 50;

    [SerializeField] public List<GameObject> buildingsInSphereDetection = new List<GameObject>();

    private MoneyManager moneyManager;
    private PopulationManager populationManager;

    public bool isPlaced = false;

    

    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        populationManager = FindObjectOfType<PopulationManager>();

        alterSphereDetectionSize();

        if (isPlaced && isCommericial)
        {
            StartCoroutine(generateMoney());
        }
        else if (isPlaced && !isCommericial)
        {
            populationManager.currentPopulation += residents;
        }

        if (isPlaced && !isCommericial)
        {
            SpawnResidents();
        }

    }

    private void Update()
    {
    }

    public void addObjectToList(GameObject go)
    {
        buildingsInSphereDetection.Add(go);
    }

    public void removeObjectFromList(GameObject go)
    {
        buildingsInSphereDetection.Remove(go);
    }

    private void alterSphereDetectionSize()
    {
        sphereDetection.transform.localScale = new Vector3(detectionRadius, detectionRadius / 2, detectionRadius);
    }

    public void toggleSphereDetectionGO()
    {
        sphereDetection.SetActive(!sphereDetection.activeInHierarchy);
    }

    private void SpawnResidents()
    {
        for (int i = 0; i < residents; i++)
        {
            GameObject newCitizen = Instantiate(citizenPrefab, residentSpawnPoint.position, transform.rotation);

            newCitizen.transform.parent = transform;
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

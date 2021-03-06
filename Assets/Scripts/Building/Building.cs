﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] public TMP_Text scoreText = null;
    [SerializeField] private int detectionRadius = 50;

    [SerializeField] public int points = 0;
    [SerializeField] public int pointsOfficial = 0;


    [SerializeField] public List<GameObject> buildingsInSphereDetection = new List<GameObject>();

    //[SerializeField] private int residentialNearby = 0;
    //[SerializeField] private int commercialNearby = 0;

    private PopulationManager populationManager;

    public bool isPlaced = false;
    public bool canPlace = true;

    

    void Start()
    {
        populationManager = FindObjectOfType<PopulationManager>();

        alterSphereDetectionSize();

        
        if (isPlaced && !isCommericial)
        {
            populationManager.currentPopulation += residents;
        }

        if (isPlaced && !isCommericial)
        {
            SpawnResidents();
        }

        StartCoroutine(UpdateBuildingsCounts());
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Building>() || collision.gameObject.GetComponent<Environment>())
        {
            canPlace = false;
        }
        else
        {
            canPlace = true;
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<Building>() || collision.gameObject.GetComponent<Environment>())
    //    {
    //        canPlace = true;
    //    }
    //}


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

    public int GetCost()
    {
        return buildingCost;
    }

    public int GetWorkersNeeded()
    {
        return workersNeeded;
    }

    

    private IEnumerator UpdateBuildingsCounts()
    {
        yield return new WaitForSeconds(1f);
        //UpdateResidentialBuildings();
        //UpdateCommericialBuildings();
        StartCoroutine(UpdateBuildingsCounts());
    }

    //private void UpdateResidentialBuildings()
    //{
    //    residentialNearby = 0;

    //    for (int i = 0; i < buildingsInSphereDetection.Count; i++)
    //    {
    //        if (buildingsInSphereDetection[i] == null)
    //        {
    //            buildingsInSphereDetection.Remove(buildingsInSphereDetection[i]);
    //        }
    //    }

    //    foreach (var building in buildingsInSphereDetection)
    //    {
    //        if (building.GetComponent<Building>().isCommericial == false)
    //        {
    //            residentialNearby++;
    //        }
    //    }
    //}

    //private void UpdateCommericialBuildings()
    //{
    //    commercialNearby = 0;

    //    for (int i = 0; i < buildingsInSphereDetection.Count; i++)
    //    {
    //        if (buildingsInSphereDetection[i] == null)
    //        {
    //            buildingsInSphereDetection.Remove(buildingsInSphereDetection[i]);
    //        }
    //    }

    //    foreach (var building in buildingsInSphereDetection)
    //    {
    //        if (building.GetComponent<Building>().isCommericial == true)
    //        {
    //            commercialNearby++;
    //        }
    //    }
    //}
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject buildingPrefab = null;
    [SerializeField] private LayerMask planetMask = new LayerMask();
    [SerializeField] private LayerMask buildingMask = new LayerMask();

    //[SerializeField] private List<GameObject> buildingsInSphereDetectionCurrent = new List<GameObject>();

    private Quaternion rendererRotation;

    private GameObject buildingPreviewInstance;
    private Camera mainCamera;

    private MoneyManager moneyManager;
    private PopulationManager populationManager;
    private PointsManager pointsManager;

    void Start()
    {
        mainCamera = Camera.main;

        moneyManager = FindObjectOfType<MoneyManager>();
        populationManager = FindObjectOfType<PopulationManager>();
        pointsManager = FindObjectOfType<PointsManager>();
    }

    void Update()
    {
        if (buildingPreviewInstance)
        {
            UpdateBuildingPreview();

            UpdatePointText();

            UpdateOtherBuildingsPointTempText();
        }
    }

    private void UpdateOtherBuildingsPointTempText()
    {
        var buildingsInSphereDetection = buildingPreviewInstance.GetComponent<Building>().buildingsInSphereDetection;

        foreach (var building in buildingsInSphereDetection)
        {
            int currentPoints = building.GetComponent<Building>().pointsOfficial;

            int newPoints = building.GetComponent<Building>().points;

            var thisBuilding = building.GetComponent<Building>();

            thisBuilding.scoreText.gameObject.SetActive(true);

            thisBuilding.scoreText.text = thisBuilding.points.ToString();

            int newPointsDifference = Mathf.Abs(thisBuilding.points - thisBuilding.pointsOfficial);

            if (currentPoints < newPoints)
            {
                thisBuilding.scoreText.text = $"+{(newPointsDifference).ToString()}";
                thisBuilding.scoreText.color = Color.green;
            }
            else if (currentPoints > newPoints)
            {
                thisBuilding.scoreText.text = $"-{(newPointsDifference).ToString()}";
                thisBuilding.scoreText.color = Color.red;
            }
        }
    }

    private void UpdatePointText()
    {
        buildingPreviewInstance.GetComponent<Building>().scoreText.text = (buildingPreviewInstance.GetComponent<Building>().points).ToString();
    }

    private void UpdateBuildingPreview()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, planetMask))
        {
            return;
        }

        buildingPreviewInstance.transform.position = hit.point;

        RotateBuilding();

        buildingPreviewInstance.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);


        Transform rendererTransform = buildingPreviewInstance.transform.Find("Renderer");

        if (TryPlaceBuilding())
        {
            foreach (Transform child in rendererTransform)
            {
                child.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }

        }
        else
        {
            foreach (Transform child in rendererTransform)
            {
                child.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
        }

        if (!buildingPreviewInstance.activeSelf)
        {
            buildingPreviewInstance.SetActive(true);
        }


    }

    private bool TryPlaceBuilding()
    {

        if (buildingPreviewInstance.GetComponent<Building>().canPlace)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RotateBuilding()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            var thisRenderer = buildingPreviewInstance.transform.Find("Renderer");
            thisRenderer.transform.Rotate(0, 100 * Time.deltaTime, 0);
            //rendererRotation = new Vector3(thisRenderer.transform.rotation.eulerAngles.x, thisRenderer.transform.rotation.eulerAngles.y, thisRenderer.transform.rotation.eulerAngles.z);
        }
        if (Input.GetKey(KeyCode.E))
        {
            var thisRenderer = buildingPreviewInstance.transform.Find("Renderer");
            thisRenderer.transform.Rotate(0, -100 * Time.deltaTime, 0);
            //rendererRotation = new Vector3(thisRenderer.transform.rotation.eulerAngles.x, thisRenderer.transform.rotation.eulerAngles.y, thisRenderer.transform.rotation.eulerAngles.z);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }

        buildingPreviewInstance = Instantiate(buildingPrefab);

        if (moneyManager.currentDollars < buildingPreviewInstance.GetComponent<Building>().GetCost())
        {
            print("not enough money");
            Destroy(buildingPreviewInstance);
        }

        if (populationManager.currentPopulation < buildingPreviewInstance.GetComponent<Building>().GetWorkersNeeded())
        {
            print("not enough workers");
            Destroy(buildingPreviewInstance);
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buildingPreviewInstance == null) { return; }


        GameObject newBuilding = null;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, planetMask))
        {
            newBuilding = InstantiateNewBuilding(newBuilding, hit);
        }

        print("hit");

        Destroy(buildingPreviewInstance);

        pointsManager.CalculateTotalBuildings();


        CleanUpBuildingScoreText();
    }

    private GameObject InstantiateNewBuilding(GameObject newBuilding, RaycastHit hit)
    {
        if (TryPlaceBuilding())
        {
            newBuilding = Instantiate(buildingPrefab, hit.point, Quaternion.identity);

            newBuilding.transform.rotation = buildingPreviewInstance.transform.rotation;
        }
        else
        {
            Destroy(buildingPreviewInstance);
        }

        if (newBuilding)
        {
            newBuilding.GetComponent<Building>().isPlaced = true;

            newBuilding.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);

            BuyBuilding(newBuilding);

            UseResidents(newBuilding);

            newBuilding.GetComponent<Building>().sphereDetection.GetComponent<MeshRenderer>().enabled = false;

            newBuilding.GetComponent<Building>().scoreText.gameObject.SetActive(false);

            foreach (var building in buildingPreviewInstance.GetComponent<Building>().buildingsInSphereDetection)
            {
                building.GetComponent<Building>().pointsOfficial = building.GetComponent<Building>().points;
            }

            newBuilding.GetComponent<Building>().pointsOfficial = buildingPreviewInstance.GetComponent<Building>().points;



        }



        return newBuilding;
    }

    private void CleanUpBuildingScoreText()
    {
        var buildingsInSphereDetection = buildingPreviewInstance.GetComponent<Building>().buildingsInSphereDetection;

        foreach (var building in buildingsInSphereDetection)
        {
            var thisBuilding = building.GetComponent<Building>();

            thisBuilding.scoreText.gameObject.SetActive(false);

        }

        buildingPreviewInstance = null;
    }

    private void UseResidents(GameObject newBuilding)
    {
        populationManager.currentPopulation -= newBuilding.GetComponent<Building>().GetWorkersNeeded();
    }

    private void BuyBuilding(GameObject newBuilding)
    {
        moneyManager.currentDollars -= newBuilding.GetComponent<Building>().GetCost();

    }

    

}

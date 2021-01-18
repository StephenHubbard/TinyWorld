using System;
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
    [SerializeField] private TMP_Text scoreText = null;

    [SerializeField] private List<GameObject> buildingsInSphereDetectionCurrent = new List<GameObject>();

    private Vector3 rendererRotation;

    private GameObject buildingPreviewInstance;
    private Camera mainCamera;

    private MoneyManager moneyManager;
    private PopulationManager populationManager;

    void Start()
    {
        mainCamera = Camera.main;

        moneyManager = FindObjectOfType<MoneyManager>();
        populationManager = FindObjectOfType<PopulationManager>();

    }

    void Update()
    {

        if (buildingPreviewInstance == null) { return; }

        UpdateBuildingPreview();

        UpdatePointText();
    }

    private void UpdatePointText()
    {
        if (scoreText)
        {
            scoreText.text = buildingsInSphereDetectionCurrent.Count.ToString();
        }
    }

    private void UpdateBuildingPreview()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, planetMask)) { return; }

        buildingPreviewInstance.transform.position = hit.point;

        buildingPreviewInstance.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);


        //if (TryPlaceBuilding(hit.point))
        //{
        //    buildingPreviewInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        //}
        //else
        //{
        //    buildingPreviewInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        //}

        if (!buildingPreviewInstance.activeSelf)
        {
            buildingPreviewInstance.SetActive(true);
        }

        RotateBuilding();

    }

    private void RotateBuilding()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            var thisRenderer = buildingPreviewInstance.transform.Find("Renderer");
            thisRenderer.Rotate(0, 100 * Time.deltaTime, 0);
            rendererRotation = new Vector3(thisRenderer.transform.rotation.eulerAngles.x, thisRenderer.transform.rotation.eulerAngles.y, thisRenderer.transform.rotation.eulerAngles.z);
        }
        if (Input.GetKey(KeyCode.E))
        {
            var thisRenderer = buildingPreviewInstance.transform.Find("Renderer");
            thisRenderer.Rotate(0, -100 * Time.deltaTime, 0);
            rendererRotation = new Vector3(thisRenderer.transform.rotation.eulerAngles.x, thisRenderer.transform.rotation.eulerAngles.y, thisRenderer.transform.rotation.eulerAngles.z);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }

        buildingPreviewInstance = Instantiate(buildingPrefab);

        //scoreText = buildingPreviewInstance.transform.Find("ScoreText").gameObject.GetComponent<TextMeshPro>();

        //buildingPreviewInstance.GetComponent<Building>().toggleSphereDetectionGO();

        
        if (moneyManager.currentDollars < buildingPreviewInstance.GetComponent<Building>().getCost())
        {
            print("not enough money");
            Destroy(buildingPreviewInstance);
        }

        if (populationManager.currentPopulation < buildingPreviewInstance.GetComponent<Building>().getWorkersNeeded())
        {
            print("not enough workers");
            Destroy(buildingPreviewInstance);
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buildingPreviewInstance == null) { return; }

        buildingsInSphereDetectionCurrent = buildingPreviewInstance.GetComponent<Building>().buildingsInSphereDetection;

        GameObject newBuilding = null;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, planetMask))
        {

            if (TryPlaceBuilding(hit.point))
            {
                newBuilding = Instantiate(buildingPrefab, hit.point, Quaternion.identity);

                newBuilding.GetComponent<Building>().buildingsInSphereDetection = buildingsInSphereDetectionCurrent;
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
            }
        }
        Destroy(buildingPreviewInstance);



    }

    private void UseResidents(GameObject newBuilding)
    {
        populationManager.currentPopulation -= newBuilding.GetComponent<Building>().getWorkersNeeded();
    }

    private void BuyBuilding(GameObject newBuilding)
    {
        moneyManager.currentDollars -= newBuilding.GetComponent<Building>().getCost();

    }

    private bool TryPlaceBuilding(Vector3 point)
    {
        BoxCollider buildingCollider = buildingPreviewInstance.GetComponent<BoxCollider>();

        // can't get checkbox to function as intended

        if (Physics.CheckBox(
                    point + buildingCollider.center,
                    buildingCollider.size / 2,
                    Quaternion.identity,
                    planetMask))
        {
            return true;
        }
        else
        {
            return true;
        }
    }

}

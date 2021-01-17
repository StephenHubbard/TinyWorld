using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject buildingPrefab = null;
    [SerializeField] private LayerMask planetMask = new LayerMask();
    [SerializeField] private LayerMask buildingMask = new LayerMask();

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
    }

    private void UpdateBuildingPreview()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, planetMask)) { return; }

        buildingPreviewInstance.transform.position = hit.point;

        buildingPreviewInstance.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);


        if (TryPlaceBuilding(hit.point))
        {
            buildingPreviewInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }
        else
        {
            buildingPreviewInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }



        if (!buildingPreviewInstance.activeSelf)
        {
            buildingPreviewInstance.SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }

        buildingPreviewInstance = Instantiate(buildingPrefab);

        
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


        GameObject newBuilding = null;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, planetMask))
        {

            if (TryPlaceBuilding(hit.point))
            {
                newBuilding = Instantiate(buildingPrefab, hit.point, Quaternion.identity);
            }
            else
            {
                Destroy(buildingPreviewInstance);
            }

            if (newBuilding)
            {
                newBuilding.GetComponent<Building>().isPlaced = true;

                newBuilding.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);

                moneyManager.currentDollars -= newBuilding.GetComponent<Building>().getCost();

                populationManager.currentPopulation -= newBuilding.GetComponent<Building>().getWorkersNeeded();
            }
        }


        Destroy(buildingPreviewInstance);
    }

    private void BuyBuilding()
    {

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject building1Prefab = null;
    [SerializeField] private LayerMask planetMask = new LayerMask();

    private GameObject buildingPreviewInstance;
    private Camera mainCamera;
    

    void Start()
    {
        mainCamera = Camera.main;
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


        if (!buildingPreviewInstance.activeSelf)
        {
            buildingPreviewInstance.SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }

        buildingPreviewInstance = Instantiate(building1Prefab);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buildingPreviewInstance == null) { return; }



        GameObject newBuilding = null;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, planetMask))
        {
            newBuilding = Instantiate(building1Prefab, hit.point, Quaternion.identity);

            if (newBuilding.GetComponent<ResidentialBuilding>())
            {
                newBuilding.GetComponent<ResidentialBuilding>().isPlaced = true;
            }
            else if (newBuilding.GetComponent<CommercialBuilding>())
            {
                newBuilding.GetComponent<CommercialBuilding>().isPlaced = true;
            }
        }

        newBuilding.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);

        Destroy(buildingPreviewInstance);

    }
}

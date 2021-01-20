using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleNewBuildings : MonoBehaviour
{
    [SerializeField] private GameObject[] buildingButtonPrefabs = null;
    [SerializeField] private Scrollbar scrollbar = null;

    private void Awake()
    {
        
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnNewButton();
        }

        scrollbar.value = 1;
    }

    public void SpawnNewButton()
    {
        int randomNum = Random.Range(0, buildingButtonPrefabs.Length);

        GameObject newBuildingButton =  Instantiate(buildingButtonPrefabs[randomNum], transform.position, transform.rotation);

        newBuildingButton.transform.parent = transform;

        scrollbar.value = 1;
    }
}

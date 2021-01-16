using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetGen : MonoBehaviour
{
    [SerializeField] GameObject planet;

    [SerializeField] private LayerMask planetMask = new LayerMask();

    [SerializeField] GameObject tree;
    [SerializeField] GameObject plateau;
    [SerializeField] GameObject volcano;

    [SerializeField] private int amountOfTrees = 20;
    [SerializeField] private int amountOfPlateaus = 20;
    [SerializeField] private int amountOfVolcano = 20;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        SpawnPlateaus();
        SpawnTrees();
        SpawnVolcanoes();
    }

    private void SpawnTrees()
    {
        for (int i = 0; i < amountOfTrees; i++)
        {
            Vector3 randomPointOnSphere = Random.onUnitSphere * ((planet.transform.localScale.x / 2) + 1f);

            GameObject newTree = Instantiate(tree, randomPointOnSphere, transform.rotation);

            RaycastHit hit;

            if (Physics.Raycast(newTree.transform.position, planet.transform.position - newTree.transform.position, out hit, planetMask))
            {
                newTree.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);

            }


            newTree.transform.parent = planet.transform;

            newTree.transform.Translate(Vector3.down * 1.2f, Space.Self);

        }
    }

    private void SpawnPlateaus()
    {
        for (int i = 0; i < amountOfPlateaus; i++)
        {
            Vector3 randomPointOnSphere = Random.onUnitSphere * ((planet.transform.localScale.x / 2) + 1f);

            GameObject newPlateau = Instantiate(plateau, randomPointOnSphere, transform.rotation);

            RaycastHit hit;

            if (Physics.Raycast(newPlateau.transform.position, planet.transform.position - newPlateau.transform.position, out hit, planetMask))
            {
                newPlateau.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);

            }


            newPlateau.transform.parent = planet.transform;

            newPlateau.transform.Translate(Vector3.down * 1.3f, Space.Self);

        }
    }

    private void SpawnVolcanoes()
    {
        for (int i = 0; i < amountOfVolcano; i++)
        {
            Vector3 randomPointOnSphere = Random.onUnitSphere * ((planet.transform.localScale.x / 2) + 1f);

            GameObject newVolcano = Instantiate(volcano, randomPointOnSphere, transform.rotation);

            RaycastHit hit;

            if (Physics.Raycast(newVolcano.transform.position, planet.transform.position - newVolcano.transform.position, out hit, planetMask))
            {
                newVolcano.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);

            }

            newVolcano.transform.parent = planet.transform;

            newVolcano.transform.Translate(Vector3.down * 1.3f, Space.Self);

        }
    }

    void Update()
    {

    }
}

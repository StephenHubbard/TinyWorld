using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetGen : MonoBehaviour
{
    [SerializeField] GameObject planet;
    [SerializeField] GameObject planetCore;

    [SerializeField] private LayerMask planetMask = new LayerMask();
    [SerializeField] private LayerMask planetCoreMask = new LayerMask();

    [SerializeField] GameObject[] tree;
    [SerializeField] GameObject[] plateau;
    [SerializeField] GameObject volcano;

    [SerializeField] private int amountOfTrees = 20;
    [SerializeField] private int amountOfPlateaus = 20;
    [SerializeField] private int amountOfVolcano = 20;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        SpawnPlateaus();
        SpawnVolcanoes();
        SpawnTrees();
    }

    private void SpawnTrees()
    {
        for (int i = 0; i < amountOfTrees; i++)
        {
            Vector3 randomPointOnSphere = Random.onUnitSphere * ((planet.transform.localScale.x / 2));

            GameObject newTree = Instantiate(tree[Random.Range(0, tree.Length)], randomPointOnSphere, transform.rotation);

            RaycastHit hit;

            if (Physics.Raycast(newTree.transform.position, planetCore.transform.position - newTree.transform.position, out hit, planetCoreMask, ~planetMask))
            {
                newTree.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);

            }

            newTree.transform.parent = planet.transform;

            newTree.transform.Translate(Vector3.down * .2f, Space.Self);
        }
    }

    private void SpawnPlateaus()
    {
        for (int i = 0; i < amountOfPlateaus; i++)
        {
            Vector3 randomPointOnSphere = Random.onUnitSphere * ((planet.transform.localScale.x / 2));

            GameObject newPlateau = Instantiate(plateau[Random.Range(0, plateau.Length)], randomPointOnSphere, transform.rotation);

            RaycastHit hit;

            if (Physics.Raycast(newPlateau.transform.position, planetCore.transform.position - newPlateau.transform.position, out hit, planetCoreMask, ~planetMask))
            {
                newPlateau.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);
                newPlateau.transform.Rotate(0f, Random.Range(0f, 90f), 0f, Space.Self);
            }

            newPlateau.transform.parent = planet.transform;

            newPlateau.transform.Translate(Vector3.down * .3f, Space.Self);

        }
    }

    private void SpawnVolcanoes()
    {
        for (int i = 0; i < amountOfVolcano; i++)
        {
            Vector3 randomPointOnSphere = Random.onUnitSphere * ((planet.transform.localScale.x / 2));

            GameObject newVolcano = Instantiate(volcano, randomPointOnSphere, transform.rotation);

            RaycastHit hit;

            if (Physics.Raycast(newVolcano.transform.position, planetCore.transform.position - newVolcano.transform.position, out hit, planetCoreMask, ~planetMask))
            {
                newVolcano.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);

            }

            newVolcano.transform.parent = planet.transform;

            newVolcano.transform.Translate(Vector3.down * .2f, Space.Self);

        }
    }

    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDetection : MonoBehaviour
{
    [SerializeField] private Building myBuilding;

    private void OnEnable()
    {
        myBuilding = GetComponentInParent<Building>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<Building>())
        {

            myBuilding.addObjectToList(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Building>())
        {
            myBuilding.removeObjectFromList(other.gameObject);

            other.GetComponent<Building>().scoreText.gameObject.SetActive(false);
        }
    }
}

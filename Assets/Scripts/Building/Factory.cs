using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private Building building;

    private void Start()
    {
        building = GetComponent<Building>();
    }

    private void Update()
    {
        calcPoints();
    }

    private void calcPoints()
    {
        int calcPoints = 0;

        foreach (var building in building.buildingsInSphereDetection)
        {
            if (building != null)
            {
                if (building.GetComponent<House>())
                {
                    calcPoints -= 2;
                }
                if (building.GetComponent<Factory>())
                {
                    calcPoints += 1;
                }
                if (building.GetComponent<Commercial>())
                {
                    calcPoints += 1;
                }
                if (building.GetComponent<CityHall>())
                {
                    calcPoints += 1;
                }
                if (building.GetComponent<Apartment>())
                {
                    calcPoints -= 1;
                }
            }
        }

        building.points = calcPoints;
    }
}

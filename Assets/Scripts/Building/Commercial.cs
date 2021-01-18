using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commercial : MonoBehaviour
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
                    calcPoints += 1;
                }
                if (building.GetComponent<Factory>())
                {
                    calcPoints -= 3;
                }
                if (building.GetComponent<Commercial>())
                {
                    calcPoints += 1;
                }
                if (building.GetComponent<CityHall>())
                {
                    calcPoints += 5;
                }
                if (building.GetComponent<Apartment>())
                {
                    calcPoints += 2;
                }
            }
        }

        building.points = calcPoints;
    }
}

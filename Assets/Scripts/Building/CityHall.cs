using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHall : MonoBehaviour
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
                    calcPoints += 0;
                }
                if (building.GetComponent<Factory>())
                {
                    calcPoints -= 0;
                }
                if (building.GetComponent<Commercial>())
                {
                    calcPoints += 0;
                }
                if (building.GetComponent<CityHall>())
                {
                    calcPoints += 0;
                }
                if (building.GetComponent<Apartment>())
                {
                    calcPoints += 0;
                }
            }
        }

        building.points = calcPoints;
    }
}

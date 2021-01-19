using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PointsManager : MonoBehaviour
{
    [SerializeField] TMP_Text pointsTexts = null;
    [SerializeField] public int currentPoints = 0;

    void Start()
    {

    }

    void Update()
    {
        pointsTexts.text = $"Points: {currentPoints}";
    }

    public void CalculatePoints()
    {
        int calcPoints = 0;

        Building[] allBuildings = FindObjectsOfType<Building>();

        foreach (var building in allBuildings)
        {
            calcPoints += building.pointsOfficial;
        }

        currentPoints = calcPoints;
    }

    
}

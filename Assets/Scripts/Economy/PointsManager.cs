using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class PointsManager : MonoBehaviour
{
    [SerializeField] TMP_Text pointsTexts = null;
    [SerializeField] public int currentPoints = 0;

    [SerializeField] Building[] allBuildings = null;

    void Start()
    {

    }

    void Update()
    {
        pointsTexts.text = $"Points: {currentPoints}";

        CalculatePoints();

        
    }

    public void CalculateTotalBuildings()
    {
        allBuildings = FindObjectsOfType<Building>();
    }

    public void CalculatePoints()
    {
        int calcPoints = 0;

        foreach (var building in allBuildings)
        {
            calcPoints += building.pointsOfficial;
        }

        currentPoints = calcPoints;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private int currentDay = 0;
    [SerializeField] private float rotateSpeed = 1f;

    [SerializeField] private TMP_Text currentDayText = null;

    float currentDayFloat = 0f;


    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0f, 1f * rotateSpeed * Time.deltaTime, 0f, Space.Self);

        CalculateCurrentDay();

        currentDayText.text = $"Day: {currentDay.ToString()}";

    }

    private void CalculateCurrentDay()
    {
        currentDayFloat += Time.deltaTime / (360 / rotateSpeed);

        currentDay = Mathf.FloorToInt(currentDayFloat);
    }
}

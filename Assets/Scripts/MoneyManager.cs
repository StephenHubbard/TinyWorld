using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] TMP_Text dollarsText = null;
    [SerializeField] public int currentDollars;

    
    void Start()
    {
        
    }

    void Update()
    {
        dollarsText.text = $"Dollars: {currentDollars}";
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageController : MonoBehaviour
{
    // Start is called before the first frame update

    public int MyLevel = 1;
    public int stacksPerLevel;
    public StackCounterUI stackCounter;
    public int expPerLevel;

    public static StageController instance;

    public static Action<int> OnStackNumberChanged;
    public TextMeshProUGUI MaxLimitText;
    private int CurrentStack;

    public int currentExp;

     void Awake()
     {
        instance = this; 
     }

    private void OnStackChanged(int quant)
    {
        stackCounter.UpdateUI(GetCurrentMaxStacks(), quant, MyLevel, (expPerLevel*MyLevel) - currentExp);
        CurrentStack = quant;
    }


    void Start()
    {
        stackCounter.UpdateUI(GetCurrentMaxStacks(), 0, MyLevel, (expPerLevel * MyLevel) - currentExp);
        OnStackNumberChanged += OnStackChanged;
    }

    private void OnDestroy()
    {
        OnStackNumberChanged -= OnStackChanged;
    }

    private int GetCurrentMaxStacks() => MyLevel * stacksPerLevel;
    public bool CanAddStack() =>  CurrentStack < GetCurrentMaxStacks();
    
    public void ShowMaxStackMessage()
    {
        MaxLimitText.gameObject.SetActive(true);
    }

    public void AddExp(int exp)
    {
        currentExp += exp;
        if(currentExp >= (expPerLevel * MyLevel))
        {
            MyLevel++;
            currentExp = 0;
        }
        stackCounter.UpdateUI(GetCurrentMaxStacks(), CurrentStack, MyLevel, (expPerLevel * MyLevel) - currentExp);
    }
   
}

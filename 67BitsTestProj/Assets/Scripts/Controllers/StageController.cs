using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public int MyLevel = 1;
    public int stacksPerLevel;
    public StackCounterUI stackCounter;
    public int expPerLevel;
    public Player myPlayer;
    public static StageController instance;

    public static Action<int> OnStackNumberChanged;
    public TextMeshProUGUI MaxLimitText;
    private int CurrentStack;

    public int currentExp;

    public const string coinsSavePath = "CoinsSavePath";
    private const string levelSavePath = "LevelSavePath";
    private const string expSavePath = "ExpSavePath";

    void Awake()
    {
        instance = this;
        LoadPlayerData(); // Carrega o nível e a experiência do PlayerPrefs
    }

    private void LoadPlayerData()
    {
        MyLevel = PlayerPrefs.GetInt(levelSavePath, 1);
        currentExp = PlayerPrefs.GetInt(expSavePath, 0);
    }

    private void OnStackChanged(int quant)
    {
        stackCounter.UpdateUI(GetCurrentMaxStacks(), quant, MyLevel, (expPerLevel * MyLevel) - currentExp, expPerLevel * MyLevel);
        CurrentStack = quant;
    }

    void Start()
    {
        stackCounter.UpdateUI(GetCurrentMaxStacks(), 0, MyLevel, (expPerLevel * MyLevel) - currentExp, expPerLevel * MyLevel);
        OnStackNumberChanged += OnStackChanged;
        stackCounter.UpdateCoins(GetCoins());
    }

    private void OnDestroy()
    {
        OnStackNumberChanged -= OnStackChanged;
    }

    private int GetCurrentMaxStacks() => MyLevel * stacksPerLevel;
    public bool CanAddStack() => CurrentStack < GetCurrentMaxStacks();

    public void ShowMaxStackMessage()
    {
        MaxLimitText.gameObject.SetActive(true);
    }

    public void AddExp(int exp)
    {
        currentExp += exp;
        if (currentExp >= (expPerLevel * MyLevel))
        {
            SoundManager.Instance.PlaySound("levelUp");
            MyLevel++;
            myPlayer.LevelUpAnimation();
            currentExp = 0;
            SavePlayerData(); // Salva nível e experiência atualizados
        }
        else
        {
            SaveExp(); // Salva somente a experiência se o nível não foi alterado
        }

        stackCounter.UpdateUI(GetCurrentMaxStacks(), CurrentStack, MyLevel, (expPerLevel * MyLevel) - currentExp, expPerLevel * MyLevel);
    }

    private void SavePlayerData()
    {
        PlayerPrefs.SetInt(levelSavePath, MyLevel);
        PlayerPrefs.SetInt(expSavePath, currentExp);
    }

    private void SaveExp()
    {
        PlayerPrefs.SetInt(expSavePath, currentExp);
    }

    public void AddCoins()
    {
        int quantCurrent = GetCoins();
        GameObject clone = ObjectPoolManager.Instance.SpawnFromPool("gainCoin");
        clone.gameObject.SetActive(true);
        quantCurrent++;
        SaveCoins(quantCurrent);
        stackCounter.UpdateCoins(quantCurrent);
    }

    public void RemoveCoins(int quant)
    {
        int quantCurrent = GetCoins();
        quantCurrent -= quant;
        if (quantCurrent < 0)
            quantCurrent = 0;
        SaveCoins(quantCurrent);
        stackCounter.UpdateCoins(quantCurrent);
    }

    public int GetCoins()
    {
        return PlayerPrefs.GetInt(coinsSavePath, 0);
    }

    private void SaveCoins(int current)
    {
        PlayerPrefs.SetInt(coinsSavePath, current);
    }
}

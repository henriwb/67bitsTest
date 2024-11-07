using System;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private int MyLevel = 1;
    [SerializeField] private int stacksPerLevel;
    [SerializeField] private int expPerLevel;
    [SerializeField] private int currentExp;
    [SerializeField] private int CurrentStack;

    [SerializeField] private StageUI UIControler;
    [SerializeField] private Player myPlayer;
    [SerializeField] private GameObject LevelUpMessage;
    [SerializeField] private GameObject MaxLimitText;
    [SerializeField] private GameObject BuildCompleteMessage;

    public static StageController instance;
    public static Action<int> OnStackNumberChanged;
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
        CurrentStack = quant;
        UpdateUI();
    }

    void Start()
    {
        UpdateUI();
        OnStackNumberChanged += OnStackChanged;
        UIControler.UpdateCoins(GetCoins());
    }

    private void OnDestroy() => OnStackNumberChanged -= OnStackChanged;
    private int GetCurrentMaxStacks() => MyLevel * stacksPerLevel;
    public bool CanAddStack() => CurrentStack < GetCurrentMaxStacks();
    public void ShowMaxStackMessage() => MaxLimitText.gameObject.SetActive(true);

    public void ShowBuildCompleteMessage() => BuildCompleteMessage.gameObject.SetActive(true);

    public void AddExp(int exp)
    {
        currentExp += exp;
        if (currentExp > (expPerLevel * MyLevel))
        {
            
            LevelUpAction();
            return;
        }
        else
        {
            SaveExp(); // Salva somente a experiência se o nível não foi alterado
        }

        UpdateUI();
    }

    private void UpdateUI() => UIControler.UpdateUI(GetCurrentMaxStacks(), CurrentStack, MyLevel, (expPerLevel * MyLevel) - currentExp, expPerLevel * MyLevel);

    private void LevelUpAction()
    {
        SoundManager.Instance.PlaySound("levelUp");
        MyLevel++;
        myPlayer.LevelUpAnimation();
        LevelUpMessage.SetActive(true);
        currentExp = 0;
        UIControler.ResetExpBar();
        UpdateUI();
        SavePlayerData(); // Salva nível e experiência atualizados

    }

    private void SavePlayerData()
    {
        PlayerPrefs.SetInt(levelSavePath, MyLevel);
        PlayerPrefs.SetInt(expSavePath, currentExp);
    }

    private void SaveExp() => PlayerPrefs.SetInt(expSavePath, currentExp);

    public void AddCoins()
    {
        int quantCurrent = GetCoins();
        GameObject clone = ObjectPoolManager.Instance.SpawnFromPool("gainCoin");
        clone.gameObject.SetActive(true);
        quantCurrent++;
        GameObject cloneCoin = ObjectPoolManager.Instance.SpawnFromPool("addCoin");
        cloneCoin.gameObject.SetActive(true);
        SaveCoins(quantCurrent);
        UIControler.UpdateCoins(quantCurrent);
    }

    public void RemoveCoins(int quant)
    {
        int quantCurrent = GetCoins();
        quantCurrent -= quant;
        if (quantCurrent < 0)
            quantCurrent = 0;
        SaveCoins(quantCurrent);
        UIControler.UpdateCoins(quantCurrent);
    }

    public int GetCoins() => PlayerPrefs.GetInt(coinsSavePath, 0);

    private void SaveCoins(int current) => PlayerPrefs.SetInt(coinsSavePath, current);
    
}

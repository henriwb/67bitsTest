using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildStation : BaseStation
{
    public static bool Receiving;

    [SerializeField] private int targetToComplete;
    [SerializeField] private Image completionBar;
    public UnityEvent OnTargetComplete;

    private int currentAmount;
    private string savePath;
    private bool isCompleted;

    private void Start()
    {
        savePath = transform.parent.name;
        currentAmount = PlayerPrefs.GetInt(savePath, 0);
        isCompleted = currentAmount >= targetToComplete;

        UpdateSlider();

        if (isCompleted)
        {
            OnTargetComplete?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public override void ReceiverInit(Player who)
    {
        if (isCompleted || Receiving)
        {
            return;
        }

        if (StageController.instance.GetCoins() < coinsConsumed)
        {
            return;
        }

        Receiving = true;
        StartCoroutine(ReceiverRoutine(who));
    }

    private IEnumerator ReceiverRoutine(Player who)
    {
        while (StageController.instance.GetCoins() >= coinsConsumed)
        {
            StageController.instance.RemoveCoins(coinsConsumed);
            SoundManager.Instance.PlaySound("pickupCoin", 0.9f);
            GameObject clone = ObjectPoolManager.Instance.SpawnFromPool("spentCoin");
            clone.gameObject.SetActive(true);

            currentAmount++;
            PlayerPrefs.SetInt(savePath, currentAmount);

            UpdateSlider();

            if (currentAmount >= targetToComplete)
            {
                StageController.instance.ShowBuildCompleteMessage();
                isCompleted = true;
                OnTargetComplete?.Invoke();
                gameObject.SetActive(false);
                break;
            }

            yield return new WaitForSeconds(0.15f);
        }

        Receiving = false;
    }

    private void UpdateSlider()
    {
        completionBar.fillAmount = (float)currentAmount / targetToComplete;
    }
}

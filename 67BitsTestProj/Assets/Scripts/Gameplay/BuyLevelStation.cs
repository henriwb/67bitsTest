using System.Collections;
using UnityEngine;

public class BuyLevelStation : BaseStation
{
    public static bool Receiving;

    public override void ReceiverInit(Player who)
    {
        if (Receiving)
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

    IEnumerator ReceiverRoutine(Player who)
    {
        while (StageController.instance.GetCoins() >= coinsConsumed)
        {
            StageController.instance.RemoveCoins(coinsConsumed);
            SoundManager.Instance.PlaySound("pickupCoin", 0.9f);
            GameObject clone = ObjectPoolManager.Instance.SpawnFromPool("spentCoin");
            clone.gameObject.SetActive(true);
            StageController.instance.AddExp(1);
            yield return new WaitForSeconds(0.15f);
        }
        Receiving = false;
    }
}

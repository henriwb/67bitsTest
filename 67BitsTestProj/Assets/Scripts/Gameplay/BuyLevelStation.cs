using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyLevelStation : MonoBehaviour
{
    public static bool Receiving;
    public void ReceiverInit(Player who)
    {
        if(Receiving)
        {
            return;
        }

        if(StageController.instance.GetCoins() <= 0)
        {
            return;
        }

        Receiving = true;
        StartCoroutine(ReceiverRoutine(who));

    }

    IEnumerator ReceiverRoutine(Player who)
    {
        while(StageController.instance.GetCoins() > 0)
        {
            StageController.instance.RemoveCoins(1);
            SoundManager.Instance.PlaySound("pickupCoin", 0.9f);
            GameObject clone = ObjectPoolManager.Instance.SpawnFromPool("spentCoin");
            clone.gameObject.SetActive(true);
            StageController.instance.AddExp(1);
            yield return new WaitForSeconds(0.15f);
        }
        Receiving = false;
    }
}

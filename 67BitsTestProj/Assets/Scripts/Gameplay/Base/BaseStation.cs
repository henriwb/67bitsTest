using System.Collections;
using UnityEngine;

public abstract class BaseStation : MonoBehaviour
{
    [SerializeField] protected int coinsConsumed = 1; // Quantidade de moedas consumidas por operação
    public abstract void ReceiverInit(Player who);
}

using System.Collections;
using UnityEngine;

public abstract class BaseStation : MonoBehaviour
{
    [SerializeField] protected int coinsConsumed = 1; // Quantidade de moedas consumidas por opera��o
    public abstract void ReceiverInit(Player who);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackObjects : MonoBehaviour
{
    public Transform StackParent;
    public float heightStack = 1f; // Altura de cada objeto na pilha
    private List<GameObject> objects = new List<GameObject>();

    public void AddToStack(GameObject newObject)
    {
        newObject.transform.SetParent(StackParent); // Define o StackParent como pai do objeto
        objects.Add( newObject );
        int stackCount = StackParent.childCount-1; // Número de objetos na pilha
        Vector3 newPosition = new Vector3(0, stackCount * heightStack, 0); // Calcula a posição em Y

        newObject.transform.localPosition = newPosition; // Posiciona o novo objeto
    }

    public int GetCurrentStack()
    {
        return objects.Count;
    }
}

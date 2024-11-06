using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackObjects : MonoBehaviour
{
    public Transform StackParent;
    private float heightStack = 0.5f; // Altura de cada objeto na pilha
    private List<GameObject> objects = new List<GameObject>();

    public void AddToStack(GameObject newObject)
    {
        newObject.transform.SetParent(StackParent); // Define o StackParent como pai do objeto
        newObject.transform.localPosition = Vector3.zero;
        objects.Add( newObject );
        //Vector3 newPosition = new Vector3(0, StackParent.transform.position.y + ( objects.Count-1 * heightStack), 0); // Calcula a posição em Y

        newObject.transform.localPosition += new Vector3(0, ((objects.Count-1)  * heightStack), 0);

        int currentStaclk = GetCurrentStack();
        StageController.OnStackNumberChanged(currentStaclk);
    }


    public bool HasOne()
    {
        return objects.Count > 0;
    }

    public int GetCurrentStack()
    {
        return objects.Count;
    }


    public GameObject ReturnLastFromList()
    {
        if(objects.Count == 0 ) return null;

        GameObject who = objects[objects.Count-1];
        objects.Remove(who);
        int currentStaclk = GetCurrentStack();
        StageController.OnStackNumberChanged(currentStaclk);
        return who;

    }
}

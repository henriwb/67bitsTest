using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackObjects : MonoBehaviour
{
    public Transform StackParent;
    public Transform playerTransform;
    public Transform ModelRotation; // Objeto de referência para rotação
    private float heightStack = 0.35f;
    private List<GameObject> objects = new List<GameObject>();
    private List<Vector3> playerPositions = new List<Vector3>();
    private int followDelay = 4; // Ajuste do delay para simular inércia

    void Update()
    {
        // Copia a rotação do ModelRotation para StackParent
        StackParent.rotation = ModelRotation.rotation;

        // Armazena a posição atual do player, mantendo um histórico
        playerPositions.Insert(0, playerTransform.position);

        // Limita o histórico para evitar uso excessivo de memória
        if (playerPositions.Count > objects.Count * followDelay + 1)
            playerPositions.RemoveAt(playerPositions.Count - 1);

        // Atualiza a posição dos objetos empilhados
        for (int i = 0; i < objects.Count; i++)
        {
            int delayIndex = (i + 1) * followDelay;
            if (delayIndex < playerPositions.Count)
            {
                Vector3 targetPosition = playerPositions[delayIndex];
                // Mantém o eixo Y original
                targetPosition.y = StackParent.position.y + i * heightStack;
                objects[i].transform.position = Vector3.Lerp(objects[i].transform.position, targetPosition, 0.1f);
            }
        }
    }

    public void AddToStack(GameObject newObject)
    {
        newObject.transform.SetParent(StackParent);
        newObject.transform.localPosition = Vector3.zero;
        objects.Add(newObject);
        newObject.transform.localPosition += new Vector3(0, (objects.Count - 1) * heightStack, 0);

        int currentStack = GetCurrentStack();
        StageController.OnStackNumberChanged(currentStack);
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
        if (objects.Count == 0) return null;

        GameObject who = objects[objects.Count - 1];
        objects.Remove(who);
        int currentStack = GetCurrentStack();
        StageController.OnStackNumberChanged(currentStack);
        return who;
    }
}

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
    private float springDamping = 0.08f; // Força do efeito de mola
    private Vector3 lastPlayerPosition;
    private bool isPlayerMoving = false;
    private List<Vector3> objectVelocities = new List<Vector3>(); // Velocidade dos objetos para movimento oscilante

    void Start()
    {
        lastPlayerPosition = playerTransform.position;

        // Inicializa as velocidades para cada objeto
        for (int i = 0; i < objects.Count; i++)
        {
            objectVelocities.Add(Vector3.zero);
        }
    }

    void Update()
    {
        // Copia a rotação do ModelRotation para StackParent
        StackParent.rotation = ModelRotation.rotation;

        // Verifica se o player está parado
        isPlayerMoving = (lastPlayerPosition != playerTransform.position);
        lastPlayerPosition = playerTransform.position;

        // Armazena a posição atual do player se ele estiver em movimento
        if (isPlayerMoving)
        {
            playerPositions.Insert(0, playerTransform.position);
        }
        else
        {
            // Mantém a última posição no histórico para o efeito de mola
            playerPositions.Insert(0, lastPlayerPosition);
        }

        // Limita o histórico para evitar uso excessivo de memória
        if (playerPositions.Count > objects.Count * followDelay + 1)
            playerPositions.RemoveAt(playerPositions.Count - 1);

        // Atualiza a posição dos objetos empilhados com efeito de mola e inércia
        for (int i = 0; i < objects.Count; i++)
        {
            int delayIndex = (i + 1) * followDelay;
            Vector3 targetPosition;

            // Usa a posição histórica para aplicar o efeito de mola até centralizar
            if (delayIndex < playerPositions.Count)
            {
                targetPosition = playerPositions[delayIndex];
            }
            else
            {
                // Centraliza no player, permitindo que o objeto ultrapasse o centro
                targetPosition = playerTransform.position;
            }

            // Mantém o eixo Y original e ajusta a altura para empilhamento
            targetPosition.y = StackParent.position.y + i * heightStack;

            // Aplica o efeito de mola usando a velocidade acumulada
            Vector3 currentPosition = objects[i].transform.position;
            Vector3 direction = targetPosition - currentPosition;

            // Ajusta a velocidade do objeto para efeito de mola, permitindo ultrapassar o ponto central
            objectVelocities[i] += direction * springDamping;
            objectVelocities[i] *= 0.9f; // Amortecimento da velocidade para reduzir oscilações gradualmente

            // Atualiza a posição do objeto
            objects[i].transform.position += objectVelocities[i];
        }
    }

    public void AddToStack(GameObject newObject)
    {
        newObject.transform.SetParent(StackParent);
        newObject.transform.localPosition = Vector3.zero;
        objects.Add(newObject);
        objectVelocities.Add(Vector3.zero); // Adiciona a velocidade para o novo objeto
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
        objectVelocities.RemoveAt(objects.Count); // Remove a velocidade correspondente
        int currentStack = GetCurrentStack();
        StageController.OnStackNumberChanged(currentStack);
        return who;
    }
}

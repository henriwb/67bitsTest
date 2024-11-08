using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackObjects : MonoBehaviour
{
    public Transform StackParent;
    public Transform playerTransform;
    public Transform ModelRotation; // Objeto de refer�ncia para rota��o
    private float heightStack = 0.35f;
    private List<GameObject> objects = new List<GameObject>();
    private List<Vector3> playerPositions = new List<Vector3>();
    private int followDelay = 4; // Ajuste do delay para simular in�rcia
    private float springDamping = 0.08f; // For�a do efeito de mola
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
        // Copia a rota��o do ModelRotation para StackParent
        StackParent.rotation = ModelRotation.rotation;

        // Verifica se o player est� parado
        isPlayerMoving = (lastPlayerPosition != playerTransform.position);
        lastPlayerPosition = playerTransform.position;

        // Armazena a posi��o atual do player se ele estiver em movimento
        if (isPlayerMoving)
        {
            playerPositions.Insert(0, playerTransform.position);
        }
        else
        {
            // Mant�m a �ltima posi��o no hist�rico para o efeito de mola
            playerPositions.Insert(0, lastPlayerPosition);
        }

        // Limita o hist�rico para evitar uso excessivo de mem�ria
        if (playerPositions.Count > objects.Count * followDelay + 1)
            playerPositions.RemoveAt(playerPositions.Count - 1);

        // Atualiza a posi��o dos objetos empilhados com efeito de mola e in�rcia
        for (int i = 0; i < objects.Count; i++)
        {
            int delayIndex = (i + 1) * followDelay;
            Vector3 targetPosition;

            // Usa a posi��o hist�rica para aplicar o efeito de mola at� centralizar
            if (delayIndex < playerPositions.Count)
            {
                targetPosition = playerPositions[delayIndex];
            }
            else
            {
                // Centraliza no player, permitindo que o objeto ultrapasse o centro
                targetPosition = playerTransform.position;
            }

            // Mant�m o eixo Y original e ajusta a altura para empilhamento
            targetPosition.y = StackParent.position.y + i * heightStack;

            // Aplica o efeito de mola usando a velocidade acumulada
            Vector3 currentPosition = objects[i].transform.position;
            Vector3 direction = targetPosition - currentPosition;

            // Ajusta a velocidade do objeto para efeito de mola, permitindo ultrapassar o ponto central
            objectVelocities[i] += direction * springDamping;
            objectVelocities[i] *= 0.9f; // Amortecimento da velocidade para reduzir oscila��es gradualmente

            // Atualiza a posi��o do objeto
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
        GameObject clone = ObjectPoolManager.Instance.SpawnFromPool("addStack");
        clone.gameObject.SetActive(true);
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

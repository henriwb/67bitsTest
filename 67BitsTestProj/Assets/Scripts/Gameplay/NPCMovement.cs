using System;
using System.Collections;
using UnityEngine;

public class NPCMovement : PlayerMovement
{
    public Action<Vector3> MoveDirection;
    public Animator animator;

    void Start()
    {
        MoveDirection += UpdateMoveDirection;
        StartCoroutine(RandomMovementRoutine());
    }

    private void OnDisable()
    {
        MoveDirection -= UpdateMoveDirection;
    }

    IEnumerator RandomMovementRoutine()
    {
        while (true)
        {
            Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
            MoveDirection(randomDirection);
            RunTrigger();
            float randomDuration = UnityEngine.Random.Range(1f, 3f); // Dura��es entre 1 e 3 segundos
            yield return new WaitForSeconds(randomDuration); // Espera um tempo aleat�rio antes de mudar a dire��o
            MoveDirection(Vector3.zero);
            RunTrigger();
            randomDuration = UnityEngine.Random.Range(1f, 3f); // Dura��es entre 1 e 3 segundos
            yield return new WaitForSeconds(randomDuration); // Espera um tempo aleat�rio antes de mudar a dire��o
        }
    }

    public void RunTrigger()
    {
        animator.SetTrigger("run");
    }
    
}

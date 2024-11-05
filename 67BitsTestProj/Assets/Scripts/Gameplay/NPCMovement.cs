using System;
using System.Collections;
using UnityEngine;

public class NPCMovement : CharacterMovement
{
    private Action<Vector3> MoveDirection;
    public Animator animator;

    protected override void Update()
    {
        base.Update();
    }

    

    void OnEnable()
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
            RunTrigger(true);
            float randomDuration = UnityEngine.Random.Range(1f, 3f); // Durações entre 1 e 3 segundos
            yield return new WaitForSeconds(randomDuration); // Espera um tempo aleatório antes de mudar a direção
            MoveDirection(Vector3.zero);
            RunTrigger(false);
            randomDuration = UnityEngine.Random.Range(1f, 3f); // Durações entre 1 e 3 segundos
            yield return new WaitForSeconds(randomDuration); // Espera um tempo aleatório antes de mudar a direção
        }
    }

    private void RunTrigger(bool run)
    {
        animator.SetBool("running", run);
    }
    
}

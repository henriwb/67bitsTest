using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ReceiverTask : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SourceToConsume;

    public static bool DoingAnimation;
    

    public void ReceiverInit(StackObjects stacker, Player who)
    {
        if (!stacker.HasOne())
        {
            return;
        }

        if(DoingAnimation)
        {
            return;
        }
        DoingAnimation = true;
        who.myMovement.PauseMovement(true);
        StartCoroutine(ReceiverRoutine(stacker, who));
    }

    IEnumerator ReceiverRoutine(StackObjects stacker, Player who)
    {
        int addCoins = 0;
        while (stacker.HasOne())
        {
            GameObject getFromStack = stacker.ReturnLastFromList();
            addCoins++;
            StartCoroutine(MoveAndShrinkCoroutine(getFromStack, SourceToConsume.transform.position, 0.4f));
            yield return new WaitForSeconds(0.4f);
        }

        while(addCoins > 0)
        {
            StageController.instance.AddCoins();
            yield return new WaitForSeconds(0.2f);
            addCoins--;
        }
        who.myMovement.PauseMovement(false);
        DoingAnimation = false;

    }

    private IEnumerator MoveAndShrinkCoroutine(GameObject objectToMove, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = objectToMove.transform.position;
        Vector3 startScale = objectToMove.transform.localScale;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;

            // Interpolação de posição e escala
            objectToMove.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            objectToMove.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, progress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Garante que o objeto termina exatamente na posição e escala final
        objectToMove.transform.position = targetPosition;
        objectToMove.transform.localScale = Vector3.zero;

        // Desativa o objeto
        
        objectToMove.transform.SetParent(null);
        Destroy(objectToMove);
    }
}

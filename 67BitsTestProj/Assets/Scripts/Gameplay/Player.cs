using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterAnimation myAnimation;

    public Button punchButton;
    void Start()
    {
        punchButton.onClick.RemoveAllListeners();
        punchButton.onClick.AddListener(PunchAction);
    }


    private void PunchAction()
    {
        myAnimation.Punch();
    }
}

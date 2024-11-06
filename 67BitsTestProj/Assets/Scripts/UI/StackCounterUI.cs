using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StackCounterUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI m_stackMaximum;
    public TextMeshProUGUI m_stackCurrent;
   
    public void UpdateUI(int stackMax, int stackCurrent)
    {
        m_stackMaximum.text = stackMax.ToString();
        m_stackCurrent.text = stackCurrent.ToString();


    }
}

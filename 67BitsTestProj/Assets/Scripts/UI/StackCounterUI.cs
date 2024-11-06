using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StackCounterUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI m_stackMaximum;
    public TextMeshProUGUI m_stackCurrent;

    public TextMeshProUGUI m_myLevel;
    public TextMeshProUGUI m_expToLevel;

    public TextMeshProUGUI m_coinsText;

    public Image fillExperienceBar;
   
    public void UpdateUI(int stackMax, int stackCurrent, int myLevel, int expToLevel, int MaxExp)
    {
        m_stackMaximum.text = stackMax.ToString();
        m_stackCurrent.text = stackCurrent.ToString();
        m_myLevel.text = myLevel.ToString();
        m_expToLevel.text = expToLevel.ToString();
        fillExperienceBar.fillAmount = 1 - ((float)expToLevel / MaxExp);
    }

    public void UpdateCoins(int quant)
    {
        m_coinsText.text = quant.ToString();
    }
}

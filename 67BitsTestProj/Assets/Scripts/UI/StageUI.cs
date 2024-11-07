using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private Image m_fillExperienceBar;

    [SerializeField] private TextMeshProUGUI m_stackMaximum;
    [SerializeField] private TextMeshProUGUI m_stackCurrent;
    [SerializeField] private TextMeshProUGUI m_myLevel;
    [SerializeField] private TextMeshProUGUI m_expToLevel;
    [SerializeField] private TextMeshProUGUI m_coinsText;

    private float m_targetFillAmount;
    private float m_fillSpeed = 3f;

    public void UpdateUI(int stackMax, int stackCurrent, int myLevel, int expToLevel, int MaxExp)
    {
        m_stackMaximum.text = stackMax.ToString();
        m_stackCurrent.text = stackCurrent.ToString();
        m_myLevel.text = myLevel.ToString();
        m_expToLevel.text = expToLevel.ToString();
        m_targetFillAmount = 1 - ((float)expToLevel / MaxExp);
    }

    public void ResetExpBar()
    {
        m_targetFillAmount = 0f;
        m_fillExperienceBar.fillAmount = 0f;
    }

    void Update()
    {
        BarSuavization();
    }

    private void BarSuavization() => m_fillExperienceBar.fillAmount = Mathf.Lerp(m_fillExperienceBar.fillAmount, m_targetFillAmount, m_fillSpeed * Time.deltaTime);

    public void UpdateCoins(int quant) =>  m_coinsText.text = quant.ToString();
}

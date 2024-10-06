using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    public float currentHealth = 0.5f;
    VisualElement healthBar;

    public float displayTime = 4.0f;
    VisualElement m_NonPlayerDialogue;
    float m_TimerDisplay;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);
        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
    }

    private void Update()
    {
        if(m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if(m_TimerDisplay < 0)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    public void DisplayDialogue() 
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    public void SetHealthValue(float newValue) 
    {
        if(healthBar != null) 
        {
            healthBar.style.width = Length.Percent(newValue * 100.0f);
        }
    }
}

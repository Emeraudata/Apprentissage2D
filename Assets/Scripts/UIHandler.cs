using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    public float currentHealth = 0.5f;
    VisualElement healthBar;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);
    }

    public void SetHealthValue(float newValue) 
    {
        if(healthBar != null) 
        {
            healthBar.style.width = Length.Percent(newValue * 100.0f);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;
    public UnityEvent onComboEnd;

    public List<ComboSettings> comboSettings = new();
    public Slider timerSlider;

    private void Awake()
    {
        Instance = this;
        timerSlider.maxValue = SnakeManager.comboTimerMax;
    }

    private void Update()
    {
        timerSlider.value = SnakeManager.comboTimer;
        if (SnakeManager.comboTimer > 0) SnakeManager.comboTimer -= Time.deltaTime;
        if (SnakeManager.comboTimer <= 0 && SnakeManager.currentState != SnakeManager.CurrentState.Stop)
        {
            SnakeManager.comboTimer = 0;
            SnakeManager.scoreMulti = 0;
            onComboEnd?.Invoke();
        }
    }
    
    public void AddCombo()
    {
        SnakeManager.scoreMulti++;
        SnakeManager.comboTimer = SnakeManager.comboTimerMax;
        for (int i = 0; i < comboSettings.Count; i++)
        {
            if (comboSettings[i].comboMultiply == SnakeManager.scoreMulti)
            {
                SnakeManager.comboText = comboSettings[i].comboText;
                SnakeManager.comboColor = comboSettings[i].color;
                comboSettings[i].onCombo?.Invoke();
            }
        }
    }
}

[System.Serializable]
public class ComboSettings
{
    public int comboMultiply;
    public string comboText;
    public Color color;
    public UnityEvent onCombo;
}

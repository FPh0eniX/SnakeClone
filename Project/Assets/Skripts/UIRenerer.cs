using TMPro;
using UnityEngine;

public class UIRenerer : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;
    internal enum TextMode
    {
        Score,
        Multiplayer,
        Speed,
        ComboText
    }

    [SerializeField] private TextMode textmode;

    void Update()
    {
        switch (textmode)
        {
            case TextMode.Score:
                textField.text = "Score: " + SnakeManager.score.ToString() + " <size=50>" + SnakeManager.scoreMulti.ToString() + "x</size>";
                break;
            case TextMode.Multiplayer:
                textField.text = SnakeManager.scoreMulti.ToString() + "x";
                break;
            case TextMode.Speed:
                textField.text = "Speed: " + Time.timeScale.ToString() + "x";
                break;
            case TextMode.ComboText:
                textField.text = SnakeManager.comboText;
                textField.color = SnakeManager.comboColor;
                break;
            default:
                break;
        }
    }
}

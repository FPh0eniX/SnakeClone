using System.Collections.Generic;
using UnityEngine;

public static class SnakeManager
{
    internal enum CurrentState
    {
        Stop,
        Start,
        Pause
    }
    internal static CurrentState currentState;

    internal static List<Transform> snakeSegments;

    internal static int score = 0;
    internal static float speed = 1;

    internal static int scoreMulti = 0;
    internal static float comboTimer;
    internal static float comboTimerMax = 5.5f;
    internal static string comboText;
    internal static Color comboColor;
}

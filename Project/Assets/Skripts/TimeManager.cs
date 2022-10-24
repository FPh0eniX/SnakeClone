using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void StartTime(float speed)
    {
        Time.timeScale = speed;
    }
}

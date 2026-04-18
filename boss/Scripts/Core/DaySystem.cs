using UnityEngine;

public class DaySystem : MonoBehaviour
{
    public int currentDay = 1;

    public void NextDay()
    {
        currentDay++;
        Debug.Log("Den je teraz: " + currentDay);
    }
}

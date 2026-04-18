using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public UserSystem userSystem;
    public BossSystem bossSystem;
    public DaySystem daySystem;
    public GameManager gameManager;

    [Header("Boss UI")]
    public Slider bossMoodBar;
    public TextMeshProUGUI bossMoodText;
    public TextMeshProUGUI bossTaskText;

    [Header("Audience UI")]
    public Slider audienceMoodBar;
    public TextMeshProUGUI audienceMoodText;
    public TextMeshProUGUI audienceTaskText;

    [Header("Day UI")]
    public TextMeshProUGUI postsLeftText;

    void Update()
    {
        if (bossSystem != null)
        {
            if (bossMoodBar != null)
            {
                bossMoodBar.minValue = 0;
                bossMoodBar.maxValue = 100;
                bossMoodBar.value = bossSystem.mood;
            }
            if (bossMoodText != null)
                bossMoodText.text = "Boss: " + bossSystem.mood;

            if (bossTaskText != null)
                bossTaskText.text = "Boss task:\n" + bossSystem.GetTaskString();
        }

        if (userSystem != null)
        {
            if (audienceMoodBar != null)
            {
                audienceMoodBar.minValue = 0;
                audienceMoodBar.maxValue = 100;
                audienceMoodBar.value = userSystem.mood;
            }
            if (audienceMoodText != null)
                audienceMoodText.text = "Audience: " + userSystem.mood;

            if (audienceTaskText != null)
                audienceTaskText.text = "Audience task:\n" + userSystem.GetTaskString();
        }

        if (gameManager != null && postsLeftText != null)
            postsLeftText.text = "Posty: " + gameManager.GetPostsLeft() + " / " + gameManager.postsPerDay;
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UserSystem userSystem;
    public BossSystem bossSystem;
    public DaySystem daySystem;

    public int postsPerDay = 3;
    private int postsUsedToday = 0;

    public void Publish(PostData post)
    {
        if (postsUsedToday >= postsPerDay) 
        {
            Debug.Log("Uz si pouzil vsetky posty na dnes!");
            return;
        }

        postsUsedToday++;
        userSystem.ApplyPost(post);
        bossSystem.ApplyPost(post);

        CheckGameState();
    }

    public void EndDay()
    {
        bossSystem.EvaluateDay();
        userSystem.EvaluateDay();

        postsUsedToday = 0;
        daySystem.NextDay();
        CheckGameState();
    }

    public int GetPostsLeft() => postsPerDay - postsUsedToday;

    private void CheckGameState()
    {
        if (bossSystem.IsBossAngry())
            Debug.Log("You are fired!");

        if (userSystem.AllUsersLost())
            Debug.Log("Users left!");
    }
}

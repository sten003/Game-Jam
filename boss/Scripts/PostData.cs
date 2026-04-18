using UnityEngine;

public enum PostType
{
    Meme,
    News,
    Boss
}

[CreateAssetMenu(menuName = "Game/Post")]
public class PostData : ScriptableObject
{
    public PostType type;
    public int engagementValue;
    public int bossValue;
}
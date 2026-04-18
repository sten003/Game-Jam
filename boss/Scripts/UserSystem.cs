using UnityEngine;
using System.Collections.Generic;

public class UserSystem : MonoBehaviour
{
    public int mood = 75;
    
    public int rewardPerCorrectPost = 3;
    public int penaltyPerMissingPost = 4;
    public int penaltyPerExtraPost = 3;

    public Dictionary<PostType, int> requiredPosts = new Dictionary<PostType, int>();
    public Dictionary<PostType, int> currentPosts = new Dictionary<PostType, int>();

    void Start()
    {
        GenerateNewTask();
    }

    public void ApplyPost(PostData post)
    {
        if (!currentPosts.ContainsKey(post.type))
            currentPosts[post.type] = 0;

        currentPosts[post.type]++;
        Debug.Log("Audience: dostal " + post.type);
    }

    public void EvaluateDay()
    {
        int moodChange = 0;

        HashSet<PostType> allInvolvedTypes = new HashSet<PostType>(requiredPosts.Keys);
        foreach (var key in currentPosts.Keys)
        {
            allInvolvedTypes.Add(key);
        }

        foreach (var type in allInvolvedTypes)
        {
            int required = requiredPosts.ContainsKey(type) ? requiredPosts[type] : 0;
            int provided = currentPosts.ContainsKey(type) ? currentPosts[type] : 0;

            if (provided <= required)
            {
                moodChange += provided * rewardPerCorrectPost; 
                moodChange -= (required - provided) * penaltyPerMissingPost;
            }
            else
            {
                moodChange += required * rewardPerCorrectPost;
                moodChange -= (provided - required) * penaltyPerExtraPost; 
            }
        }

        mood += moodChange;
        mood = Mathf.Clamp(mood, 0, 100);
        
        Debug.Log($"Audience vyhodnotenie! Zmena nálady: {moodChange}. Aktuálna nálada: {mood}");
        
        GenerateNewTask();
    }

    public void GenerateNewTask()
    {
        requiredPosts.Clear();
        currentPosts.Clear();

        int typesCount = Random.Range(1, 3);
        List<PostType> allTypes = new List<PostType> { PostType.Meme, PostType.News, PostType.Boss };

        for (int i = 0; i < allTypes.Count; i++)
        {
            int r = Random.Range(i, allTypes.Count);
            PostType tmp = allTypes[i];
            allTypes[i] = allTypes[r];
            allTypes[r] = tmp;
        }

        for (int i = 0; i < typesCount; i++)
        {
            int count = Random.Range(1, 3);
            requiredPosts[allTypes[i]] = count;
        }

        Debug.Log("Novy audience task: " + GetTaskString());
    }

    public bool AllUsersLost() => mood <= 0;

    public string GetTaskString()
    {
        string s = "";
        foreach (var req in requiredPosts)
        {
            int got = currentPosts.ContainsKey(req.Key) ? currentPosts[req.Key] : 0;
            s += req.Key + ": " + got + "/" + req.Value + "  ";
        }
        return s;
    }
}

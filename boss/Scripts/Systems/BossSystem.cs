using UnityEngine;
using System.Collections.Generic;

public class BossSystem : MonoBehaviour
{
    public int mood = 50;

    public int rewardPerCorrectPost = 4;
    public int penaltyPerMissingPost = 5;
    public int penaltyPerExtraPost = 2;

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
        Debug.Log("Boss: dostal " + post.type);
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
                //menej alebo presne ako malo byt
                moodChange += provided * rewardPerCorrectPost; //bonus
                moodChange -= (required - provided) * penaltyPerMissingPost; //za to ze chybalo
            }
            else
            {
                //bolo viac ako trebalo
                moodChange += required * rewardPerCorrectPost; //plny bonus za splnene
                moodChange -= (provided - required) * penaltyPerExtraPost; //nadbytocne
            }
        }

        mood += moodChange;
        mood = Mathf.Clamp(mood, 0, 100);
        
        Debug.Log($"Boss vyhodnotenie! Zmena nálady: {moodChange}. Aktuálna nálada: {mood}");
        
        GenerateNewTask();
    }

    public void GenerateNewTask()
    {
        requiredPosts.Clear();
        currentPosts.Clear();

        int typesCount = Random.Range(1, 3);
        List<PostType> allTypes = new List<PostType> { PostType.Meme, PostType.News, PostType.Boss };

        // Zamiešaj
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

        Debug.Log("Novy boss task: " + GetTaskString());
    }

    public bool IsBossAngry() => mood <= 0;

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

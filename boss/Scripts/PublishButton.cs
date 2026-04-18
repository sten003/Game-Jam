using UnityEngine;

/*public class PublishButton : MonoBehaviour
{
    public GameManager gameManager;
    public PostData post;

    public void OnClick()
    {
        gameManager.Publish(post);
    }
}*/

public class PublishButton : MonoBehaviour
{
    public GameManager gameManager;
    public PostType postType;

    public void OnClick()
    {
        PostData tempPost = ScriptableObject.CreateInstance<PostData>();
        tempPost.type = postType;
        tempPost.engagementValue = 10;
        tempPost.bossValue = 10;
        gameManager.Publish(tempPost);
    }
}

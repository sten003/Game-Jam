using UnityEngine;

[CreateAssetMenu(menuName = "Game/UserGroup")]
public class UserGroupData : ScriptableObject
{
    public string groupName;
    public PostType preferredType;
    public int interest = 50;
}
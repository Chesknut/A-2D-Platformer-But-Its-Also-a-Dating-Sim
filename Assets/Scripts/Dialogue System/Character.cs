using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character", order = 0)]
public class Character : ScriptableObject 
{
    public string fullName;
    public Sprite portrait;
}
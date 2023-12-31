using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public int health;
    public int attackMod;
    public float speedMod;
    public Element elementType;
    public GameObject skin;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyObject/GenericEnemyObject")]
public class EnemyObject : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private EnemyTier enemyTier;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float visionRange;
    [SerializeField] private float fov;
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<Attribute> attributes = new List<Attribute>();
    [SerializeField] private List<EquipmentItemObject> equipments = new List<EquipmentItemObject>();
    public string EnemyName { get => enemyName; set => enemyName = value; }
    public EnemyTier EnemyTier { get => enemyTier; set => enemyTier = value; }
    public EnemyType EnemyType { get => enemyType; set => enemyType = value; }
    public float VisionRange { get => visionRange; set => visionRange = value; }
    public float Fov { get => fov; set => fov = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public List<Attribute> Attributes { get => attributes; set => attributes = value; }
    public List<EquipmentItemObject> Equipments { get => equipments; set => equipments = value; }
}
public enum EnemyTier
{
    Easy,
    Medium,
    Hard,
    MiniBoss,
    FianlBoss
}
public enum EnemyType
{
    Zombie,
    Skeleton,
    Dragon
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ObstacleConfig", menuName = "Config/Obstacle")]

public class ObstacleConfig : ScriptableObject
{
    public Sprite icon;
    public BoosterConfig booster;
    public int health;
}

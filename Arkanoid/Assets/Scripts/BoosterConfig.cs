using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BoosterConfig", menuName = "Config/Booster")]

public class BoosterConfig : ScriptableObject
{
    public Sprite icon;
    public BoosterController.boostertype boostertype;
}

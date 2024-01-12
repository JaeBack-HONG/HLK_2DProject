using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataJson
{
    
    public int AbilityCheck_1 = 0;
    public int AbilityCheck_2 = 0;
    public int AbilityCheck_3 = 0;
    
    public int Ability_1 = 0;
    public int Ability_2 = 0;
    public int Ability_3 = 0;
    
    public int Ability_1_count = 0;
    public int Ability_2_count = 0;
    public int Ability_3_count = 0;

    public float maxHealth = 3f;
    public float currentHealth = 3f;    
    
    public string SceneName = string.Empty;    
}

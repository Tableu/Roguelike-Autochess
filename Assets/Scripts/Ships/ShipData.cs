using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShipData
{
    public string ShipName;
    public int Cost;
    public int Health;
    public int MaxHealth;
    public int Speed;
    public float StopDistance;
    public float AggroRange;
    public List<AttackScriptableObject> Weapons;
    public List<ScriptableObject> Modules;
    public List<ScriptableObject> Buffs;
    public Vector2 StartingPos;
    public GameObject ShipVisuals;
    public ShipDataComponent BattleController;
    public ShipDataComponent ShopUI;
    public int RepairCost => (MaxHealth - Health) * 100;
}
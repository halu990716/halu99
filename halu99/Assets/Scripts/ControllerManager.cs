using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
    private ControllerManager() { }
    private static ControllerManager Instance = null;

    public static ControllerManager GetInstance()
    {
        if (Instance == null)
            Instance = new ControllerManager();
        return Instance;
    }
    public int Rand = 1;

    public int Player_List = 1;

    public int Player_MaxHp = 3;
    public int Player_HP = 3;


    public int MissileDamage = 10;
    public int MissileHp = 1;
    public float AttackSpeed = 1;
    public int AttackCount = 1;

    public int EnemyHp = 30;

    public int BossHp = 30;


    public string UserName;

    public int ClearTime = 9999;

    public string BastUserName;
    public int BastClearTime = 1111;

    public string SecondUserName;
    public int SecondClearTime = 2222;

    public string ThirdName;
    public int ThirdClearTime = 3333;
}
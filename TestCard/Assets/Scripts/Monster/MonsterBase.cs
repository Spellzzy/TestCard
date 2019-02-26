using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOSTER_TYPE
{
    PUBLIC, // 普通
    SPECIAL, // 精英
    BOSS,   // Boss
    DERIVE, // 衍生物
}

public class MonsterBase : MonoBehaviour {

    // 血量
    public int Hp;

    // 游戏展现对象
    public GameObject gameObj;

    // 召唤物 普通 精英 Boss 
    public MOSTER_TYPE type = MOSTER_TYPE.PUBLIC;

    public MonsterBase(MonsterInfo info)
    {

    }

    // 怪物攻击
    public virtual void Attack(int value, Action callback = null)
    {
        if (callback != null)
        {
            callback();
        }
    }

    // 怪物防御
    public virtual void Defend(int value , Action callback = null)
    {
        if (callback != null)
        {
            callback();
        }
    }
}

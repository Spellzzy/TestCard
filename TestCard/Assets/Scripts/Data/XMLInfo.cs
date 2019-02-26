using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLInfo
{
    public int ID { get; set; }
    public virtual void GetValue(XmlElement xl)
    {
        ID = Convert.ToInt32(xl.GetAttribute("ID"));
    }
}

// 职业信息
public class CarrerInfo : XMLInfo
{
    public string Name { get; set; }

    public string Desc { get; set; }

    public string Icon { get; set; }

    public int HP { get; set; }

    public override void GetValue(XmlElement xl)
    {
        base.GetValue(xl);
        Name = xl.GetAttribute("Name");

        HP = Convert.ToInt32(xl.GetAttribute("HP"));

        Desc = xl.GetAttribute("Desc");

        Icon = xl.GetAttribute("Icon");
    }

}

// 路线 地图信息
public class MapInfo : XMLInfo
{
    // 当前关卡ID
    public int StageID;
    // 该关卡 怪物ID List
    public List<int> MonsterID_List = new List<int>();

    public override void GetValue(XmlElement xl)
    {                                           
        base.GetValue(xl);

        StageID = Convert.ToInt32(xl.GetAttribute("ID"));

        string monsterList_str = xl.GetAttribute("NextYPos");
        MonsterID_List = new List<int>();
        foreach (var item in monsterList_str.Split('@'))
        {
            MonsterID_List.Add(Convert.ToInt32(item));
        }
    }
}

public enum STAGE_TYPE
{    
    RANDOM,     // 随机事件
    SHOP,       // 商店
    REWARD,     // 宝箱奖励
    REST,       // 休息处
    NORMAL,     // 普通怪
    SPECIAL,    // 精英怪
    BOSS,       // 章节BOSS
}

// 关卡位置信息
public class StagePos
{
    public int XPos { get; set; } // 16 - 1
    public int YPos { get; set; } // 1  - 5

    public StagePos(int x, int y)
    {
        XPos = x;
        YPos = y;
    }
}


// 关卡信息
public class StageInfo : XMLInfo
{
    // 关卡ID
    public int StageID { get; set; }

    // 关卡类型
    public STAGE_TYPE StageType { get; set; }

    // 关卡所在层级(1- 16) 16为Boss层
    public int XPos { get; set; }

    //  关卡所在层 的横向位置
    public int YPos { get; set; }

    // 下一关卡所在层的 横向位置 用于路线指向
    public List<int> NextYPos { get; set; }

    // 怪物ID
    public int MonsterID { get; private set; }

    public StagePos Position { get; private set; }

    // 类型 

    public override void GetValue(XmlElement xl)
    {
        base.GetValue(xl);

        StageID = Convert.ToInt32(xl.GetAttribute("ID"));

        StageType = (STAGE_TYPE)Convert.ToInt32(xl.GetAttribute("StageType"));

        MonsterID = Convert.ToInt32(xl.GetAttribute("MonsterID"));

        XPos = Convert.ToInt32(xl.GetAttribute("XPos"));
        YPos = Convert.ToInt32(xl.GetAttribute("YPos"));

        string NextYPos_str = xl.GetAttribute("NextYPos");
        NextYPos = new List<int>();
        foreach (var item in NextYPos_str.Split('@'))
        {
            NextYPos.Add(Convert.ToInt32(item));
        }

        Position = new StagePos(XPos, YPos);
    }
}


public class MonsterInfo : XMLInfo
{
    public int MonsterID;

    public int Hp;

    // 行为逻辑
    public String ActionLogic;

    // 
    public string Name;

    public string Art;
}
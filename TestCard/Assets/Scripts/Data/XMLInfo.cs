using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLInfo
{
    public string ID { get; set; }
    public virtual void GetValue(XmlElement xl)
    {
        ID = xl.GetAttribute("ID");
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
        ID = xl.GetAttribute("ID");

        Name = xl.GetAttribute("Name");

        HP = Convert.ToInt32(xl.GetAttribute("HP"));

        Desc = xl.GetAttribute("Desc");

        Icon = xl.GetAttribute("Icon");
    }

}

// 路线 地图信息
public class MapInfo : XMLInfo
{
    public override void GetValue(XmlElement xl)
    {                                           
        base.GetValue(xl);
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


// 关卡信息
public class StageInfo : XMLInfo
{
    // 关卡ID
    public int StageID { get; set; }

    // 关卡类型
    public STAGE_TYPE StageType { get; set; }

    // 关卡所在层级(1- 16) 16为Boss层
    public int Level { get; set; }

    //  关卡所在层 的横向位置( 1 - 5 )
    public int YPos { get; set; }

    // 下一关卡所在层的 横向位置( 1 - 5 ) 用于路线指向
    public int NextPos { get; set; }

    // 怪物ID
    public int MonsterID { get; private set; }

    // 类型 

    public override void GetValue(XmlElement xl)
    {
        base.GetValue(xl);

        StageID = Convert.ToInt32(xl.GetAttribute("StageID"));

        StageType = (STAGE_TYPE)Convert.ToInt32(xl.GetAttribute("StageType"));

        MonsterID = Convert.ToInt32(xl.GetAttribute("MonsterID")); 
    }
}
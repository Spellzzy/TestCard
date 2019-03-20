using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using SlayCard;

// 卡牌类型
public enum CARD_TYPE {
    ATTAK,      // -- 攻击
    SKILL,      // -- 技能
    ABILITY     // -- 能力
}

// 卡牌品质
public enum CARD_QUALITY {
    COMMON,     // 普通
    RARE,       // 稀有
    LEGEND      // 传说
}

public enum CARD_CAREER {
    WARRIOR = 101,        // -- 战士
    HUNTER,         // -- 猎人
    WARLOCK,        // -- 术士
    COMMON,         // -- 通用
}

public class BaseCard : XMLInfo {

    // 卡牌ID
    public long ID { get; private set; }

    // 名称
    private string _name;
    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    // 卡牌消耗
    public int Cost { get; private set; }

    // 卡牌类型
    public CARD_TYPE Type { get; private set; }

    // 卡牌品质
    public CARD_QUALITY Quality { get; private set; }

    // 卡牌图标
    public string Icon { get; private set; }

    // 卡牌描述
    public string Desc { get; private set; }

    // 卡牌职业
    public CAREER Career { get; private set; }

    // 等级
    public int Level { get; set; }

    public int Index { get; set; }

    public int Atk;

    public int Def;

    // 卡牌升级
    public void Upgrade()
    {
        if (Level > 2)
        {
            return;
        }
        //todo 更新属性
    }

    // 卡牌随机变换
    public void Change()
    {

    }

    public override void GetValue(XmlElement xl)
    {
        base.GetValue(xl);

        ID = Convert.ToInt32(xl.GetAttribute("ID"));

        Quality = (CARD_QUALITY)Convert.ToInt32(xl.GetAttribute("Quality"));

        Career = (CAREER)Convert.ToInt32(xl.GetAttribute("Carrer"));

        Name = xl.GetAttribute("Name");

        Icon = xl.GetAttribute("Icon");

        Atk = Convert.ToInt32(xl.GetAttribute("Attack"));
        Def = Convert.ToInt32(xl.GetAttribute("Defence"));
    }
}

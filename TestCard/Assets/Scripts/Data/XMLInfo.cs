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

// 职业类
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UIBattlePage : Window{

    protected override void OnInit()
    {
        base.OnInit();

        contentPane = UIPackage.CreateObject("main", "battle_show").asCom;

    }



    public void SetMonsterInfo(MonsterBase info, int index)
    {
        // todo 找到怪物位置  初始化怪物UI信息
    }

    public void SetPlayerInfo()
    {
        // todo 设置玩家状态UI 信息
    }
}

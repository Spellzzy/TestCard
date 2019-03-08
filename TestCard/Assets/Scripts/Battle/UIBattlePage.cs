using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using SlayCard;

public class UIBattlePage : Window{

    private Player cur_player;

    #region UI组件
    // panel
    private GComponent panel_com;
    // 
    private GTextField hp_text;

    private GTextField gold_text;

    private GTextField floor_text;

    private GTextField name_text;


    #endregion
    protected override void OnInit()
    {
        base.OnInit();

        InitData();


        contentPane = UIPackage.CreateObject("main", "battle_show").asCom;
    }

    protected override void OnShown()
    {
        base.OnShown();
        InitPlayerInfo();

        contentPane = panel_com;
    }


    public void InitData()
    {
        //  获取当前玩家信息
        cur_player = Controller.Instance.PlayerInfo;

    }

    public void InitCom()
    {
        // 玩家名称
        name_text = panel_com.GetChild("name_text").asTextField;
    }


    public void SetMonsterInfo(MonsterBase info, int index)
    {
        // todo 找到怪物位置  初始化怪物UI信息
    }

    public void InitPlayerInfo()
    {
        //设置玩家状态UI 信息
        name_text.text = Controller.Instance.model.Name;
        SetHpChange();
        SetGoldChange();

        // todo设置玩家 左侧信息
        // todo初始化牌组
    }
















    public void SetHpChange()
    {
        hp_text.text = string.Format("%d/%d", cur_player.HP, cur_player.MAX_HP);
    }

    public void SetGoldChange()
    {
        gold_text.text = cur_player.Money.ToString();
    }
}

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

    private GComponent up_com;
    // 
    private GTextField hp_text;

    private GTextField gold_text;

    private GTextField floor_text;

    private GTextField name_text;

    private GList card_list;


    #endregion
    protected override void OnInit()
    {
        base.OnInit();
        panel_com = UIPackage.CreateObject("main", "battle_show").asCom;

        InitCom();
        InitData();
        InitPlayerInfo();
    }

    protected override void OnShown()
    {
        contentPane = panel_com;
        base.OnShown();
    }


    public void InitData()
    {
        //  获取当前玩家信息
        cur_player = Controller.Instance.PlayerInfo;

    }

    public void InitCom()
    {
        up_com = panel_com.GetChild("up_com").asCom;
        // 玩家名称
        name_text = up_com.GetChild("name_text").asTextField;

        hp_text = up_com.GetChild("hp_text").asTextField;

        gold_text = up_com.GetChild("gold_text").asTextField;

        card_list = panel_com.GetChild("card_list").asList;
        card_list.RemoveChildrenToPool();
    }


    public void SetMonsterInfo(MonsterBase info, int index)
    {
        // todo 找到怪物位置  初始化怪物UI信息
        Debug.Log("UI 初始化展现 怪物 ===> " + info.info.Name);
    }

    public void InitPlayerInfo()
    {
        //设置玩家状态UI 信息
        //name_text.text = Controller.Instance.model.Name;
        SetHpChange();
        SetGoldChange();

        // todo设置玩家 左侧信息
        // todo初始化牌组

        

    }
















    public void SetHpChange()
    {
        hp_text.text = string.Format("{0}/{1}", cur_player.HP, cur_player.MAX_HP);
    }

    public void SetGoldChange()
    {
        gold_text.text = cur_player.Money.ToString();
    }

    public void SetListCardUI(BaseCard _card)
    {
        // todo 玩家抽到的卡牌 加入手牌列表展示用
        Debug.Log(_card.ID);

        GComponent com = card_list.GetFromPool("").asCom;

        GTextField name_text = com.GetChild("name_text").asTextField;
        name_text.text = _card.Name;

        GTextField value_text = com.GetChild("value_text").asTextField;
        value_text.text = _card.Atk.ToString();

        card_list.AddChild(com);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using SlayCard;

public class UIBattlePage : Window{

    private Player cur_player;

    private BattleLogic _logic;

    #region UI组件
    // panel
    private GComponent panel_com;

    private GComponent up_com;
    // 
    private GTextField hp_text;

    private GTextField gold_text;

    private GTextField floor_text;

    private GTextField name_text;

    private GComponent card_list;

    private FairyGUI.Controller card_list_con;

    #endregion

    public UIBattlePage(BattleLogic logic)
    {
        _logic = logic;
    }

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

        card_list = panel_com.GetChild("card_list").asCom;

        card_list_con = card_list.GetController("card");
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

    public void SetListCardUI(int index, BaseCard _card)
    {
        // todo 玩家抽到的卡牌 加入手牌列表展示用
        Debug.Log(_card.ID);

        GComponent com = card_list.GetChild("card_" + index).asCom.GetChild("body").asCom;

        GTextField name_text = com.GetChild("name_text").asTextField;
        name_text.text = _card.Name;

        GTextField value_text = com.GetChild("value_text").asTextField;
        value_text.text = _card.Atk.ToString();
        com.data = _card;

        //com.onTouchBegin.Set(TouchCard);

        com.draggable = true;
        com.onDragStart.Set(OnDragSatart);
        com.onDragMove.Set(OnDrag);
        com.onDragEnd.Set(OnDragEnd);
        com.dragBounds = new Rect(0, 0, 2048, 1152);

        card_list_con.SetSelectedIndex(index);
    }
    Vector2 card_positon;

    private void OnDragSatart(EventContext context)
    {
        //context.PreventDefault();
        GComponent com = (GComponent)context.sender;
        card_positon = com.xy;
        Debug.Log("Start pos " + card_positon);
    }
    private void OnDrag(EventContext context)
    {
        //Debug.Log(Input.mousePosition.x + " // " +  Input.mousePosition.y);
        //Debug.Log(Tools.GetScreenXY(screenPos));
        //((GButton)context.sender).xy = Tools.GetScreenXY(Input.mousePosition);
    }

    private void OnDragEnd(EventContext context)
    {
        GComponent com = (GComponent)context.sender;

        if (CheckCardPos(card_positon, com.xy))
        {
            // todo 手牌列表数据移除
            card_list_con.SetSelectedIndex(card_list_con.selectedIndex - 1);
        }
        else
        {
            // 拖拽距离过短 表示不使用 回归原位
            com.position = card_positon;
        }
    }

    private bool CheckCardPos(Vector2 before, Vector2 after)
    {
        Debug.Log("before ---> " + before);
        Debug.Log("after ---> " + after);
        float dis = Vector2.Distance(before, after);
        if (dis > 300.0f)
        {
            return true;
        }
        return false;
    }

    public void TouchCard(EventContext context)
    {
        if (_logic.GetCurrentCard())
        {
            return;
        }
        // 当前所选卡牌
        Debug.Log("context ---> " + context);
        Debug.Log("context.data ---> " + context.sender);
        GComponent card_com = (GComponent)context.sender;
        BaseCard card_data = (BaseCard)card_com.data;

        //// 记录原层
        //int before_order = card_com.sortingOrder;

        //// 记录原宽高
        //float befor_width = card_com.width;
        //float befor_height = card_com.height;


        //// 置顶层
        //card_com.sortingOrder = 100000000;
        //// 放大
        //card_com.width *= 1.2f;
        //card_com.width *= 1.2f;

        _logic.SetCurrentCard(card_data);

        Debug.LogError("Select ----> " + card_data.ID);

    }
}

using SlayCard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TURN_TYPE
{
    EMPTY,
    PLAYER,
    MONSTER,
    OVER
}

public class BattleLogic : MonoBehaviour {
    private int ADDNUM = 3;
    // UI
    private UIBattlePage battleUI;
    // 回合状态
    private TURN_TYPE currentTurn = TURN_TYPE.EMPTY;

    private int monsterID;
    // 关卡id 暂时弃用 先实现单个怪物
    private int stageID;

    private MapInfo curMap;
    // 怪物详情list
    private Dictionary<int, MonsterBase> curMonster_dic = new Dictionary<int, MonsterBase>();
    // 怪物原型list
    private Dictionary<int, MonsterInfo> proMonster_dic = new Dictionary<int, MonsterInfo>();
    // 玩家信息
    private Player player;

    private CardGroup cardGroup;

    // 当前焦点卡牌
    private BaseCard currentCard = null;
    public bool GetCurrentCard()
    {
        return currentCard != null;
    }

    public void SetCurrentCard(BaseCard _card)
    {
        currentCard = _card;
    }

    public BattleLogic()
    {
        InitMonsterDic();        
    }

    public void ShowBattleUI(int _id)
    {
        monsterID = _id;

        player = Controller.Instance.model.player;
        cardGroup = player.CardGroup;

        battleUI = new UIBattlePage(this);
        battleUI.Show();
        // 当前怪物详情
        curMonster_dic[monsterID] = new MonsterBase(GetMonsterInfo(monsterID));

        InitMonster();
        InitPlayer();
    }

    private void InitMonsterDic()
    {
        proMonster_dic = ReadXML.GetInfoDic<MonsterInfo>(Application.dataPath + "/Resources/XML/" + "monster_info.xml");
        Debug.Log("怪物原型数量 ===> " + proMonster_dic.Count);
    }

    private MonsterInfo GetMonsterInfo(int id)
    {
        if (proMonster_dic.ContainsKey(id))
        {
            return proMonster_dic[id];
        }
        return null;
    }

    private void GetMonsterFromStage()
    {
        // 根据 stageID 获取 关卡中 怪物ID(可能多名)
        Dictionary<int, MapInfo> cur_dic = ReadXML.GetInfoDic<MapInfo>(Application.dataPath + "/Resources/XML/" + "stage_info.xml");
        if (cur_dic.ContainsKey(stageID))
        {
            Debug.LogError("error  stage ID");
            return;
        }
        else
        {
            curMap = cur_dic[stageID];
        }

        for (int i = 0; i < curMap.MonsterID_List.Count; i++)
        {
            int id = curMap.MonsterID_List[i];
            curMonster_dic[id] = new MonsterBase(GetMonsterInfo(id));
        }
        
        // todo UI展示玩家当前信息
        // todo 怪物群 初始化战斗阶段 (回合数从1开启) 实例时 缓存怪物的进攻逻辑(例如 两个攻击回合接一个buff回合再接一个防御回合  每轮逻辑之后可能数值要发生变换)
        // todo 开始战斗准备 玩家洗牌 -> 抽牌 

    }




    //  初始化怪物信息
    private void InitMonster()
    {
        // 实例化怪物(群)信息 -> UI展示 
        int index = 1;
        foreach (var item in curMonster_dic)
        {
            battleUI.SetMonsterInfo(item.Value, index);
            index++;
        }
    }
    // 战斗开始初始玩家信息
    private void InitPlayer()
    {
        battleUI.InitPlayerInfo();
        cardGroup.FightBegin();
        InitCardBattle();
    }

    private void InitCardBattle()
    {
        // 回合开始 抽牌 多三张
        for (int i = 0; i < player.ExtractNum + ADDNUM; i++)
        {
            // 抽出来的牌
            BaseCard cur_card = cardGroup.ExtractCard(cardGroup.GetRandomIndex());
            // UI展示压入手牌列表
            battleUI.SetListCardUI(cardGroup.GetHandListNum(), cur_card);
        }
    }

    void Update () {
        if (currentTurn == TURN_TYPE.EMPTY)
            return;

        if (currentTurn == TURN_TYPE.PLAYER)
        {
            // todo 玩家回合 等待玩家操作
        }
        else if (currentTurn == TURN_TYPE.EMPTY)
        {
            // todo 怪物回合 按照攻击逻辑 依次进行攻击 防御 进化
        }
        else if (currentTurn == TURN_TYPE.OVER)
        {
            // todo 战斗结束 判断 胜利方
            currentTurn = TURN_TYPE.EMPTY;
        }
	}
}

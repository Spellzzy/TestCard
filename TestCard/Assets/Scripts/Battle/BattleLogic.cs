using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogic : MonoBehaviour {
    // UI
    private UIBattlePage battleUI;

    private int stageID;

    private MapInfo curMap;

    private Dictionary<int, MonsterBase> curMonster_dic = new Dictionary<int, MonsterBase>();

    private Dictionary<int, MonsterInfo> proMonster_dic = new Dictionary<int, MonsterInfo>();

    public BattleLogic(int stage_id)
    {
        stageID = stage_id;
    }

    private void ShowBattleUI()
    {
        battleUI = new UIBattlePage();
        battleUI.Show();
    }

    private void InitMonsterDic()
    {
        proMonster_dic = ReadXML.GetInfoDic<MonsterInfo>(Application.dataPath + "/Resources/XML/" + "monster_info.xml");
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
        Dictionary<int, MapInfo> cur_dic = ReadXML.GetInfoDic<MapInfo>(Application.dataPath + "/Resources/XML/" + "map_info.xml");
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
        // todo 根据 stageID 获取 关卡中 怪物ID(可能多名)
        // todo 实例化怪物(群)信息 -> UI展示 
        // todo UI展示玩家当前信息
        // todo 怪物群 初始化战斗阶段 (回合数从1开启) 实例时 缓存怪物的进攻逻辑(例如 两个攻击回合接一个buff回合再接一个防御回合  每轮逻辑之后可能数值要发生变换)
        // todo 开始战斗准备 玩家洗牌 -> 抽牌 

    }

    void Update () {
		
	}
}

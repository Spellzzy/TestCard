using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlayCard
{

public enum CAREER
{
    NONE,
    WARRIOR,        // -- 战士
    HUNTER,         // -- 猎人
    WARLOCK,        // -- 术士
}

public class Player {

    // 玩家职业
    private CAREER _career = CAREER.NONE;

    // 血量
    public int HP { get; private set; }
    
    // 血量上限
    public int MAX_HP { get; private set; }
        
    // 玩家名
    public string Name { get; set; }
    
    // 金币
    public int Money { get; private set; }

    // 卡组 CardGroup
    public CardGroup CardGroup;

    // todo 神器组 ItemGroup    

    public Player(int career)
    {
        _career = (CAREER)career;
        SetInfo();
        CardGroup = CardGroup.Create(_career);
    }

    private void SetInfo()
    {
        switch (_career)
        {
            case CAREER.WARRIOR:
                SetWarrior();
                break;
            case CAREER.HUNTER:
                SetHunter();
                break;
            case CAREER.WARLOCK:
                SetWarlock();
                break;
            default:
                break;
        }
    }

    #region 设置职业基础信息

    // 设置战士初始信息
    private void SetWarrior()
    {
        HP = 90;
        MAX_HP = HP;
    }
    // 设置lieren初始信息
    private void SetHunter()
    {
        HP = 75;
        MAX_HP = HP;
    }
    // 设置术士初始信息
    private void SetWarlock()
    {
        HP = 65;
        MAX_HP = HP;
    }
    #endregion


    }

}


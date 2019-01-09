using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SlayCard;

public class CardGroup {
    // 牌组堆
    private List<BaseCard> deckCardList = null;

    // 手牌
    private List<BaseCard> handCardList = null;

    // 战斗开始前 记录下原牌堆 局内对此堆操作
    private List<BaseCard> startCardList = null;

    // 弃牌堆
    private List<BaseCard> usedCardList = null;

    public CardGroup(CAREER _career)
    {
        deckCardList = new List<BaseCard>();
        usedCardList = new List<BaseCard>();
        handCardList = new List<BaseCard>();

        SetInitCard(_career);
    }

    public static CardGroup Create(CAREER _career)
    {
        return new CardGroup(_career);
    }

    public void Init()
    {
        deckCardList.Clear();
        handCardList.Clear();
        usedCardList.Clear();
    }

    public void FightOver()
    {
    }

    private void SetInitCard(CAREER _career)
    {
        // 判定职业设置初始卡
        switch (_career)
        {
            case CAREER.WARRIOR:
                SetWarriorCard();
                break;
            case CAREER.HUNTER:
                SetHunterCard();
                break;
            case CAREER.WARLOCK:
                SetWarlockCard();
                break;
            default:
                break;
        }
    }

    #region 设置职业基础卡牌
    // 设置初始卡牌 
    // todo 从xml 中读取 职业初始卡有哪些
    private void SetWarriorCard()
    {

    }

    private void SetHunterCard()
    {

    }

    private void SetWarlockCard()
    {

    }



    #endregion


    #region 常规卡组操作

    // todo 向卡组内添加卡牌

    // todo 移除卡组内一张卡牌

    // todo 对卡组内一张卡牌进行升级

    // todo 对卡组内一张卡牌进行随机变化

    // todo 查看 展示卡组

    #endregion

    #region 战斗内卡组操作

    // todo 抽牌

    // todo 用牌

    // todo 弃牌

    // todo 查看 展示剩余卡组卡牌

    // todo 查看 展示弃牌堆卡组

    // todo 卡组内卡数不足 将弃牌堆卡装回卡组 清空弃牌堆

    // todo 战斗结束 清空 弃牌堆 手牌 起始牌堆

    #endregion
}

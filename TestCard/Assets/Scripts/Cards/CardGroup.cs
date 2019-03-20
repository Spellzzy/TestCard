using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SlayCard;

public class CardGroup {

    // 卡牌原型
    private Dictionary<int, BaseCard> proCard_dic = new Dictionary<int, BaseCard>();

    // 牌组堆
    private Dictionary<int, BaseCard> deckCardDic = null;

    // 手牌
    private List<BaseCard> handCardList = null;

    // 战斗开始前 记录下原牌堆 局内对此堆操作
    private List<BaseCard> deckCardList = null;

    // 弃牌堆
    private List<BaseCard> usedCardList = null;

    // 卡组内数量
    public int Count { get; private set; }

    protected CardGroup(CAREER _career)
    {
        // 卡牌原型字典
        proCard_dic = ReadXML.GetInfoDic<BaseCard>(Application.dataPath + "/Resources/XML/" + "card_info.xml");

        deckCardDic = new Dictionary<int, BaseCard>();

        deckCardList = new List<BaseCard>();
        usedCardList = new List<BaseCard>();
        handCardList = new List<BaseCard>();

        Count = 0;

        SetInitCard(_career);
    }

    public static CardGroup Create(CAREER _career)
    {
        return new CardGroup(_career);
    }

    public void Init()
    {
        // 卡组排数归0
        deckCardDic.Clear();
        Count = 0;

        // 初始 三个列表
        InitAllCardList();
    }

    public void FightBegin()
    {
        InitAllCardList();

        DeckDicToList();
    }

    public void FightOver()
    {

    }

    private void SetInitCard(CAREER _career)
    {
        SetInitCardByCaeer(_career);
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

    private void SetInitCardByCaeer(CAREER _career)
    {
        foreach (var item in proCard_dic)
        {
            Debug.Log("player ---> "+ _career  +  " ||  dic --> " + item.Value.Career);
            if (item.Value.Career == _career)
            {
                deckCardDic.Add(item.Key, item.Value);
            }
        }
    }

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

    // 向卡组内添加不定数量卡牌
    public void AddDeckCard(params BaseCard[] _cards)
    {
        for (int i = 0; i < _cards.Length; i++)
        {
            AddDeckCardOne(_cards[i]);
        }
    }

    // 向卡组添加单张卡牌
    public void AddDeckCardOne(BaseCard _card)
    {
        if (deckCardDic == null)
            return;

        Count = deckCardDic.Count + 1;
        _card.Index = Count;

        deckCardDic.Add(Count, _card);
    }

    // 移除卡组内多张卡牌
    public void RemoveDeckCard(params int[] indexs)
    {
        for (int i = 0; i < indexs.Length; i++)
        {
            RemoveDeckCardOne(i);
        }
    }

    // 移除卡组内一张卡牌
    public void RemoveDeckCardOne(int index)
    {
        if (!deckCardDic.ContainsKey(index))
        {
            return;
        }
        
        // 把当前卡牌移到字典尾部
        for (int i = index; i < deckCardDic.Count; i++)
        {
            var card = deckCardDic[i];
            deckCardDic[i] = deckCardDic[i + 1];
            deckCardDic[i + 1] = card;
        }

        // 删除尾部元素
        deckCardDic.Remove(deckCardDic.Count);

        Count--;
    }


    // 对卡组内一张卡牌进行升级
    public void UpgradeDeckCardOne(int index)
    {
        if (!deckCardDic.ContainsKey(index))
        {
            return;
        }

        deckCardDic[index].Upgrade();
    }

    // 对卡组内一张卡牌进行随机变化
    public void ChangeDeckCardOne(int index)
    {
        if (!deckCardDic.ContainsKey(index))
        {
            return;
        }

        deckCardDic[index].Change();
    }

    // 查看 展示卡组
    public BaseCard ShowCardOne(int index)
    {
        if (!deckCardDic.ContainsKey(index))
        {
            return null;
        }

        return deckCardDic[index];
    }

    #endregion


    #region 战斗内卡组操作
    // 战斗开始 把卡组导入牌堆
    public void DeckDicToList()
    {
        foreach (var item in deckCardDic)
        {
            deckCardList.Add(item.Value);
        }
    }

    public int GetRandomIndex()
    {
        return Random.Range(0, deckCardList.Count);
    }

    // 抽牌
    public BaseCard ExtractCard(int index)
    {
        BaseCard _card = deckCardList[index];

        // 加入手牌
        handCardList.Add(_card);
        // 从牌堆移出
        deckCardList.Remove(_card);

        return _card;

        // todo 加入手牌动画 展示卡牌
    }

    // 用牌
    public void PlayCard(BaseCard _card)
    {
        usedCardList.Add(_card);

        handCardList.Remove(_card);
        //todo  卡牌使用效果
    }

    // 弃牌 手牌进弃牌堆
    public void HandListToUsed()
    {
        PutAllList(handCardList, usedCardList);
    }  

    /// <summary>
    /// 卡组内卡数不足 将弃牌堆卡装回卡组 清空弃牌堆
    /// </summary>
    public void UsedToDeck()
    {
        PutAllList(usedCardList, deckCardList);
    }

    /// <summary>
    /// 牌堆导入到另一牌堆操作
    /// </summary>
    public void PutAllList(List<BaseCard> list1, List<BaseCard> list2)
    {
        for (int i = 0; i < list1.Count; i++)
        {
            list2.Add(list1[i]);
        }
        list1.Clear();
    }

    // 查看 展示剩余卡组卡牌
    public List<BaseCard> GetDeckList()
    {
        return deckCardList == null ? deckCardList : null;
    }

    // 查看 展示弃牌堆卡组
    public List<BaseCard> GetUsedList()
    {
        return usedCardList == null ? usedCardList : null;
    }

    // 查看 当前手牌
    public List<BaseCard> GetHandList()
    {
        return handCardList == null ? handCardList : null;
    }

    /// <summary>
    /// 战斗结束 清空 弃牌堆 手牌 起始牌堆
    /// </summary>
    public void InitAllCardList()
    {
        deckCardList.Clear();
        handCardList.Clear();
        usedCardList.Clear();
    }
    #endregion
}

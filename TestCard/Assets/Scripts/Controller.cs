using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lowdll;
using SlayCard;

public enum GAME_STATE
{
    LOGIN,      // 起始界面
    SELECT,     // 选择职业界面
    MAP,        // 选择地图
    Fight,      // 战斗中
    Reward,     // 奖励界面
    Pause,      // 暂停
    Over,       // 结束
}

public class Controller : Singleton<Controller> {

    [HideInInspector]
    public Model model;

    [HideInInspector]
    public View view;

    // 默认暂停
    public GAME_STATE state = GAME_STATE.Over;

    public BattleLogic battleLogic;
    
    protected override void Awake()
    {
        base.Awake();
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();

        battleLogic = new BattleLogic();

        InitStartGame();

        DontDestroyOnLoad(this);
    }

    public void IntoBattle(int _id)
    {
        battleLogic.ShowBattleUI(_id);
    }

    public void InitStartGame()
    {
        // 打开起始界面
        view.OpenLoginPage();

    }

	void Update () {
        //Debug.LogError(LowTest.GetZZZ(10, 10).ToString());
	}

    public Player PlayerInfo {
        get {
            return model.GetPlayer();
        }
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected override void Awake()
    {
        base.Awake();
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();

        InitStartGame();

        DontDestroyOnLoad(this);
    }

    public void InitStartGame()
    {
        // todo 打开起始界面
        view.OpenLoginPage();

    }

	void Update () {
		
	}
}

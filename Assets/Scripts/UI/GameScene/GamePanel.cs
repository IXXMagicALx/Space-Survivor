using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanel : BasePanel<GamePanel>
{
    #region 按键相关
    public UIButton QuitBtn;
    #endregion

    #region 计时相关
    public UILabel TimeCount;

    [HideInInspector]
    public float NowTime;
    #endregion

    #region 血条相关
    public List<HealthGridObject> HealthBar;
    #endregion

    public override void Init()
    {
        QuitBtn.onClick.Add(new EventDelegate(() =>
        {
            //显示确认面板
            ConfirmPanel.Instance.ShowPanel();

        }));

    }

    void Update()
    {
        #region 计时逻辑
        NowTime += Time.deltaTime;
        TimeCount.text = "";
        if ((int)NowTime / 3600 > 0)//小时
        {
            TimeCount.text += $"{(int)NowTime / 3600}h";
        }
        if (((int)NowTime % 3600) / 60 > 0 || TimeCount.text != "")//分钟
        {
            TimeCount.text += $"{((int)NowTime % 3600) / 60}min";
        }
        if ((int)NowTime % 60 > 0)//秒
        {
            TimeCount.text += $"{((int)NowTime % 60)}s";
        }
        #endregion
    }
    /// <summary>
    /// 更新血条
    /// </summary>
    /// <param name="health">当前血量</param>
    public void UpdateHealthBar(int health)
    {
        for(int i = 0; i < HealthBar.Count;i++)
        {
            HealthBar[i].IsFilled(i < health);
        }
    }
    
}

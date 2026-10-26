using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanel : BasePanel<EndPanel>
{
    #region 最终计时相关
    public UILabel FinalTime;
    #endregion

    #region 输入名字相关
    public UIInput EnterName;
    #endregion

    #region 按键相关
    public UIButton ConfirmBtn;
    #endregion

    public override void Init()
    {

        ConfirmBtn.onClick.Add(new EventDelegate(() =>
        {
            //记录一条新的游玩数据
            GameDataManager.Instance.AddSingleRankData(EnterName.label.text, (int)GamePanel.Instance.NowTime);
            //数据持久化游玩数据
            GameDataManager.Instance.SaveRankData();
            //关闭面板
            HidePanel();
            //返回开始场景
            SceneManager.LoadScene("BeginScene");
        }));

        HidePanel();

    }
    /// <summary>
    /// 显示面板同时暂停
    /// </summary>
    public override void ShowPanel()
    {
        base.ShowPanel();
        //暂停游戏
        Time.timeScale = 0;
        //初始化显示的时间
        FinalTime.text = GamePanel.Instance.TimeCount.text;
        
    }
    /// <summary>
    /// 关闭面板同时恢复
    /// </summary>
    public override void HidePanel()
    {
        base.HidePanel();
        //游戏恢复
        Time.timeScale = 1.0F;
    }
}

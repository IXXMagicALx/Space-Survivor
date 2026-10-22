using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : BasePanel<MenuPanel>
{
    #region 按键相关
    public UIButton StartBtn;
    public UIButton RankBtn;
    public UIButton SettingBtn;
    public UIButton QuitBtn;
    #endregion

    public override void Init()
    {
        StartBtn.onClick.Add(new EventDelegate(() =>
        {
            //显示角色选择面板

            //隐藏主菜单面板
            HidePanel();
        }));

        RankBtn.onClick.Add(new EventDelegate(() =>
        {
            //打开排行榜面板
            RankPanel.Instance.ShowPanel();

        }));

        SettingBtn.onClick.Add(new EventDelegate(() =>
        {
            //打开设置面板
            SettingPanel.Instance.ShowPanel();

        }));

        QuitBtn.onClick.Add(new EventDelegate(() =>
        {
            //退出游戏
            Application.Quit();
        }));

        
    }
}

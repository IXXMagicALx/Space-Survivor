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
            //进入游戏场景
            SceneManager.LoadScene("GameScene");
        }));

        RankBtn.onClick.Add(new EventDelegate(() =>
        {
            //打开排行榜面板


        }));

        SettingBtn.onClick.Add(new EventDelegate(() =>
        {
            //打开设置面板

        }));

        QuitBtn.onClick.Add(new EventDelegate(() =>
        {
            //退出游戏
            Application.Quit();
        }));

        
    }
}

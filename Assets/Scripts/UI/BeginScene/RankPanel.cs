using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    #region 按键相关
    public UIButton CloseBtn;
    #endregion

    #region 滚动视图相关
    public UIScrollView RankDataSV;
    #endregion

    /// <summary>
    /// 用来存放实例化的排行数据
    /// </summary>
    private List<SingleRankDataObject> objList = new List<SingleRankDataObject>();

    public override void Init()
    {
        CloseBtn.onClick.Add(new EventDelegate(() =>
        {
            //关闭排行面板
            HidePanel();

        }));

        HidePanel();

    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        //更新排行面板信息
        UpdatePanel();
    }
    /// <summary>
    /// 更新排行榜信息
    /// </summary>
    public void UpdatePanel()
    {
        //加载排行数据
        RankData rankData = GameDataManager.Instance.RankData;
        List<SingleRankData> rankDatas = rankData.RankDatas;

        for(int i = 0; i < rankDatas.Count;i++)
        {
            if(objList.Count > i)//如果已经存在一些对象则不用再重新实例化，直接修改内容即可
            {
                objList[i].InitInfo(i + 1,rankDatas[i].Name,rankDatas[i].Time);
            }
            else
            {
                //实例化对象
                GameObject gameObj = Instantiate(Resources.Load<GameObject>("UIObject/SingleRankData"));
                gameObj.transform.SetParent(RankDataSV.transform, false);
                gameObj.transform.localPosition = new Vector3(371, -447, 0) + new Vector3(0, i * (-80), 0);
                //修改显示信息
                SingleRankDataObject obj = gameObj.GetComponent<SingleRankDataObject>();
                obj.InitInfo(i + 1, rankDatas[i].Name, rankDatas[i].Time);
                //将实例化的对象存入列表
                objList.Add(obj);
            }
            
        }
    }
}

using UnityEngine;

public class GameDataManager
{
	#region 单例模式
	private static GameDataManager instance = new GameDataManager();
	public static GameDataManager Instance => instance;
	private GameDataManager()
	{
		//每次启动时加载数据
		VoiceData = XmlDataManager.Instance.LoadXmlData(typeof(VoiceData), "VoiceData") as VoiceData;
		RankData = XmlDataManager.Instance.LoadXmlData(typeof(RankData),"RankData") as RankData;
		AirplaneData = XmlDataManager.Instance.LoadXmlData(typeof(AirplaneData),"AirplaneData") as AirplaneData;
		BulletData = XmlDataManager.Instance.LoadXmlData(typeof(BulletData),"BulletData") as BulletData;
		TurretData = XmlDataManager.Instance.LoadXmlData(typeof(TurretData),"TurretData") as TurretData;

	}
	#endregion

	public VoiceData VoiceData;
	public RankData RankData;
	public AirplaneData AirplaneData;
	public BulletData BulletData;
	public TurretData TurretData;

	#region 声音相关方法
	/// <summary>
	/// 数据持久化声音数据
	/// </summary>
	public void SaveVoiceData()
	{
		XmlDataManager.Instance.SaveXmlData(VoiceData, "VoiceData");
	}
	/// <summary>
	/// 改变音乐音量大小
	/// </summary>
	/// <param name="value">新的音量</param>
	public void ChangeMusicVolume(float value)
	{
		VoiceData.MusicVolume = value;
	}
	/// <summary>
	/// 改变音效音量大小
	/// </summary>
	/// <param name="value">新的音量</param>
    public void ChangeSoundVolume(float value)
    {
        VoiceData.SoundVolume = value;
    }
	/// <summary>
	/// 开/关音乐
	/// </summary>
	/// <param name="isOpen">是否开启</param>
	public void OpenOrCloseMusic(bool isOpen)
	{
		VoiceData.MusicIsOpen = isOpen;
	}
	/// <summary>
	/// 开/关音效
	/// </summary>
	/// <param name="isOpen">是否开启</param>
	public void OpenOrCloseSound(bool isOpen)
	{
		VoiceData.SoundIsOpen = isOpen;
	}
	#endregion

	#region 排行榜相关方法
	/// <summary>
	/// 为排行榜添加数据
	/// </summary>
	/// <param name="name">玩家名称</param>
	/// <param name="time">完成时间</param>
	public void AddSingleRankData(string name,int time)
	{
		//添加数据
		RankData.RankDatas.Add(new SingleRankData(name, time));
        #region 排序整理排行榜数据
        //排序完成时间更长的越靠上
        RankData.RankDatas.Sort((a, b) =>
		{
			return a.Time > b.Time ? -1 : 1; 
		});
		//限制排行榜数据不能超过20个
		if(RankData.RankDatas.Count > 20) RankData.RankDatas.RemoveAt(20);
		#endregion
		//保存数据
		SaveRankData();
	}
    /// <summary>
    /// 数据持久化排行榜数据
    /// </summary>
    public void SaveRankData()
	{
		XmlDataManager.Instance.SaveXmlData(RankData, "RankData");
	}
	#endregion

	#region 选角相关方法
	/// <summary>
	/// 当前选择飞船索引
	/// </summary>
	public int NowSelectAirplaneIndex = 0;
	/// <summary>
	/// 切换到上一个飞船
	/// </summary>
	public void ChangeToLastAirplane()
	{
		
		if(NowSelectAirplaneIndex != 0)
		{
			//不是第一个切换就能得到上一个
			NowSelectAirplaneIndex--;
		}
		else
		{
			//是第一个切换到上一个就得到最后一个
			NowSelectAirplaneIndex = AirplaneData.AirplaneDatas.Count - 1;
		}
	}
	/// <summary>
	/// 切换到下一个飞船
	/// </summary>
	public void ChangeToNextAirplane()
	{
        if (NowSelectAirplaneIndex != AirplaneData.AirplaneDatas.Count - 1)
        {
            //不是最后一个切换就能得到下一个
            NowSelectAirplaneIndex++;
        }
        else
        {
            //是最后一个切换到下一个就得到第一个
            NowSelectAirplaneIndex = 0;
        }

    }

	#endregion
}

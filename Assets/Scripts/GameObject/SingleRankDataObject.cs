using UnityEngine;

public class SingleRankDataObject : MonoBehaviour
{
	#region 文本相关
	public UILabel RankingLabel;
	public UILabel NameLabel;
	public UILabel TimeLabel;
	#endregion
	/// <summary>
	/// 初始化单条排行榜数据自身信息
	/// </summary>
	/// <param name="ranking">排名</param>
	/// <param name="name">玩家名称</param>
	/// <param name="time">存活时间</param>
	public void InitInfo(int ranking, string name, int time)
	{
		this.RankingLabel.text = ranking.ToString();
		this.NameLabel.text = name;

		//将秒转换成时分秒格式
		string timeStr = "";
		if(time/3600 > 0)//小时
        {
			timeStr += $"{time / 3600}h";
		}
		if((time%3600)/60 > 0||timeStr!="")//分钟
		{
			timeStr += $"{(time % 3600) / 60}min";

        }
		if(time%60 > 0)//秒
		{
			timeStr += $"{(time % 60)}s";
		}

		this.TimeLabel.text = timeStr;

	}
}

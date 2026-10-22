using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
/// <summary>
/// 多条排行榜数据
/// </summary>
public class RankData
{
    /// <summary>
    /// 多条排行榜数据列表
    /// </summary>
    public List<SingleRankData> RankDatas = new List<SingleRankData>();
}
/// <summary>
/// 单条排行榜数据
/// </summary>
public class SingleRankData
{
    [XmlAttribute]
    public string Name;
    [XmlAttribute]
    public int Time;

    public SingleRankData() { }

    public SingleRankData(string name,int time)
    {
        this.Name = name;
        this.Time = time;
    }
}

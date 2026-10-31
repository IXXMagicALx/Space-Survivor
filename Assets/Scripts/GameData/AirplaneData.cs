using System.Collections.Generic;
using System.Xml.Serialization;
using NUnit.Framework;
using UnityEngine;

public class AirplaneData
{
    public List<SingleAirplaneData> AirplaneDatas = new List<SingleAirplaneData>(); 
}

public class SingleAirplaneData
{
    /// <summary>
    /// 在选角面板显示的缩放大小
    /// </summary>
    [XmlAttribute]
    public float Scale;
    /// <summary>
    /// 飞机的ID
    /// </summary>
    [XmlAttribute]
    public int ID;
    /// <summary>
    /// 飞机对应模型所在路径
    /// </summary>
    [XmlAttribute]
    public string ModelPath;
    /// <summary>
    /// 飞机血量
    /// </summary>
    [XmlAttribute]
    public int Hp;
    /// <summary>
    /// 飞机移动速度
    /// </summary>
    [XmlAttribute]
    public int MoveSpeed;
    /// <summary>
    /// 飞机水平飞行时旋转速度
    /// </summary>
    [XmlAttribute]
    public int RotateSpeed;
    /// <summary>
    /// 飞机体积
    /// </summary>
    [XmlAttribute]
    public int Volume;
}

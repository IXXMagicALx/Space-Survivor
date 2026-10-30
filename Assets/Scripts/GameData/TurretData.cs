using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
public enum E_TurretType
{
    /// <summary>
    /// 单发
    /// </summary>
    Single,
    /// <summary>
    /// 多发(延时或同时)
    /// </summary>
    Multi,
}

public class TurretData
{
    public List<SingleTurretData> TurretDatas = new List<SingleTurretData>();
}

public class SingleTurretData
{
    /// <summary>
    /// 炮台的ID
    /// </summary>
    [XmlAttribute]
    public int ID;
    /// <summary>
    /// 炮台的类型
    /// </summary>
    [XmlAttribute]
    public E_TurretType Type;
    /// <summary>
    /// 每组具有子弹的数量
    /// </summary>
    [XmlAttribute]
    public int Num;
    /// <summary>
    /// 每颗子弹发射的间隔时间(可以理解为射速) 
    /// </summary>
    [XmlAttribute]
    public float CD;
    /// <summary>
    /// 炮台发射子弹的ID随机范围 
    /// </summary>
    [XmlAttribute]
    public string BulletsID;
    /// <summary>
    /// 炮台每组子弹延时发射时间(可以理解为换弹时间)
    /// </summary>
    [XmlAttribute]
    public float Delay;

}


using NUnit.Framework;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
public enum E_BulletType
{
    Straight,
    Wave,
    LeftArc,
    RightArc,
    Homing,
}

public class BulletData
{
    public List<SingleBulletData> BulletDatas = new List<SingleBulletData>();
    
}

public class SingleBulletData
{
    [XmlAttribute]
    public E_BulletType Type;
    [XmlAttribute]
    public float ForwardSpeed;
    [XmlAttribute]
    public float RightSpeed;
    [XmlAttribute]
    public float RotateSpeed;
    [XmlAttribute]
    public string ResourcePath;
    [XmlAttribute]
    public string DeadEffect;
    [XmlAttribute]
    public float LifeTime;
    [XmlAttribute]
    public float HomingTime;

}

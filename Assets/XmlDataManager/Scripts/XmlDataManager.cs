using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XmlDataManager
{
    #region 单例模式
    private static XmlDataManager instance = new XmlDataManager();
    public static XmlDataManager Instance => instance;
    private XmlDataManager() { }
    #endregion

    /// <summary>
    /// 保存数据为Xml文件
    /// </summary>
    /// <param name="data">要保存的数据对象</param>
    /// <param name="fileName">保存文件名</param>
    public void SaveXmlData(object data,string fileName)
    {
        //保存文件的路径
        string path = $"{Application.persistentDataPath}/{fileName}.xml";

        using ( StreamWriter writer = new StreamWriter(path) )
        {
            //序列化器
            XmlSerializer s = new XmlSerializer(data.GetType());
            s.Serialize(writer,data);
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public object LoadXmlData(Type type,string fileName)
    {
        //文件加载路径
        string path = $"{Application.persistentDataPath}/{fileName}.xml";
        //先判断数据持久化路径是否存在文件可被加载
        if(!File.Exists(path))
        {
            //如果数据持久化路径中不存在文件可被加载
            //就去流动资源文件夹中加载默认配置
            path = $"{Application.streamingAssetsPath}/{fileName}.xml";
            if(!File.Exists(path))
            {
                //Debug.Log("未在任何资源文件夹中找到配置文件");
                //如果不存在默认配置就根据类型利用反射创建默认对象
                return Activator.CreateInstance(type);
            }
            //Debug.Log("从流动资源文件夹中获取");
        }
        //Debug.Log("从数据持久化文件夹中获取");

        using ( StreamReader reader = new StreamReader(path) )
        {
            //序列化器
            XmlSerializer s = new XmlSerializer(type);
            return s.Deserialize(reader);
        }
      
    }
    
}

using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class CustomDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
    public XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        //反序列化器
        XmlSerializer keyS = new XmlSerializer(typeof(TKey));
        XmlSerializer valueS = new XmlSerializer(typeof(TValue));

        //跳过根节点的头节点再读取
        reader.Read();

        while (reader.NodeType != XmlNodeType.EndElement)//读到根元素尾节点就跳出循环
        {
            //注意以下是通过Deserialize进行反序列化
            TKey key = (TKey)keyS.Deserialize(reader);
            TValue value = (TValue)valueS.Deserialize(reader);

            //存入字典
            this.Add(key, value);
        }

        //跳过根节点的尾节点，避免影响之后读取
        reader.Read();


    }

    public void WriteXml(XmlWriter writer)
    {
        //序列化器
        XmlSerializer keyS = new XmlSerializer(typeof(TKey));
        XmlSerializer valueS = new XmlSerializer(typeof(TValue));

        foreach (KeyValuePair<TKey, TValue> item in this)
        {
            //键序列化
            keyS.Serialize(writer, item.Key);
            //值序列化
            valueS.Serialize(writer, item.Value);
        }

    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public void Save()
    {
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");

        Player_Data data = new Player_Data
        {
            allItems = new List<SaveGameObject>()
        };

        foreach (Transform child in GameObject.Find("AllItems").GetComponent<Transform>())
        {
            if (child.gameObject.tag == "Root")
                continue;

            SaveGameObject sgo = new SaveGameObject(child.gameObject);

            data.allItems.Add(sgo);
        }

        data.camperSizeX = GameObject.Find("VanBase").GetComponent<Transform>().localScale.x;
        data.camperSizeY = GameObject.Find("VanBase").GetComponent<Transform>().localScale.z;

        DataContractSerializer bf = new DataContractSerializer(data.GetType());
        MemoryStream streamer = new MemoryStream();

        bf.WriteObject(streamer, data);
        streamer.Seek(0, SeekOrigin.Begin);

        file.Write(streamer.GetBuffer(), 0, streamer.GetBuffer().Length);

        file.Close();
    }

    public void Read()
    {
        string tempFile = Path.GetTempFileName();

        using (var sr = new StreamReader(Application.persistentDataPath + "/PlayerData.dat"))
        using (var sw = new StreamWriter(tempFile))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains(" xmlns=\"http://schemas.datacontract.org/2004/07/\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\""))
                {
                    string lineChanged = line.Replace(" xmlns=\"http://schemas.datacontract.org/2004/07/\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    sw.WriteLine(lineChanged);
                }
                else
                    sw.WriteLine(line);
            }
        }

        File.Delete(Application.persistentDataPath + "/PlayerData.dat");
        File.Move(tempFile, Application.persistentDataPath + "/PlayerData.dat");

        XmlSerializer serializer =
        new XmlSerializer(typeof(Player_Data));

        Player_Data playerData;

        using (Stream reader = new FileStream(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open))
        {
            playerData = (Player_Data)serializer.Deserialize(reader);
        }

        CamperCreator camperCreator = GameObject.Find("MenuGameController").GetComponent<CamperCreator>();

        camperCreator.SetCamper(playerData.camperSizeX.ToString(), playerData.camperSizeY.ToString());

        foreach (var item in playerData.allItems)
        {
            PopulateGrid populateGrid = GameObject.FindGameObjectWithTag("Content_PopulateGrid").GetComponent<PopulateGrid>();

            Vector3 position = new Vector3(item.PositionX, item.PositionY, item.PositionZ);
            Quaternion rotation = new Quaternion(item.RotationX, item.RotationY, item.RotationZ, item.RotationW);
            Vector3 scale = new Vector3(item.ScaleX, item.ScaleY, item.ScaleZ);

            populateGrid.InstantiateResource(item.TagName, position, rotation, scale);
        }
    }
}

[DataContract]
[XmlRoot("Player_Data")]
public class Player_Data
{
    [DataMember]
    [XmlArray("allItems")]
    [XmlArrayItem(typeof(SaveGameObject))]
    public List<SaveGameObject> allItems;

    [DataMember]
    [XmlElement("camperSizeX")]
    public float camperSizeX;

    [DataMember]
    [XmlElement("camperSizeY")]
    public float camperSizeY;
}

[DataContract]
[XmlRoot("SaveGameObject")]
public class SaveGameObject
{
    public SaveGameObject() { }
    public SaveGameObject(GameObject go)
    {
        TagName = go.tag;

        PositionX = go.transform.position.x;
        PositionY = go.transform.position.y;
        PositionZ = go.transform.position.z;

        RotationX = go.transform.rotation.x;
        RotationY = go.transform.rotation.y;
        RotationZ = go.transform.rotation.z;
        RotationW = go.transform.rotation.w;

        ScaleX = go.transform.localScale.x;
        ScaleY = go.transform.localScale.y;
        ScaleZ = go.transform.localScale.z;
    }

    [DataMember]
    [XmlElement("TagName")]
    public string TagName { set; get; }

    [DataMember]
    public float PositionX { set; get; }
    [DataMember]
    public float PositionY { set; get; }
    [DataMember]
    public float PositionZ { set; get; }

    [DataMember]
    public float RotationX { set; get; }
    [DataMember]
    public float RotationY { set; get; }
    [DataMember]
    public float RotationZ { set; get; }
    [DataMember]
    public float RotationW { set; get; }

    [DataMember]
    public float ScaleX { set; get; }
    [DataMember]
    public float ScaleY { set; get; }
    [DataMember]
    public float ScaleZ { set; get; }
}

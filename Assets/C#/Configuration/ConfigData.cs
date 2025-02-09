using AYellowpaper.SerializedCollections;
using UnityEngine;

public class ConfigData :MonoBehaviour
{
    //存储文本信息及对应预制卡片对象   
    public  SerializedDictionary<string,GameObject> pictureText = new SerializedDictionary<string, GameObject>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessToData {

    private Dictionary<string, string> dataDic;
    private List<Dictionary<string, string>> dataList;

    public AccessToData() {
        dataDic = new Dictionary<string, string>();
        dataList = new List<Dictionary<string, string>>();
    }

    public List<Dictionary<string, string>> accessData(JSONObject obj, string lastKeys) {
        
        switch (obj.type) {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++) {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];
                    Debug.Log("<Color=red>"+key+"</Color>");
                    lastKeys = key;
                    accessData(j, lastKeys);
                }
                break;
            case JSONObject.Type.ARRAY:
                foreach (JSONObject j in obj.list) {
                    dataList = accessData(j, lastKeys);
                    dataList.Add(dataDic);
                    Debug.Log("<Color=blue>" + dataList.Count + "</Color>");
                    dataDic = new Dictionary<string, string>();
                }
                break;
            case JSONObject.Type.STRING:
                Debug.Log(obj.str);
                Debug.Log("<Color=red>" + obj.str + "</Color>");
                dataDic.Add(lastKeys, obj.str);
                break;
            case JSONObject.Type.NUMBER:
                Debug.Log(obj.n);
                break;
            case JSONObject.Type.BOOL:
                Debug.Log(obj.b);
                break;
            case JSONObject.Type.NULL:
                Debug.Log("NULL");
                break;
        }
        return dataList;
    }
}


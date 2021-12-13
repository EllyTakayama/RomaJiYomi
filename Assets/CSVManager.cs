using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//12月12日更新CSVファイル取得用

public class CSVManager : MonoBehaviour
{
    private List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト

    // Start is called before the first frame update
    void Start()
    {
        TextAsset csv = Resources.Load("Chimei") as TextAsset;
        StringReader reader = new StringReader(csv.text);
        while (reader.Peek() > -1) {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(',')); // リストに入れる
        }
        for (var x = 0; x < csvDatas.Count; x++)
       {
       for (var y = 0; y < csvDatas[x].Length; y++)
       {
         Debug.Log(csvDatas[x][y]);
         }
         }
    }
}

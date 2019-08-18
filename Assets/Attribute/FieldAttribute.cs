using UnityEngine;
using System.Collections.Generic;
public class FieldAttribute
{
    public enum Type
    {
        Farmland,               //  農地
        Plowed_farmland,        //  耕された農地
        Wasteland,              //  荒地
        Road,                   // 道
    }

    /// <summary>
    /// 地面ブロックに適応する色
    /// todo : 将来的にはイメージを用意して差し替えるようにしたい
    /// </summary>
    /// <typeparam name="Color"></typeparam>
    /// <returns></returns>
    public static List<Color32> FieldColor = new List<Color32>()
    {
        new Color32(255,160,0,255),     //  農地
        new Color32(140,80,0,255),      //  耕された農地
        new Color32(110,180,100,255),   //  荒地
        new Color32(100,100,0,255),     //  道
    };
}

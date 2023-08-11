using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
public class MapSize : MonoBehaviour
{
    public InputField Mwid, Mhei;
    public void MapProportion()
    {
        if (int.TryParse(Mwid.text, out _) && int.TryParse(Mhei.text, out _))
        {
            //if (int.Parse(Mwid.text) > 0 && int.Parse(Mhei.text) > 0)
            //{
            //    if (54 / int.Parse(Mwid.text) >= 20 / int.Parse(Mhei.text))
            //    {
            //        GetComponent<RectTransform>().localScale = new Vector3(float.Parse(Mwid.text) / float.Parse(Mhei.text), 1, 0);
            //    }
            //    else
            //    {
            //        GetComponent<RectTransform>().localScale = new Vector3(1, float.Parse(Mhei.text) / float.Parse(Mwid.text), 0);
            //    }
            //}
            if (int.Parse(Mwid.text) <= int.Parse(Mhei.text))
            {
                GetComponent<RectTransform>().localScale = new Vector3(float.Parse(Mwid.text) / float.Parse(Mhei.text), 1, 0);
            }
            else
            {
                GetComponent<RectTransform>().localScale = new Vector3(1, float.Parse(Mhei.text) / float.Parse(Mwid.text), 0);
            }
        }
    }
}

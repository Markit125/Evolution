using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
public class BactSize : MonoBehaviour
{
    public InputField wid, hei, predation;
    public void BactProportion()
    {
        if (int.TryParse(wid.text, out _) && int.TryParse(hei.text, out _))
        {
            if (int.Parse(wid.text) > 0 && int.Parse(hei.text) > 0)
            {
                if (170 / int.Parse(wid.text) >= 240 / int.Parse(hei.text)) GetComponent<RectTransform>().localScale = new Vector3(2 * float.Parse(wid.text) / float.Parse(hei.text), 1, 0);
                else GetComponent<RectTransform>().localScale = new Vector3(1, float.Parse(hei.text) / float.Parse(wid.text) / 2, 0);
            }
        }
        if (float.TryParse(predation.text, out _))
        {
            if (float.Parse(predation.text) >= 0) GetComponent<Image>().color = new Color(float.Parse(predation.text) / 100, 1 - float.Parse(predation.text) / 100, 0f, 1f);
        }
    }
}

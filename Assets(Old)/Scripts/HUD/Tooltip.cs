using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private GameObject _toolTipObject;
    private Text _toolTip;

    void Start()
    {
        _toolTipObject = GameObject.Find("ToolTip");
        _toolTipObject.SetActive(false);
        _toolTip = GetComponentInChildren<Text>();
        _toolTip.gameObject.SetActive(false);
    }

    public void GenerateToolTip(Item item)
    {
        string statText = "";
        if (item.stats.Count > 0)
        {
            foreach (var stat in item.stats)
            {
                statText += stat.Key.ToString() + ": " + stat.Value.ToString() + "\n";
            }
        }
        string toolTipString = string.Format("<b>{0}</b>\n{1}\n\n<b>{2}</b>", item.title, item.description, statText);
        
        _toolTip.text = toolTipString;
        _toolTip.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

}

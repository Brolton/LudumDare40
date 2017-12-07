using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

// this thing is the crown juwele of dirty code in this project
class Localiser : MonoBehaviour
{
    static string data;

    static Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
    static bool dictFilled = false;

    public Text[] texts = new Text[] { };

    static int languageId = -1;

    void Start()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                languageId = -1;
                break;
            case SystemLanguage.Italian:
                languageId = 0;
                break;
            case SystemLanguage.German:
                languageId = 1;
                break;
            case SystemLanguage.Slovak:
                languageId = 2;
                break;
            case SystemLanguage.Russian:
                languageId = 3;
                break;
            case SystemLanguage.Hebrew:
                languageId = 4;
                break;
            case SystemLanguage.Czech:  // Czech and Slovak are very similiar
                languageId = 2;
                break;
            default:
                languageId = -1;
                break;
        }

        data = Resources.Load<TextAsset>("Localisation").text.Replace(@"""""""", @"""").Replace(@"""""", @"""");
        Debug.Log("language id " + languageId);
        foreach (var text in texts)
        {
            LocaliseUIElement(text);
        }
    }

    static public void LocaliseUIElement(Text obj)
    {
        if (!dictFilled)
        {
            var lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var words = line.Split(',');
                var localised = new string[words.Length - 1];
                for (int i = 1; i < words.Length; i++)
                {
                    localised[i - 1] = words[i];
                }
                dict[words[0]] = localised;
            }

            dictFilled = true;
        }

        //Application.systemLanguage == SystemLanguage.
        if (obj != null)
        {
            var english = obj.text;
            try
            {
                var lines = obj.text.Split(new string[] { "\n" }, StringSplitOptions.None);
                obj.text = "";
                if (lines.Length == 0)
                {
                    obj.text = dict[obj.text][languageId];
                }
                else
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (dict.ContainsKey(lines[i]) && !dict[lines[i]].Equals(""))
                            obj.text += dict[lines[i]][languageId] + "\n";
                        else
                            obj.text += lines[i] + "\n";
                    }
                }

            }
            catch (System.Exception)
            {
                obj.text = english; // fall back
            }
        }
    }
}
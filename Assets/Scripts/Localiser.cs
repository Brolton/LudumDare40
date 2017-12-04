using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

// this thing is the crown juwele of dirty code in this project
class Localiser : MonoBehaviour
{
    const string data = @"ENGLISH,ITALIAN,GERMAN,SLOVAK,RUSSIAN,HEBREW,SPANISH
SFX Volume,Volume Suoni,Geräuschlautstärke,Hlasitosť efektov,Громкость звука,עוצמת אפקטים,
Music Volume,Volume Musica,Musiklautstärke,Hlasitosť hudby,Громкость музыки,עוצמת מוזיקה,
Credits,Riconoscimenti,Mitwirkende,Autory,Авторы,קרדיטים,
Options,Opzioni,Optionen,Nastavenia,Настройки,אפשרויות,
Main Menu,Menù principale,Hauptmenü,Hlavná ponuka,Главное меню,תפריט ראשי,
Retry,Riprova,Nochmal versuchen,Skúsiť znovu,Повторить,נסה שוב,
Game over,Hai perso,Game Over,Koniec hry,Конец игры,משחק נגמר,
Escape,Fuggi,Flucht,Odísť,Выход,בריחה,
Play,Gioca,Spielen,Hrať,Играть,שחק,
,,,,,,
ART,GRAFICA,GRAFIK,GRAFIKA,ГРАФИКА,ארט,
ADDITIONAL ART,GRAFICA AGGIUNTIVA,WEITERE GRAFIKEN,DODATOČNÁ GRAFIKA,ДОП. ГРАФИКА,ארט נוסף,
PROGRAMMING,PROGRAMMAZIONE,PROGRAMMIERUNG,PROGRAMOVANIE,ПРОГРАММИРОВАНИЕ,תכנות,
MANAGEMENT,COORDINAMENTO,KOORDINATION,VEDENIE,МЕНЕДЖМЕНТ,ניהול,
SOUND,SONORO,AUDIO,ZVUK,ЗВУК,סאונד,
ANIMATION,ANIMAZIONE,ANIMATION,ANIMÁCIA,АНИМАЦИЯ,אנימציה,";

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
            default:
                languageId = -1;
                break;
        }
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
            var lines = data.Split('\n');
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
                obj.text = dict[obj.text][languageId];

            }
            catch (System.Exception)
            {
                obj.text = english; // fall back
            }
        }
    }
}
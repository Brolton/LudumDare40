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
    const string data = @"ENGLISH,ITALIAN,GERMAN,SLOVAK,RUSSIAN,HEBREW,SPANISH
SFX Volume,Volume Suoni,Geräuschlautstärke,Hlasitosť efektov,Громкость звука,עוצמת אפקטים,Volumen Sonido
Music Volume,Volume Musica,Musiklautstärke,Hlasitosť hudby,Громкость музыки,עוצמת מוזיקה,Volumen Musica
Credits,Riconoscimenti,Mitwirkende,Autory,Авторы,קרדיטים,Los Créditos
Options,Opzioni,Optionen,Nastavenia,Настройки,אפשרויות,Opciónes
Main Menu,Menù principale,Hauptmenü,Hlavná ponuka,Главное меню,תפריט ראשי,Menú Principal
Retry,Riprova,Nochmal versuchen,Skúsiť znovu,Повторить,נסה שוב, Reintenta
Game over,Hai perso,Game Over,Koniec hry,Конец игры,משחק נגמר,Fin Del Juego
Escape,Fuggi,Flucht,Odísť,Выход,בריחה,La Fuga
Play,Gioca,Spielen,Hrať,Играть,שחק,Juega
,,,,,,
ART,GRAFICA,GRAFIK,GRAFIKA,ГРАФИКА,ארט,ARTES
ADDITIONAL ART,GRAFICA AGGIUNTIVA,WEITERE GRAFIKEN,DODATOČNÁ GRAFIKA,ДОП. ГРАФИКА,ארט נוסף,ARTES ADICIONALES
PROGRAMMING,PROGRAMMAZIONE,PROGRAMMIERUNG,PROGRAMOVANIE,ПРОГРАММИРОВАНИЕ,תכנות,PROGRAMACIÓN
MANAGEMENT,COORDINAMENTO,KOORDINATION,VEDENIE,МЕНЕДЖМЕНТ,ניהול,DIRECCIÓN
SOUND,SONORO,AUDIO,ZVUK,ЗВУК,סאונד,SONIDO
ANIMATION,ANIMAZIONE,ANIMATION,ANIMÁCIA,АНИМАЦИЯ,אנימציה,ANIMACIÓN
,,,,,,
Controls:,,Steuerung,Ovládanie,,,Controles
WASD,WASD,WASD,WASD,,,WASD
Arrow Keys,Frecce,Pfeiltasten,Šípky,,,Teclas de Flecha
Scroll-wheel,Rotellina del mouse,Mausrad,Koliesko myši,,,Rueda de Rollo
""U"" and ""I"",""U"" e ""I"",""U"" und ""I"", ""U"" a ""I"",,,""U"" y ""I""
Keys to change lantern size,tasti per cambiare il raggio della lanterna,Tasten um die Größe der Laterne zu ändern,Tlačítka na zmenu veľkosti lampáša,,,Claves para cambiar el tamaño de la linterna.
Keep monsters out of your lantern range to keep your heartrate low,mantieni i mostri al di fuori del raggio della lanterna per mantenere il tuo battito lento,Halte Monster aus der Reichweite deiner Laterne, um deinen Herzschlag niedrig zu halten,Drž monštrá mimo dosah svetla tvojho lampáša, aby sa ti nezvyšoval srdečný tep,,,Mantenga a los monstruos fuera del alcance de su linterna para mantener baja su frecuencia cardíaca.
Collect Fireflies by walking on them,Raccogli le lucciole raggiungendo la loro posizione,Samle Glühwürmchen indem du sie berührst,Zbieraj svetlušky tým, že prejdeš cez ne,,,Recolecta luciérnagas por caminando sobre ellas.";

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
        //languageId = 3;
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
                        if (dict.ContainsKey(lines[i]))
                            obj.text += dict[lines[i]][languageId] + "\n";
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
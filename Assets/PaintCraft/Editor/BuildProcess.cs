﻿// C# example.
using UnityEditor;
using System.Diagnostics;
using UnityEngine;

public class ScriptBatch
{

    [MenuItem("PaintCraft/Build Android")]
    public static void BuildAndroid()
    {
        // Get filename.
        string path = "/Users/par/Dropbox";
        string name = "/PaintCraft.apk";
        string[] levels = new string[]
        {
            "Assets/PaintCraft/Scenes/skins.unity",
            "Assets/PaintCraft/Scenes/main.unity"
        };


        // Build player.
        BuildPipeline.BuildPlayer(levels, path + name, BuildTarget.Android, BuildOptions.None);

        // Copy a file from the project folder to the build folder, alongside the built game.
        // FileUtil.CopyFileOrDirectory("Assets/Templates/Readme.txt", path + "Readme.txt");

        // Run the game (Process class from System.Diagnostics).
        // Process proc = new Process();
        // proc.StartInfo.FileName = path + "BuiltGame.exe";
        // proc.Start();
    }


    [InitializeOnLoad]
    public class PreloadSigningAlias
    {
        static PreloadSigningAlias()
        {
            PlayerSettings.Android.keystorePass = "prelude99";
            PlayerSettings.Android.keyaliasName = "paintcraft";
            PlayerSettings.Android.keyaliasPass = "prelude99";
        }
    }

    [MenuItem("PaintCraft/Delete All Prefs")]
    public static void DeleteAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
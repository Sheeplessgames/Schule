using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class DeleteSave : MonoBehaviour
{
    private static string SaveFilePath
    {
        get { return Application.persistentDataPath + "/player.sav"; }
    }
    [MenuItem("Tools / Delete Player Save")]
    public static void DeleteData()
    {
        File.Delete(SaveFilePath);
    }
}

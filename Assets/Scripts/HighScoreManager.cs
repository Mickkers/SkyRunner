using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public GameData gameData;
    private string Path => Application.persistentDataPath + "/data.dat";

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(Path))
        {
            LoadBinary();
        }
        else
        {
            gameData = new GameData();
            SaveBinary();
        }
    }

    public void NewScore(int score)
    {
        int[] _temp = gameData.highScores;
        Array.Resize(ref _temp, _temp.Length + 1);
        _temp[10] = score;
        Array.Sort(_temp);
        Array.Reverse(_temp);
        int[] newHighScores = new int[10];
        for (int i = 0; i < 10; i++)
        {
            newHighScores[i] = _temp[i];
        }
        gameData.highScores = newHighScores;
        SaveBinary();
    }

    public int[] GetHighScores()
    {
        return gameData.highScores;
    }

    public void SaveBinary()
    {
        FileStream file = File.OpenWrite(Path);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, gameData);
        file.Close();
    }

    public void LoadBinary()
    {
        FileStream file = File.OpenRead(Path);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        gameData = (GameData)binaryFormatter.Deserialize(file);
        file.Close();
    }
}

[Serializable]
public class GameData
{
    public int[] highScores = new int[10];
}
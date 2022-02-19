using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//Link to the tutorial for this made by Brackeys: https://www.youtube.com/watch?v=XOjd_qU2Ido
public static class StatsManager
{
    public static string saveFile = Application.persistentDataPath + DifficultyManager.difficulty + "playerStats.stats";
    public static void SaveStats(Stats stats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveData = new FileStream(saveFile, FileMode.Create);
        PlayerStats playerStats = new PlayerStats(stats);
        formatter.Serialize(saveData, playerStats);
        saveData.Close();
    }
    public static PlayerStats LoadStats()
    {
        if (File.Exists(saveFile))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveData = new FileStream(saveFile, FileMode.Open);
            PlayerStats loadedStats = formatter.Deserialize(saveData) as PlayerStats;
            saveData.Close();
            Debug.Log("Hint Count: " + loadedStats.hintCount + ", Current Streak: " + loadedStats.currentStreak + ", Highest Streak: " + loadedStats.highestStreak + ", Difficulty: " + loadedStats.difficulty);
            return loadedStats;
        }
        else
        {
            Debug.LogError("Error: Could not find a save file!");
            return null;
        }
    }
}

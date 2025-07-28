using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName){
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullpath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullpath))
        {
            try
            {
                string dataToLoad = "";
                //Get data from file
                using (FileStream stream = new FileStream(fullpath, FileMode.Open)) { using (StreamReader reader = new StreamReader(stream)) { reader.ReadToEnd(); } }

                //Deserialize data from Json to C#
                loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error while loading data" + fullpath + "\n" + e);
            }
        }
        return loadedData;

    }

    public void Save(GameData data){
        string fullpath = Path.Combine(dataDirPath, dataFileName);
        try{
            //Create Directory
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));

            //Serialize C# GameData into json
            string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented);

            //write to the filesystem
            using (FileStream stream = new FileStream(fullpath, FileMode.Create)){
                using (StreamWriter writer = new StreamWriter(stream)){
                    writer.Write(dataToStore);
                }
            }
        } catch (Exception e){
            Debug.LogError("Error while saving data" + fullpath + "\n" + e);
        }
    }
}

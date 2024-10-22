using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance {get; private set;}

    private void Awake() {
        if (instance != null) {
            Debug.LogError("Found multiple data persistence managers in the scene");
        }
        instance = this;
    }

    private void Start() {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        Debug.Log("Start");
        //LoadGame();
    }

    public void NewGame() {
        Debug.Log("New Game");
        this.gameData = new GameData();
        dataHandler.Save(gameData);
    }

    public void LoadGame() {
        Debug.Log("Loading Game");
        this.gameData = dataHandler.Load();

        if (this.gameData == null) {
            Debug.Log("No data found. Initializing new game");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            //Debug.Log("Loaded data" + dataPersistenceObj);
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame() {
        Debug.Log("Saving Game");
        if (gameData == null) {
            gameData = new GameData();
            //Debug.LogError("GameData is null. Cannot save game.");
            //return;
        }
        Debug.Log("dataPersistenceObjects: " + dataPersistenceObjects);
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            Debug.Log("Saved: " + dataPersistenceObj);
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() {
        IEnumerable<IDataPersistence> dp = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dp);
    }
}

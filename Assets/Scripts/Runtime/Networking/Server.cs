using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class Server : MonoBehaviour
{
    [SerializeField] private string multiplayerSceneName = "MultiplayerScene"; // Name of the scene to load

    void Start()
    {
        // Start the server automatically in a server build
        if (IsServerBuild())
        {
            StartServer();
        }
    }

    void StartServer()
    {
        // Ensure NetworkManager is initialized
        if (NetworkManager.Singleton == null)
        {
            Log("NetworkManager is not initialized. Cannot start server.");
            return;
        }

        // Subscribe to the client connected event
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

        // Start the server
        NetworkManager.Singleton.StartServer();
        Log("Server started!");

        // Start the coroutine to load the MultiplayerScene after 3 seconds
        StartCoroutine(LoadMultiplayerSceneAfterDelay(3));
    }

    IEnumerator LoadMultiplayerSceneAfterDelay(float delay)
    {
        Log($"Waiting for {delay} seconds before loading the MultiplayerScene...");
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        // Load the MultiplayerScene
        Log($"Loading scene: {multiplayerSceneName}");
        NetworkManager.Singleton.SceneManager.LoadScene(multiplayerSceneName, LoadSceneMode.Single);
    }

    void OnClientConnected(ulong clientId)
    {
        string message = $"Player with Client ID {clientId} connected.";
        Log(message);
    }

    void OnApplicationQuit()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
        Log("Server is quitting...");
    }

    bool IsServerBuild()
    {
#if UNITY_SERVER
        return true;
#else
        return false;
#endif
    }

    // Log to terminal
    void Log(string message)
    {
        string logMessage = $"{DateTime.Now}: {message}";
        Console.WriteLine(logMessage); // Log to terminal
    }
}
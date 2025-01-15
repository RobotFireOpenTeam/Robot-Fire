using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    // [SerializeField] private Button serverBtn;
    [SerializeField] private Button _hostBtn;
    [SerializeField] private Button _clientBtn;
    
    [SerializeField]
    private string _sceneName;

    private void Awake(){
        // serverBtn.onClick.AddListener(() =>{
        //     StartServerAndLoadScene();
        // });
        _hostBtn.onClick.AddListener(() =>{
            StartHostAndLoadScene();
        });
        _clientBtn.onClick.AddListener(() =>{
            NetworkManager.Singleton.StartClient();
        });
    }
    
    private void StartServerAndLoadScene()
    {
        if (NetworkManager.Singleton.StartServer())
        {
            Debug.Log("Server started successfully.");
            NetworkManager.Singleton.SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Failed to start server!");
        }
    }

    private void StartHostAndLoadScene()
    {
        // Start the host
        if (NetworkManager.Singleton.StartHost())
        {
            Debug.Log("Host started successfully.");
            NetworkManager.Singleton.SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Failed to start host!");
        }
    }

    private void LoadScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            // Use NetworkSceneManager to load the scene
            NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}

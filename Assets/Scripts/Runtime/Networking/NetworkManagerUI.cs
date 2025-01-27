using Unity.Netcode;
using Unity.Netcode.Transports.UTP; 
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
            ConnectToServer();
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

    
    private void ConnectToServer()
    {
        // Set the server IP and port
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport != null)
        {
            transport.SetConnectionData("127.0.0.1", 7777);
        }

        // Start the client
        if (NetworkManager.Singleton.StartClient())
        {
            Debug.Log("Client started successfully.");
        }
        else
        {
            Debug.LogError("Failed to start client!");
        }
    }
}

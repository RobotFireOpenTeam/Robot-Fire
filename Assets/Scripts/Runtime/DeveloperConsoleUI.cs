using UnityEngine;
using UnityEngine.UI;

public class DeveloperConsoleUI : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private GameObject console;
    
    private void Awake(){
        closeBtn.onClick.AddListener(() =>{
            console.SetActive(false);
        });
    }

    
    
    public void OpenConsole() {
        console.SetActive(true);
    }
    public void CloseConsole() {
        console.SetActive(false);
    }
}

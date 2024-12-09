using TMPro;
using UnityEngine;

public class Armorbar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI staticInfo;
    [SerializeField] private TextMeshProUGUI dynamicInfo;
    [SerializeField] private Trooper trooper;
    
    private int bufferHP;
    private int initialInfo;

    public TextMeshProUGUI DynamicInfo { get => dynamicInfo; set => dynamicInfo = value; }
    public TextMeshProUGUI StaticInfo { get => staticInfo; set => staticInfo = value; }
    public int BufferHP { get => bufferHP; set => bufferHP = value; }

    void Start()
    {
        initialInfo = trooper.InitialArmor;
        StaticInfo.text = initialInfo.ToString();
        DynamicInfo.text = initialInfo.ToString();
    }

    void DynamicInfoChange(int newValue)
    {
        DynamicInfo.text = bufferHP.ToString();
    }
}

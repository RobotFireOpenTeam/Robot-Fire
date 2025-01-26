using TMPro;
using UnityEngine;

public class Armorbar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _staticInfo;
    [SerializeField] private TextMeshProUGUI _dynamicInfo;
    [SerializeField] private Trooper _trooper;
    
    private int _bufferHP;
    private int _initialInfo;

    public TextMeshProUGUI DynamicInfo { get => _dynamicInfo; set => _dynamicInfo = value; }
    public TextMeshProUGUI StaticInfo { get => _staticInfo; set => _staticInfo = value; }
    public int BufferHP { get => _bufferHP; set => _bufferHP = value; }

    void Start()
    {
        _initialInfo = _trooper.InitialArmor;
        StaticInfo.text = _initialInfo.ToString();
        DynamicInfo.text = _initialInfo.ToString();
    }

    void DynamicInfoChange(int newValue)
    {
        DynamicInfo.text = _bufferHP.ToString();
    }
}

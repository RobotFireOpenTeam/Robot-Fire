using UnityEngine;

public class TestGraph : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    // Update is called once per frame
    void Update()
    {
        Keyframe keyframe = new Keyframe(Time.time, transform.position.x, 0,0,0,0);
        _curve.AddKey(keyframe);
    }
}

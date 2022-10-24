using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TVNoizeMover : MonoBehaviour
{
    [SerializeField] private Grain grain;
    private bool upDown = true;
    private float value;
    private void Awake()
    {
        grain = GetComponent<PostProcessVolume>().profile.GetSetting<Grain>();
    }
    private void Update()
    {
        if (upDown)
        {
            value += Time.deltaTime / 10;

        }
        else
        {
            value -= Time.deltaTime / 10;
        }
        if (value < 0) upDown = true;
        else if (value > 1) upDown = false;
        grain.lumContrib.Override(value);
    }
}

using UnityEngine;

[System.Serializable]
public class ConfigData
{
    private float _interactRadius;

    public ConfigData(float interactRadius)
    {
        _interactRadius = interactRadius;
    }

    public float GetInteractRadius()
    {
        return _interactRadius;
    }

    public void SetInteractRadius(float radius)
    {
        _interactRadius = radius;
    }
}
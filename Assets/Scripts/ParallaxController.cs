using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [Tooltip("Объект слоя со спрайтом (достаточно одной картинки)")]
    public Transform layerTransform;

    [Tooltip("Дальность (0 - передний план; 1 - стоит на месте)")]
    public float parallaxFactor;

    [HideInInspector] public float startPos;
    [HideInInspector] public float length;
}

public class ParallaxController : MonoBehaviour
{
    public Transform cam;
    public ParallaxLayer[] layers;

    void Start()
    {
        if (cam == null) cam = Camera.main.transform;

        foreach (ParallaxLayer layer in layers)
        {
            if (layer.layerTransform == null) continue;

            SpriteRenderer sr = layer.layerTransform.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                layer.length = sr.bounds.size.x;
                
                CreateClone(sr, layer.layerTransform, layer.length, "RightClone");
                CreateClone(sr, layer.layerTransform, -layer.length, "LeftClone");
            }
            else
            {
                Debug.LogWarning($"На слое {layer.layerTransform.name} нет SpriteRenderer!");
            }

            layer.startPos = layer.layerTransform.position.x;
        }
    }

    void CreateClone(SpriteRenderer originalSr, Transform parent, float worldOffsetX, string cloneName)
    {
        GameObject clone = new GameObject(parent.name + "_" + cloneName);

        clone.transform.position = new Vector3(parent.position.x + worldOffsetX, parent.position.y, parent.position.z);
        clone.transform.SetParent(parent, true);

        SpriteRenderer cloneSr = clone.AddComponent<SpriteRenderer>();
        cloneSr.sprite = originalSr.sprite;
        cloneSr.color = originalSr.color;
        cloneSr.sortingLayerID = originalSr.sortingLayerID;
        cloneSr.sortingOrder = originalSr.sortingOrder;

        cloneSr.material = originalSr.material;
    }

    void LateUpdate()
    {
        foreach (ParallaxLayer layer in layers)
        {
            if (layer.layerTransform == null) continue;

            float temp = cam.position.x * (1 - layer.parallaxFactor);
            float dist = cam.position.x * layer.parallaxFactor;

            layer.layerTransform.position = new Vector3(
                layer.startPos + dist,
                layer.layerTransform.position.y,
                layer.layerTransform.position.z
            );

            if (temp > layer.startPos + layer.length)
            {
                layer.startPos += layer.length;
            }
            else if (temp < layer.startPos - layer.length)
            {
                layer.startPos -= layer.length;
            }
        }
    }
}
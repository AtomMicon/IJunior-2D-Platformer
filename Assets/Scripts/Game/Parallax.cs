using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform LayerTransform;
    
    [Tooltip("Дальность (0 - передний план; 1 - стоит на месте)")]
    public float ParallaxFactor;

    [HideInInspector] public float StartPos;
    [HideInInspector] public float Length;
}

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private ParallaxLayer[] _layers;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main.transform;
        }

        foreach (ParallaxLayer layer in _layers)
        {
            if (layer.LayerTransform != null)
            { 
                SpriteRenderer spriteRenderer = layer.LayerTransform.GetComponent<SpriteRenderer>();
                
                if (spriteRenderer != null)
                {
                    layer.Length = spriteRenderer.bounds.size.x;
                    
                    CreateClone(spriteRenderer, layer.LayerTransform, layer.Length, "RightClone");
                    CreateClone(spriteRenderer, layer.LayerTransform, -layer.Length, "LeftClone");
                }
                else
                {
                    Debug.LogWarning($"На слое {layer.LayerTransform.name} нет SpriteRenderer!");
                }

                layer.StartPos = layer.LayerTransform.position.x;
            }
        }
    }

    private void LateUpdate()
    {
        foreach (ParallaxLayer layer in _layers)
        {
            if (layer.LayerTransform != null)
            {
                float temp = _camera.position.x * (1 - layer.ParallaxFactor);
                float distance = _camera.position.x * layer.ParallaxFactor;

                layer.LayerTransform.position = new Vector3(
                    layer.StartPos + distance,
                    layer.LayerTransform.position.y,
                    layer.LayerTransform.position.z
                );

                if (temp > layer.StartPos + layer.Length)
                {
                    layer.StartPos += layer.Length;
                }
                else if (temp < layer.StartPos - layer.Length)
                {
                    layer.StartPos -= layer.Length;
                }
            }
        }
    }

    private void CreateClone(SpriteRenderer originalSpriteRenderer, Transform parent, float worldOffsetX, string cloneName)
    {
        GameObject clone = new GameObject(parent.name + "_" + cloneName);

        clone.transform.position = new Vector3(parent.position.x + worldOffsetX, parent.position.y, parent.position.z);
        clone.transform.SetParent(parent, true);

        SpriteRenderer cloneSpriteRenderer = clone.AddComponent<SpriteRenderer>();
        cloneSpriteRenderer.sprite = originalSpriteRenderer.sprite;
        cloneSpriteRenderer.color = originalSpriteRenderer.color;
        cloneSpriteRenderer.sortingLayerID = originalSpriteRenderer.sortingLayerID;
        cloneSpriteRenderer.sortingOrder = originalSpriteRenderer.sortingOrder;

        cloneSpriteRenderer.material = originalSpriteRenderer.material;
    }

}
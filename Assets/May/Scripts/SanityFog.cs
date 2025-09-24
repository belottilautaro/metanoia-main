using UnityEngine;
using UnityEngine.UI; 
public class SanityFog : MonoBehaviour
{
    [SerializeField] private RawImage fogImage;
    [SerializeField] private float speedX = 0.5f;
    [SerializeField] private float speedY = 0.2f;

    private Material fogMaterial;
    private Vector2 offset = Vector2.zero;

    private void Start()
    {
        
        fogMaterial = new Material(fogImage.material);
        fogImage.material = fogMaterial;
    }

    private void Update()
    {
        offset.x += speedX * Time.deltaTime;
        offset.y += speedY * Time.deltaTime;
        fogMaterial.mainTextureOffset = offset;
    }
}

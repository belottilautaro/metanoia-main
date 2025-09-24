using UnityEngine;

/// <summary>
/// Camera movement script for third person games.
/// Este script se coloca en un objeto vacío (pivot).
/// La cámara principal (MainCamera) debe ser hija de ese objeto.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Control de cámara")]
    [Tooltip("Mover la cámara solo al mantener el botón derecho. No funciona con joystick.")]
    public bool clickToMoveCamera = false;

    [Tooltip("Habilitar zoom con la rueda del mouse. No funciona con joystick.")]
    public bool canZoom = true;

    [Tooltip("Sensibilidad de la cámara (velocidad de rotación).")]
    public float sensitivity = 5f;

    [Tooltip("Límites de rotación vertical (X = mínimo, Y = máximo).")]
    public Vector2 cameraLimit = new Vector2(-45, 40);

    [Header("Colisiones de cámara")]
    [Tooltip("Distancia deseada de la cámara respecto al jugador.")]
    public float cameraDistance = 5f;

    [Tooltip("Radio para simular que la cámara es una esfera (mejor que un punto).")]
    public float cameraRadius = 0.3f;

    [Tooltip("Capas con las que la cámara puede chocar.")]
    public LayerMask collisionMask;

    [Header("Suavizado")]
    [Tooltip("Qué tan rápido se ajusta la cámara a su nueva posición.")]
    public float smoothSpeed = 10f;

    // --- variables internas ---
    private Transform player;        // referencia al jugador
    private Transform cam;           // referencia a la cámara
    private float mouseX;            // rotación acumulada en X
    private float mouseY;            // rotación acumulada en Y
    private float offsetDistanceY;   // altura fija respecto al jugador
    private Vector3 currentVelocity; // para SmoothDamp

    void Start()
    {
        // Buscar jugador por tag
        player = GameObject.FindWithTag("Player").transform;
        offsetDistanceY = transform.position.y;

        // Guardar referencia a la cámara principal
        cam = Camera.main.transform;

        // Bloquear y ocultar el cursor si NO se usa clickToMoveCamera
        if (!clickToMoveCamera)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        // --- seguir al jugador ---
        transform.position = player.position + new Vector3(0, offsetDistanceY, 0);

        // --- rotación con mouse ---
        if (!clickToMoveCamera || Input.GetAxisRaw("Fire2") != 0)
        {
            mouseX += Input.GetAxis("Mouse X") * sensitivity;
            mouseY += Input.GetAxis("Mouse Y") * sensitivity;
            mouseY = Mathf.Clamp(mouseY, cameraLimit.x, cameraLimit.y);

            transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
        }

        // --- zoom ---
        if (canZoom && Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * sensitivity * 2;
            // opcional: Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 40f, 80f);
        }

        // --- colisiones de cámara ---
        Vector3 desiredCameraPos = transform.position - transform.forward * cameraDistance;

        if (Physics.SphereCast(transform.position, cameraRadius, -transform.forward, out RaycastHit hit, cameraDistance, collisionMask))
        {
            // colocar la cámara justo antes de la pared
            cam.position = hit.point + transform.forward * cameraRadius;
        }
        else
        {
            // suavizado hacia la posición ideal
            cam.position = Vector3.SmoothDamp(cam.position, desiredCameraPos, ref currentVelocity, 1f / smoothSpeed);
        }
    }
}


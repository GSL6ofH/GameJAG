using UnityEngine;

public class SimplePlayer : MonoBehaviour
{
    [Header("Instellingen")]
    public float loopSnelheid = 7f;
    public float muisGevoeligheid = 200f;
    public float sprongKracht = 5f;
    public float zwaartekracht = -15f;

    [Header("Referenties")]
    public Camera spelerCamera;

    private CharacterController controller;
    private float xRotatie = 0f;
    private Vector3 snelheid;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- 1. RONDKIJKEN (Muis) ---
        float muisX = Input.GetAxis("Mouse X") * muisGevoeligheid * Time.deltaTime;
        float muisY = Input.GetAxis("Mouse Y") * muisGevoeligheid * Time.deltaTime;

        xRotatie -= muisY;
        xRotatie = Mathf.Clamp(xRotatie, -90f, 90f); // Niet over de kop kijken

        spelerCamera.transform.localRotation = Quaternion.Euler(xRotatie, 0f, 0f);
        transform.Rotate(Vector3.up * muisX);


 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 beweging = transform.right * x + transform.forward * z;
        controller.Move(beweging * loopSnelheid * Time.deltaTime);

        if (controller.isGrounded && snelheid.y < 0)
        {
            snelheid.y = -2f; 
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            snelheid.y = Mathf.Sqrt(sprongKracht * -2f * zwaartekracht);
        }

        snelheid.y += zwaartekracht * Time.deltaTime;
        controller.Move(snelheid * Time.deltaTime);
    }
}
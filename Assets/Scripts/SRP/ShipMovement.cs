using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 25f;

    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        transform.position += transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
    }

    private void Rotate()
    {
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
    }

    public void IncreaseMoveSpeed(float amount) => moveSpeed += amount;
    public void IncreaseRotationSpeed(float amount) => rotationSpeed += amount;
}

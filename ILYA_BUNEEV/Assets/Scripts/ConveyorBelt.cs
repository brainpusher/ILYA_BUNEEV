using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float beltSpeed = 0.2f;
    [SerializeField] private Rigidbody beltRigidbody;

    private void FixedUpdate()
    {
        Vector3 position = beltRigidbody.position;
        beltRigidbody.position += Vector3.left * beltSpeed * Time.fixedDeltaTime;
        beltRigidbody.MovePosition(position);
    }
}

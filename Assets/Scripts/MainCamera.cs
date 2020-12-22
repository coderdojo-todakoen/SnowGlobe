using UnityEngine;

public class MainCamera : MonoBehaviour
{
    void FixedUpdate()
    {
        // キー入力でカメラを回転します
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.RotateAround(Vector3.zero, Vector3.up, 0.3f * horizontalInput);
    }
}

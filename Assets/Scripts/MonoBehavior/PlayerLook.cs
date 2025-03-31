using UnityEngine;

public class PlayerLook : MonoBehaviour {

   [SerializeField] private Transform playerTransform;

   private void Awake() {
      Cursor.lockState = CursorLockMode.Locked;
   }

   private void Update() {
      Vector2 look = GameInput.Instance.GetLook();
      playerTransform.Rotate(Vector3.up, look.x * Time.deltaTime);
      transform.Rotate(Vector3.right, look.y * Time.deltaTime);
   }
}

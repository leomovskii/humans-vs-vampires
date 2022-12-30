using UnityEngine;

public class BedOwning : MonoBehaviour {

	[SerializeField] private Room m_Room;

	private void OnCollisionEnter2D(Collision2D c) {
		if (c.gameObject.TryGetComponent(out PlayerMovement player)) {
			player.SetSleepIn(transform);
			CameraController.SetFree();
			m_Room.SetOwner();
			this.enabled = false;
		}
	}
}
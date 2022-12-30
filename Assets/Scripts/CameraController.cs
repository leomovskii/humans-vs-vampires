using UnityEngine;

public class CameraController : MonoBehaviour {

	enum CameraFollowMode {
		Locked, Following, Free
	}

	enum CameraUpdateMode {
		FixedUpdate, LateUpdate
	}

	[System.Serializable]
	struct CameraBounds {
		public float xMin;
		public float xMax;
		public float yMin;
		public float yMax;
	}

	static CameraController instance;

	private Vector3 _NextPosition;

	[SerializeField] private Camera m_Camera;
	[Space(10)]
	[SerializeField] private Transform m_FollowingTarget;
	[SerializeField] private float m_SpeedInFreeMode = 1f;
	[SerializeField] private CameraFollowMode m_FollowMode = CameraFollowMode.Free;
	[SerializeField] private CameraUpdateMode m_UpdateMode = CameraUpdateMode.LateUpdate;
	[Space(10)]
	[SerializeField] private CameraBounds m_Bounds;

	private void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(this);
	}

	private void Start() {
		_NextPosition = transform.position;
	}

	private void LateUpdate() {
		if (m_FollowMode == CameraFollowMode.Following && m_UpdateMode == CameraUpdateMode.LateUpdate)
			FollowTarget();

		else if (m_FollowMode == CameraFollowMode.Free)
			MoveCamera();
	}

	private void FixedUpdate() {
		if (m_FollowMode == CameraFollowMode.Following && m_UpdateMode == CameraUpdateMode.FixedUpdate)
			FollowTarget();
	}

	private void FollowTarget() {
		if (m_FollowingTarget != null)
			UpdatePosition(m_FollowingTarget.position.x, m_FollowingTarget.position.y);
		else
			SetFree();
	}

	private void MoveCamera() {
		var moveDelta = InputHandler.input * m_SpeedInFreeMode * Time.deltaTime;
		UpdatePosition(transform.position.x + moveDelta.x, transform.position.y + moveDelta.y);
	}

	public static void SetLocked() {
		instance.m_FollowMode = CameraFollowMode.Locked;
	}

	public static void SetFree() {
		instance.m_FollowMode = CameraFollowMode.Free;
	}

	public static void SetFollowing(Transform target) {
		instance.m_FollowMode = CameraFollowMode.Following;
		instance.m_FollowingTarget = target;
	}

	public static void JumpTo(Transform target) {
		instance.m_FollowMode = CameraFollowMode.Free;
		instance.UpdatePosition(target.position.x, target.position.y);
	}

	private void UpdatePosition(float x, float y) {
		_NextPosition.x = Mathf.Clamp(x, m_Bounds.xMin, m_Bounds.xMax);
		_NextPosition.y = Mathf.Clamp(y, m_Bounds.yMin, m_Bounds.yMax);
		transform.position = _NextPosition;
	}
}
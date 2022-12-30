using UnityEngine;

public class InputHandler : MonoBehaviour {

	enum InputType {
		Keyboard, Joystick
	}

	public static InputHandler instance;

	private Vector2 _Input;
	public static Vector2 input => instance._Input.normalized;

	[SerializeField] private InputType m_InputType;
	[SerializeField] private Joystick m_Joystick;

	private void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(this);
	}

	private void Update() {
		if (m_InputType == InputType.Keyboard) {
			_Input.x = Input.GetAxisRaw("Horizontal");
			_Input.y = Input.GetAxisRaw("Vertical");
		} else {
			_Input.x = m_Joystick.Horizontal;
			_Input.y = m_Joystick.Vertical;
		}
	}
}
using UnityEngine;

public class Door : MonoBehaviour {

	public Animator anim;

	public void Close() {
		anim.SetTrigger("close");
	}
}
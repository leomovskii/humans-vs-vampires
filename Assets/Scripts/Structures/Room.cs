using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	[SerializeField] private Door m_Door;
	[SerializeField] private GameObject[] m_BuildPlaces;

	public void SetOwner() {
		m_Door.Close();
		foreach (var place in m_BuildPlaces)
			place.SetActive(true);
	}
}
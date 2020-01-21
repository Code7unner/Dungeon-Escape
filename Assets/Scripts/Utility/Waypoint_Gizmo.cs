using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Waypoint_Gizmo : MonoBehaviour {

	public float size = 0.1f;
	public Color color = Color.white;

	void OnDrawGizmos(){

		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, size );
	}
}

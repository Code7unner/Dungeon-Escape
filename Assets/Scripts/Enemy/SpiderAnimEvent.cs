using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimEvent : MonoBehaviour {
	
	// Handle to Spider Script
	private Spider m_spider;

	void Start(){
		// Handle assignment to Spider Script
		m_spider = GetComponentInParent < Spider>();
	}

	public void fire(){

		//Tell Spider to fire
		// use handle to call Attack on spider
		m_spider.Attack();
	}
}

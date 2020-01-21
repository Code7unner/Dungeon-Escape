using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

	int Health { get; set;}
	//TODO:
	// Add int variabe to provide variable damage for each weapon 
	// or players abilities
	void Damage();


}

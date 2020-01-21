using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable {

	int Diamonds { get; set;}

	void AddDiamonds(int value);
}

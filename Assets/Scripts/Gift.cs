using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour {

	public void Pop()
	{
		Debug.Log("Gift pop!");
		Destroy(this.gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWall : MonoBehaviour {
	public bool isBackwallGotSphere=false;

	private void OnTriggerStay(Collider other)
	{
		if (!isBackwallGotSphere) {
			if (other.tag == "Sphere") {
				isBackwallGotSphere = true;
			}

		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (isBackwallGotSphere) {
			if (other.tag == "Sphere") {
				isBackwallGotSphere = false;
			}

		}

	}
}

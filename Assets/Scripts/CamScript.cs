using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target;

    void LateUpdate(){

        Vector3 focusPoint = target.transform.position;
		Vector3 lookDirection = transform.forward;
        Vector3 upOffest = new Vector3(0,0.5f,0);

		transform.localPosition = (focusPoint+upOffest) - lookDirection * 6;
    }

}

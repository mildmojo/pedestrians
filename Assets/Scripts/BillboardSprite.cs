using UnityEngine;
using System.Collections;

public class BillboardSprite : MonoBehaviour {

  void Start () {

  }

  void LateUpdate () {
    transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back,
      transform.up);
  }
}

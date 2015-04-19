using UnityEngine;
using System.Collections;

public class FeetDown : MonoBehaviour {
  public GameObject target;

  private Quaternion _facing;
  private Rigidbody _body;
  private bool _isActive;

  void Start() {
    _facing = transform.rotation;
    _body = GetComponent<Rigidbody>();
    _isActive = true;
  }

  void Update() {
    if (target && _isActive) {
      // Debug.DrawRay(transform.position, transform.forward * 20f, Color.red);
      // Debug.DrawRay(transform.position, transform.up * 20f, Color.blue);

      var feetAxis = transform.position - target.transform.position;
      transform.up = Vector3.Lerp(transform.up, feetAxis, 0.5f);
    }
  }

  void OnLanded() {
    _isActive = false;
    gameObject.layer = LayerMask.NameToLayer("InactiveCharacter");
  }

}
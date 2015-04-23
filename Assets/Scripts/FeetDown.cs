using UnityEngine;
using System.Collections;

public class FeetDown : MonoBehaviour {
  public GameObject target;
  public bool useChild;

  private bool _isActive;

  void Start() {
    _isActive = true;
  }

  void Update() {
    if (target && _isActive) {
      // Debug.DrawRay(transform.position, transform.forward * 20f, Color.red);
      // Debug.DrawRay(transform.position, transform.up * 20f, Color.blue);

      var feetAxis = gameObject.transform.position - target.transform.position;
      transform.up = Vector3.Lerp(gameObject.transform.up, feetAxis, 0.5f);
    }
  }

  void OnTakeoff() {
    _isActive = false;
    gameObject.layer = LayerMask.NameToLayer("Default");
  }

  void OnLanded() {
    _isActive = false;
    gameObject.layer = LayerMask.NameToLayer("InactiveCharacter");
  }

}
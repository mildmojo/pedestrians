using UnityEngine;
using System.Collections;

public class FeetDown : MonoBehaviour {
  public GameObject target;
  public bool useChild;

  private GameObject _self;
  private bool _isActive;

  void Start() {
    _isActive = true;
    _self = useChild ? transform.GetChild(0).gameObject : gameObject;
  }

  void Update() {
    if (target && _isActive) {
      // Debug.DrawRay(transform.position, transform.forward * 20f, Color.red);
      // Debug.DrawRay(transform.position, transform.up * 20f, Color.blue);

      var feetAxis = _self.transform.position - target.transform.position;
      transform.up = Vector3.Lerp(_self.transform.up, feetAxis, 0.5f);
    }
  }

  void OnTakeoff() {
    _isActive = false;
    _self.layer = LayerMask.NameToLayer("Default");
  }

  void OnLanded() {
    _isActive = false;
    _self.layer = LayerMask.NameToLayer("InactiveCharacter");
  }

}
using UnityEngine;

public class FlyLikeSuperhero : MonoBehaviour {
  private Rigidbody _body;

  void Awake() {
    _body = GetComponent<Rigidbody>();
  }

  void Update() {
    if (_body.velocity.sqrMagnitude > 0.01f) {
      transform.up = Vector3.MoveTowards(transform.up, _body.velocity.normalized, 0.1f);
    }
  }
}
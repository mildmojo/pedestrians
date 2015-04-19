using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InControl;

public class ButtonLauncher : MonoBehaviour {
  public float launchForce = 200f;

  private Transform _childTransform;
  private Orbiter _orbiter;
  private PedestrianManager _pedManager;

  // Use this for initialization
  void Start () {
    _orbiter = GetComponent<Orbiter>();
    _pedManager = PedestrianManager.Instance;
    LoadAmmo();
  }

  // Update is called once per frame
  void Update () {
    var device = InputManager.ActiveDevice;
    if (device.AnyButton.WasPressed && transform.childCount > 0) {
      Launch();
    }

    if (Input.GetKeyDown(KeyCode.Escape)) {
      // FadeToBlack.Instance.FadeOut(() => {
        Application.LoadLevel("menu");
      //});
    }
  }

  void Launch() {
    _childTransform.parent = null;
    var orbiter = GetComponent<Orbiter>();
    var body = _childTransform.gameObject.GetComponent<Rigidbody>();
    var orbitArm = _childTransform.position - orbiter.target.transform.position;
    var tangent = Vector3.Cross(orbitArm, Vector3.down);
    body.isKinematic = false;
    gameObject.layer = LayerMask.NameToLayer("Default");
    body.AddForce(tangent.normalized * launchForce);
    Invoke("LoadAmmo", 1f);
  }

  void LoadAmmo() {
    _childTransform = PedestrianManager.Instance.CreatePedestrian(gameObject,
      transform.position, _orbiter.target).transform;
  }

}

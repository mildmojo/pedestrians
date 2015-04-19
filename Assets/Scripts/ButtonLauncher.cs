using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InControl;

public class ButtonLauncher : MonoBehaviour {
  public float launchForce = 200f;
  public List<GameObject> pedestrians;

  private ShuffleDeck _ammoDeck;
  private Orbiter _orbiter;

  // Use this for initialization
  void Start () {
    _orbiter = GetComponent<Orbiter>();
    _ammoDeck = new ShuffleDeck(pedestrians);
    LoadAmmo();
  }

  // Update is called once per frame
  void Update () {
    var device = InputManager.ActiveDevice;
    if (device.AnyButton.WasPressed && transform.childCount > 0) {
      var child = transform.GetChild(0);
      child.parent = null;
      var orbiter = GetComponent<Orbiter>();
      var body = child.gameObject.GetComponent<Rigidbody>();
      var orbitArm = child.position - orbiter.target.transform.position;
      var tangent = Vector3.Cross(orbitArm, Vector3.down);
      body.isKinematic = false;
      body.AddForce(tangent.normalized * launchForce);
      Invoke("LoadAmmo", 1f);
    }
  }

  void LoadAmmo() {
    var ammo = Instantiate(_ammoDeck.Draw() as GameObject) as GameObject;
    ammo.transform.position = transform.position;
    ammo.transform.parent = transform;
    var feetDown = ammo.AddComponent<FeetDown>();
    feetDown.target = _orbiter.target;
    ammo.GetComponent<Rigidbody>().isKinematic = true;
  }

}

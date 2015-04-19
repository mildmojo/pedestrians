using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

  void OnStart() {
    Application.LoadLevel("game");
  }

  void OnCredits() {
    Application.LoadLevel("credits");
  }

  void OnExit() {
    Application.Quit();
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      OnExit();
    }
  }
}

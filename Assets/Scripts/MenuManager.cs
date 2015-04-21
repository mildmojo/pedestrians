using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

  public void Awake() {
    // Hide quit button on the web because it's nonsense.
    #if UNITY_WEBPLAYER || UNITY_WEBGJ
      var quitButton = GameObject.Find("QuitButton");
      quitButton.SetActive(false);
    #endif
  }

  public void Start() {
    ScreenFader.Instance.FadeOut(0f);
    ScreenFader.Instance.FadeIn(1f);
  }

  public void OnStartClick() {
    Load("game");
  }

  public void OnCreditsClick() {
    Load("credits");
  }

  public void OnQuitClick() {
    #if UNITY_WEBPLAYER
      return;
    #endif
    #if UNITY_WEBGL
      return;
    #endif

    ScreenFader.Instance.FadeOut(0.5f, () => {
      Application.Quit();
    });
  }

  void Load(string levelName) {
    ScreenFader.Instance.FadeOut(0.5f, () => {
      Application.LoadLevel(levelName);
    });
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      OnQuitClick();
    }
  }
}

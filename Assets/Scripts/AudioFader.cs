using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioFader : MonoBehaviour {
  public static AudioFader Instance;

  public AudioMixer mixer;

  private CanvasGroup _cg;

  void Awake() {
    Instance = this;
  }



  public void FadeIn(float time = 0.5f, System.Action onComplete = null) {
    Fade("Normal", time, onComplete);
  }

  public void FadeOut(float time = 0.5f, System.Action onComplete = null) {
    Fade("Mute", time, onComplete);
  }

  public void Muffle(float time = 0.5f, System.Action onComplete = null) {
    Fade("Muffled Music", time, onComplete);
  }

  private void Fade(string snapshotName, float time, System.Action onComplete) {
    LeanTween.cancel(gameObject);
    mixer.FindSnapshot(snapshotName).TransitionTo(time);
    if (onComplete != null) {
      LeanTween.delayedCall(gameObject, time, onComplete);
    }
  }
}

using UnityEngine;

public class Yeller : MonoBehaviour {
  public PedestrianManager.NameClips nameClips;

  private AudioSource _audio;

  void Awake() {
    _audio = GetComponent<AudioSource>();
  }

  void OnTakeoff() {
    var delay = 0f;
    foreach(var clip in nameClips.clips) {
      if (delay < 0.01f) {
        _audio.PlayOneShot(clip);
      } else {
        var delayedClip = clip;
        LeanTween.delayedCall(gameObject, delay, () => _audio.PlayOneShot(delayedClip));
      }
      delay += clip.length;
    }
  }
}
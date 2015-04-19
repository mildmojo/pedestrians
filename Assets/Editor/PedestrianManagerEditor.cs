using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(PedestrianManager))]
public class PedestrianManagerEditor : Editor {

  public override void OnInspectorGUI() {
    PedestrianManager pedManager = (PedestrianManager) target;

    // Rescan button, load audio assets into AudioManager automatically.
    if(GUILayout.Button("Rescan")) {
      var audioAssets = pedManager.nameAudioAssets;
      audioAssets.Clear();

      var speakerDirs = Directory.GetDirectories("Assets/Audio/names");
      foreach (var speakerDir in speakerDirs) {
Debug.Log("speakerDir: " + speakerDir);
        var speakerName = Path.GetFileName(speakerDir);

        var nameTypeDirs = Directory.GetDirectories(speakerDir);
        foreach (var nameTypeDir in nameTypeDirs) {
Debug.Log("nameTypeDir: " + nameTypeDir);
          var nameType = Path.GetFileName(nameTypeDir);
          var key = speakerName + "." + nameType;
          var clipList = new List<AudioClip>();

          var clipFiles = Directory.GetFiles(nameTypeDir, "*.mp3");
          foreach (var clipFile in clipFiles) {
Debug.Log("clipFile: " + clipFile);
            var newClip = Resources.LoadAssetAtPath(clipFile, typeof(AudioClip)) as AudioClip;
            clipList.Add(newClip);
          }

          audioAssets.Add(new PedestrianManager.SpeakerToClips(key, clipList));
        }
      }
    }

    DrawDefaultInspector();
  }
}
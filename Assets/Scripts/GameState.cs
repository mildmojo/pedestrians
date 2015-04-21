public static class GameState {
  public enum States {
    MENU = 0,
    STOPPED = 1,
    PLAYING = 2,
    PAUSED = 3
  }

  public static bool IsMenu { get { return _state == States.MENU; } }
  public static bool IsStopped { get { return _state == States.STOPPED; } }
  public static bool IsPlaying { get { return _state == States.PLAYING; } }
  public static bool IsPaused { get { return _state == States.PAUSED; } }
  public static bool IsMuted { get { return _isMuted; } }

  private static States _state = States.STOPPED;
  private static bool _isMuted;

  public static void Menu() { _state = States.MENU; }
  public static void Stop() { _state = States.STOPPED; }
  public static void Play() { _state = States.PLAYING; }
  public static void Pause() { _state = States.PAUSED; }

  public static void Mute() { _isMuted = true; }
  public static void Unmute() { _isMuted = false; }

}
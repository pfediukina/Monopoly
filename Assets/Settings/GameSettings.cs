using UnityEngine;

[CreateAssetMenu(fileName ="GameSettings", menuName = "Game Settings/Settings", order = 1)]
public class GameSettings : ScriptableObject
{
    private static GameSettings _instance;

    [Header("Test")]
    [SerializeField] private bool _testMode;
    [SerializeField] private int _playersCount;


    public static bool testMode => GetInstance()._testMode; 
    public static int playersCount => GetInstance()._playersCount; 

    //[RuntimeInitializeOnLoadMethod]
    public static GameSettings GetInstance()
    {
        if(_instance == null)
            _instance = Resources.Load<GameSettings>("GameSettings");
        return _instance;
    }
}

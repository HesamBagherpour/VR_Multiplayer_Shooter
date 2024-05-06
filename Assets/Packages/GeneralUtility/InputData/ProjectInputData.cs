using System.Linq;
using System.Runtime.InteropServices;
using Models;
using UnityEngine;

/// <summary>
/// A singleton MonoBehavior.
/// </summary>
public class ProjectInputData: MonoBehaviour
{
    /// <summary>
    /// It's the only instance of the class.
    /// </summary>
    public static ProjectInputData Instance;

    /// <summary>
    /// Sets the instance of the class.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// A array of mock query params. It's just usable in Unity Editor. 
    /// </summary>
    [SerializeField] private QueryParam[] _mockPairs;

    [DllImport("__Internal")]
    private static extern string GetQueryParam(string q);
        
    /// <summary>
    /// Returns value of intended query param. 
    /// </summary>
    /// <param name="key">The key of intended query param.</param>
    /// <returns>The value of intended query param.</returns>
    public static string Get(string key)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
                return GetQueryParam(key);
#else
        if (Instance._mockPairs.All(p => p.Key != key)) return null;
        var p = Instance._mockPairs.First(p => p.Key == key);
        return p.Value;
#endif
    }
}
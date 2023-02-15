using UnityEngine;


/// <summary>
/// Scripts that shall have just one instance should inherit from this class
/// </summary>
/// <typeparam name="T">Insert your script here</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

    /// <summary>
    /// returns instance of object
    /// </summary>
    public static T Instance => instance;

    static T instance;

    /// <summary>
    /// Checks wether instance exists
    /// </summary>
    public static bool InstanceExist => Instance != null;


    protected void Awake()
    {
        if (InstanceExist)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}

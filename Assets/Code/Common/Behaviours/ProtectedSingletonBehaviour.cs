using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bestagon.Behaviours
{
    /// <summary>
    /// A behaviour enforces the singleton pattern but does not expose the instance.
    /// An instance is spawned if one is not found during access
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ProtectedSingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// Internal reference to the type
        /// </summary>
        private static T instance;

        /// <summary>
        /// Access the scene instance of a singleton Behaviour
        /// </summary>
        protected static T Instance
        {
            get
            {
                if (Application.isPlaying == false)
                {
                    Debug.LogError(typeof(T).Name + " accessed outside of play time.");
                    return null;
                }

                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        //Debug.Log("StaticBehaviour object created: " + typeof(T).Name);
                        //Make one
                        instance = (new GameObject(typeof(T).Name + " (StaticBehaviour)")).AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        private void OnDestroy()
        {
            //Destroy first to avoid having self-references spawn a new instance
            Destroy();
            //Clear the internal instance
            instance = null;
        }

        public static bool Exists()
        {
            return instance != null;
        }

        /// <summary>
        /// Called when the StaticBehaviour is destroyed.
        /// Deletes/Cleans static state
        /// </summary>
        protected abstract void Destroy();
    }
}
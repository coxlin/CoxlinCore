using UnityEditor;
using UnityEngine;

namespace CoxlinCore
{
    public static class DeleteAllMissingScripts
    {
        [MenuItem("Tools/Find missing scripts/DeleteAll")]
        static void FindAndDeleteMissingScripts()
        {
            foreach (GameObject gameObject in Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include,
                         FindObjectsSortMode.None))
            {
                foreach (Component component in gameObject.GetComponentsInChildren<Component>())
                {
                    if (component == null)
                    {
                        GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
                        break;
                    }
                }
            }
        }
    }
}

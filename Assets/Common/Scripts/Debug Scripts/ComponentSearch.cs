using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System.Reflection;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Common
{
    public class ComponentSearch : MonoBehaviour
    {
        [Header("Enter called Method/Event")]
        public UnityEvent searchTarget;

        [Header("Objects with called Method/Event")]
        [ReadOnly] public List<Component> results;

        [Header("Objects with link to target component/gameObject")]
        [ReadOnly] public List<Component> linksToTarget;

        [Header("Objects with link to components of target gameObject")]
        [ReadOnly] public List<Component> linksToComponentOfTarget;

        [Space]

        [Header("Debug if result incorrect")]

        [ReadOnly] public Object target;
        [ReadOnly] public string targetMethodName;

        public bool debug = false;
        public MonoBehaviour answerMonobehaviour;
        public string answerFieldName;

        //public 

        public static ComponentSearch Instance
        {
            get
            {
                var instance = FindObjectOfType<ComponentSearch>();
                if (instance == null)
                {
                    var go = new GameObject("SEARCH RESULTS");
                    instance = go.AddComponent<ComponentSearch>();
                }
                return instance;
            }
        }

#if UNITY_EDITOR

        private bool CallsTargetMethod(FieldInfo field, MonoBehaviour mb)
        {
            if (mb == answerMonobehaviour)
            {
                Debug.LogFormat($"CallsTargetMethod({field}, {mb.ExtToString()})");
            }
            if (!field.FieldType.IsSubclassOf(typeof(UnityEventBase)))
            {
                return false;
            }
            var fieldValue = field.GetValue(mb);

            UnityEventBase e = fieldValue as UnityEventBase;
            if (e.Listeners().Any(l => l.target == target && l.methodName == targetMethodName))
            {
                Debug.LogFormat($"call: mb = `{mb.ExtToString()}`, field = `{field.Name}`");
                if (debug) Debug.LogFormat($"e.Listeners = {e.Listeners().ExtToString()}");
                return true;
            }

            return false;
        }

        private bool CallsTargetMethod(MonoBehaviour mb)
        {
            if (mb == this)
            {
                return false;
            }
            var fields = mb.GetType().GetFieldsRecursive(
                         BindingFlags.NonPublic |
                         BindingFlags.Public |
                         BindingFlags.Instance);
            return fields.Any(f => CallsTargetMethod(f, mb));
        }

        private bool ReferenceTarget(FieldInfo field, MonoBehaviour mb)
        {
            if (mb == answerMonobehaviour)
            {
                Debug.LogFormat($"ReferenceTarget({field}, {mb.ExtToString()})");
            }

            var fieldValue = field.GetValue(mb);
            if (mb == answerMonobehaviour && (answerFieldName == "" || field.Name == answerFieldName))
            {
                Debug.LogFormat($"field == {fieldValue}");
            }

            var unityObject = fieldValue as Object;
            if (unityObject == target)
            {
                Debug.LogFormat($"reference: mb = `{mb.ExtToString()}`, field = `{field.Name}`");
                if (debug) Debug.LogFormat($"uo = {unityObject.ExtToString()}");
                return true;
            }

            var ienumerable = fieldValue as IEnumerable;
            if (ienumerable != null && !(ienumerable is Transform))
            {
                if (mb == answerMonobehaviour && (answerFieldName == "" || field.Name == answerFieldName))
                {
                    Debug.LogFormat($"ienumerable //ReferenceTarget({field}, {mb.ExtToString()})");
                }
                if (ienumerable.Any(o => o == (object)target))
                {
                    Debug.LogFormat($"reference: mb = `{mb.ExtToString()}`, field = `{field.Name}`");
                    return true;
                }
            }

            UnityEventBase e = fieldValue as UnityEventBase;
            if (e != null && e.Listeners().Any(l => l.target == target))
            {
                Debug.LogFormat($"reference: mb = `{mb.ExtToString()}`, field = `{field.Name}`");
                if (debug) Debug.LogFormat($"e.Listeners = {e.Listeners().ExtToString()}");
                return true;
            }

            return false;
        }

        private bool ReferenceTarget(MonoBehaviour mb)
        {
            if (mb == this)
            {
                return false;
            }
            var fields = mb.GetType().GetFieldsRecursive(
                         BindingFlags.NonPublic |
                         BindingFlags.Public |
                         BindingFlags.Instance);
            return fields.Any(f => ReferenceTarget(f, mb));
        }

        private bool ReferenceComponentOfTarget(FieldInfo field, MonoBehaviour mb)
        {
            if (mb == answerMonobehaviour)
            {
                Debug.LogFormat($"ReferenceComponentOfTarget({field}, {mb.ExtToString()})");
            }

            var fieldValue = field.GetValue(mb);
            if (mb == answerMonobehaviour && (answerFieldName == "" || field.Name == answerFieldName))
            {
                Debug.LogFormat($"field == {fieldValue}");
            }

            var component = fieldValue as Component;
            if (component != null && component.gameObject == target)
            {
                Debug.LogFormat($"reference to component: mb = `{mb.ExtToString()}`, field = `{field.Name}`");
                if (debug) Debug.LogFormat($"component = {component.ExtToString()}");
                return true;
            }
            
            var ienumerable = fieldValue as IEnumerable;
            if (ienumerable != null && !(ienumerable is Transform))
            {
                if (mb == answerMonobehaviour && (answerFieldName == "" || field.Name == answerFieldName))
                {
                    Debug.LogFormat($"ienumerable //ReferenceTarget({field}, {mb.ExtToString()})");
                }
                if (ienumerable.Any(o => {
                    var o5 = o;
                    Component c5 = null;
                    try
                    {
                        c5 = o5 as Component;
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogFormat($"o: {o}");
                    }
                    var c = c5;
                    return c != null && c.gameObject == target;
                }))
                {
                    Debug.LogFormat($"reference to component: mb = `{mb.ExtToString()}`, field = `{field.Name}`");
                    return true;
                }
            }

            UnityEventBase e = fieldValue as UnityEventBase;
            if (e != null && e.Listeners().Any(l =>
            {
                var c = l.target as Component;
                return c != null && c.gameObject == target;
            }))
            {
                Debug.LogFormat($"reference to component: mb = `{mb.ExtToString()}`, field = `{field.Name}`");
                if (debug) Debug.LogFormat($"e.Listeners = {e.Listeners().ExtToString()}");
                return true;
            }

            return false;
        }

        private bool ReferenceComponentOfTarget(MonoBehaviour mb)
        {
            if (mb == this)
            {
                return false;
            }
            var fields = mb.GetType().GetFieldsRecursive(
                         BindingFlags.NonPublic |
                         BindingFlags.Public |
                         BindingFlags.Instance);
            return fields.Any(f => ReferenceComponentOfTarget(f, mb));
        }

        private void Search()
        {
            Debug.Log("Search started");

            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

            var allMonobehaviours = SceneManager.GetActiveScene()
                .GetComponents<MonoBehaviour>(includeInactive: true);

            var scriptsWhoDoesIt = allMonobehaviours.Where(CallsTargetMethod).ToList();
            results = scriptsWhoDoesIt.Cast<Component>().ToList();

            var scriptsWhoReferenceIt = allMonobehaviours.Where(ReferenceTarget).ToList();
            linksToTarget = scriptsWhoReferenceIt.Cast<Component>().ToList();

            var scriptsWhoReferenceItsComponent = allMonobehaviours.Where(ReferenceComponentOfTarget).ToList();
            linksToComponentOfTarget = scriptsWhoReferenceItsComponent.Cast<Component>().ToList();

            Debug.Log("Search finished");
        }

        [ContextMenu("Search method invocations")]
        public void SearchMethodInvocations()
        {
            if (searchTarget.GetPersistentEventCount() < 1)
            {
                Debug.LogError("No search target selected!");
                return;
            }

            target = searchTarget.GetPersistentTarget(0);
            targetMethodName = searchTarget.GetPersistentMethodName(0);

            Search();
        }

        [ContextMenu("Count gameobjects")]
        public void CountGameobjects()
        {
            Debug.LogFormat($"gameobjects: {SceneManager.GetActiveScene().GetComponents<Transform>(includeInactive: true).Count()}");
            Debug.LogFormat($"active gameobjects: {SceneManager.GetActiveScene().GetComponents<Transform>().Count()}");
        }

        [MenuItem("Utilities/Find references %e")]
        public static void FindReferences()
        {
            var search = FindObjectOfType<ComponentSearch>();
            search.target = Selection.objects[0];

            Debug.LogFormat($"Find references to {search.target.ExtToString()}");

            search.Search();

            Selection.objects = search.gameObject.Single().ToArray();
            EditorGUIUtility.PingObject(search.gameObject);
        }

#endif
    }
}
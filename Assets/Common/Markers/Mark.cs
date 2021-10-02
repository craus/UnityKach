using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Common
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Common/Mark")]
    public class Mark : ScriptableObject
    {
        public IEnumerable<Component> objects => FindObjectsOfType<Marker>().Where(m => m.mark == this);

        private Marker target;

        public Marker Target {
            get {
                if (target == null) {
                    target = FindObjectsOfType<Marker>().FirstOrDefault(m => m.mark == this);
                }
                return target;
            }
            set {
                target = value;
            }
        }
    }
}
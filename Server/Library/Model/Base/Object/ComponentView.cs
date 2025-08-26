using UnityEngine;

namespace ETModel
{
    public class ComponentView: MonoBehaviour
    {
#if UNITY_EDITOR
        public string ComponentType = string.Empty;

        private object _component = null;
        public object Component
        {
            get
            {
                return _component;
            }
            set
            {
                if (value != null)
                    ComponentType = value.GetType().ToString();
                _component = value;
            }
        }
#else
        public object Component { get; set; }
#endif

    }
}
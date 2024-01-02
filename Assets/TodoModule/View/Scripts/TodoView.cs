using UnityEngine;
using UPatterns;

namespace TodoModuleSystem.View
{
    public class TodoView : MonoBehaviour
    {
        [field: SerializeField] public Pool<TodoItem> ItemPool { private set; get; }
    }
}
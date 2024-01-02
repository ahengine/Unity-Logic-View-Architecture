using UnityEngine;
using UScreens;

namespace TodoModuleSystem.View
{
    public class TodoViewState : UScreenGeneric<TodoViewState, TodoView>
    {
        public override void InitializeState() {}
        public override void InitializeView() {}

        public void Set(Todo[] items) {

            View.ItemPool.DeactiveAllInstance();

            for (int i = 0; i < items.Length; i++)
                View.ItemPool.GetActive.Set(items[i]);
        }
    }
}
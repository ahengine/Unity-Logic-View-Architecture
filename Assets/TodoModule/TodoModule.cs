using UnityEngine;
using UPatterns;
using UScreens;

namespace TodoModuleSystem.View
{
    public static class TodoModule
    {
        public static void ShowAll() =>
            TodoController.Instance.ShowAll(UScreenRepo.Get<TodoViewState>().Set);
        
        public static void Show(int id) =>
            TodoController.Instance.ShowById(id, UScreenRepo.Get<TodoViewState>().Set);
    }
}
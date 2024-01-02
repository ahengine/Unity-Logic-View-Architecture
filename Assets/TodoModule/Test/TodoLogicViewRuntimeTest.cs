using TodoModuleSystem;
using TodoModuleSystem.View;
using UnityEngine;
using UScreens;

public class TodoLogicViewRuntimeTest : MonoBehaviour
{
    [SerializeField] private int id;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            TodoModule.ShowAll();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            TodoModule.Show(id);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UScreens
{
    public abstract class UScreen : MonoBehaviour
    {
        public bool IsShowing { protected set; get; }
        public abstract void InitializeState();
        public abstract void InitializeView();
        public abstract void Show();
        public abstract void Hide();
    }
}

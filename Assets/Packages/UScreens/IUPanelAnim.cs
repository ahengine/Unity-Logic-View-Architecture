using System.Collections;
using UnityEngine;

namespace UScreens {

    /// <summary>
    /// Interface for handling Panel Animations.
    /// </summary>
    public interface IUPanelAnim 
    {
        /// <summary>
        /// Validates the system, given the <see cref="UPanel"/> component using it. This is Editor-Only.
        /// </summary>
        public void Validate(UPanel self);

        /// <summary>
        /// Initializes the system, given the <see cref="UPanel"/> component using it.
        /// </summary>
        public void Initialize(UPanel self);

        /// <summary>
        /// Plays the show animation.
        /// </summary>
        public void Show();

        /// <summary>
        /// Playes hide animation and returns the duration in seconds
        /// </summary>
        public float Hide();
    }

    /// <summary>
    /// Panel animation that uses <see cref="Animator"/> to handle the animations.
    /// </summary>
    public class AnimatorPanelAnim : IUPanelAnim 
    {
        Animator animator;

        public void Validate(UPanel self)
        {
            if (!self.TryGetComponent(out Animator _))
            {
                self.gameObject.AddComponent<Animator>();
#if UNITY_EDITOR
                // mark dirty so it saves the changes
                UnityEditor.EditorUtility.SetDirty(self);
#endif            
            }
        }
        public void Initialize(UPanel self) => animator = self.GetComponent<Animator>();
        public float Hide() 
        {
            animator.Play("Hide");
            // get duration of the animation
            var state = animator.GetCurrentAnimatorStateInfo(0);
            return state.length;
        }

        public void Show() => animator.Play("Show");
    }
}
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace ParallaxSplitView.Behaviors
{
    //Thanks R2D2RIGO for developing this!
    //https://github.com/r2d2rigo/WinCompositionTiltEffect/blob/master/WinCompositionTiltEffect/TiltBehavior.cs
    public class TiltBehavior : Behavior
    {
        /// <summary>
        /// Windows Composition Visual element.
        /// </summary>
        private Visual elementVisual;

        /// <summary>
        /// Handle to control the behavior is attached to.
        /// </summary>
        private UIElement uiElement;

        /// <summary>
        /// Called when the behavior is attached to a control.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            // Only allow being attached to UIElement subclasses, since we are going to need it for retrieving
            // the composition Visual.
            this.uiElement = this.AssociatedObject as UIElement;
            if (this.uiElement == null)
            {
                throw new InvalidOperationException("TiltBehavior can only be attached to types inheriting UIElement");
            }

            // Obtain a composition Visual.
            this.elementVisual = ElementCompositionPreview.GetElementVisual(this.uiElement);

            // Subscribe to all events related to pointer/mouse interaction.
            this.uiElement.PointerPressed += UiElement_PointerPressed;
            this.uiElement.PointerMoved += UiElement_PointerMoved;
            this.uiElement.PointerReleased += UiElement_PointerReleased;
            this.uiElement.PointerCanceled += UiElement_PointerCanceled;
            this.uiElement.PointerExited += UiElement_PointerExited;
        }

        /// <summary>
        /// Called when the behavior is detached.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            // Free composition Visual element, if existing, and resets tilt effect.
            if (this.elementVisual != null)
            {
                ResetTiltEffect();

                this.elementVisual.Dispose();
                this.elementVisual = null;
            }

            // Unsubscribe from pointer events.
            this.uiElement.PointerPressed -= UiElement_PointerPressed;
            this.uiElement.PointerMoved -= UiElement_PointerMoved;
            this.uiElement.PointerReleased -= UiElement_PointerReleased;
            this.uiElement.PointerCanceled -= UiElement_PointerCanceled;
            this.uiElement.PointerExited -= UiElement_PointerExited;

            this.uiElement = null;
        }

        /// <summary>
        /// The pointer has been pressed on top of the control, apply tilt effect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UiElement_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            ApplyTiltEffect(e);
        }

        /// <summary>
        /// The pointer has been moved on top of the control - check if it's pressed and act accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UiElement_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (e.Pointer.IsInContact)
            {
                ApplyTiltEffect(e);
            }
            else
            {
                ResetTiltEffect();
            }
        }

        /// <summary>
        /// The pointer has been released on top of the control, reset the tilt effect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UiElement_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            ResetTiltEffect();
        }

        /// <summary>
        /// The pointer contact has been canceled, reset the tilt effect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UiElement_PointerCanceled(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            ResetTiltEffect();
        }

        /// <summary>
        /// The pointer has exited the bounds of the control, reset the tilt effect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UiElement_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            ResetTiltEffect();
        }

        /// <summary>
        /// Applies tilt effect on the control.
        /// </summary>
        /// <param name="e"></param>
        private void ApplyTiltEffect(Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            // Set a center point for rotating the Visual.
            // The Z value adds a bit of depth movement.
            this.elementVisual.CenterPoint = new Vector3((float)(this.uiElement.RenderSize.Width * 0.5f), (float)(this.uiElement.RenderSize.Height * 0.5f), -10.0f);

            // Get the contact point of the pointer, in coordinates relative to the Visual.
            var contactPoint = e.GetCurrentPoint(uiElement).Position;

            // Obtain an offset vector from the center and normalize it.
            var contactVector = new Vector3((float)contactPoint.X, (float)contactPoint.Y, 0.0f) - this.elementVisual.CenterPoint;
            contactVector = Vector3.Normalize(contactVector);

            // Swap vector coordinates so they point to the correct corner and the final rotation is correct.
            this.elementVisual.RotationAxis = new Vector3(contactVector.Y, -contactVector.X, 0.0f);

            // Rotate by a set amount of degrees.
            this.elementVisual.RotationAngleInDegrees = 20.0f;
        }

        /// <summary>
        /// Resets the values set by the tilt effect - just the rotation angle.
        /// </summary>
        private void ResetTiltEffect()
        {
            this.elementVisual.RotationAngleInDegrees = 0.0f;
        }
    }

}

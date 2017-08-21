using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using DragViewSample;
using DragViewSample.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using CoreGraphics;

[assembly: ExportRenderer(typeof(DraggableView), typeof(DraggableViewRenderer))]
namespace DragViewSample.iOS.Renderers
{
    
    public class DraggableViewRenderer : VisualElementRenderer<View>
    {
        bool longPress = false;
        double lastTimeStamp = 0f;
        UIPanGestureRecognizer panGesture;
        CGPoint lastLocation;
        UIGestureRecognizer.Token panGestureToken;
        void DetectPan()
        {
            var dragView = Element as DraggableView;
            if (longPress|| dragView.DragMode == DragMode.Touch)
            {
                if (panGesture.State == UIGestureRecognizerState.Began)
                {
                    dragView.DragStarted();
                }

                CGPoint translation = panGesture.TranslationInView(Superview);
                var currentCenterX = Center.X;
                var currentCenterY = Center.Y;
                if (dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Horizontal)
                {
                    currentCenterX = lastLocation.X + translation.X;
                }

                if (dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Vertical)
                {
                    currentCenterY= lastLocation.Y + translation.Y;
                }

                Center = new CGPoint(currentCenterX,currentCenterY);

                if (panGesture.State == UIGestureRecognizerState.Ended)
                {
                    dragView.DragEnded();
                    longPress = false;
                }
            }
        }
 
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement!=null)
            {
                RemoveGestureRecognizer(panGesture);
                panGesture.RemoveTarget(panGestureToken);
            }
            if (e.NewElement != null)
            { 
                panGesture = new UIPanGestureRecognizer();
                panGestureToken =panGesture.AddTarget(DetectPan);
                AddGestureRecognizer(panGesture);
               
            }
          
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            lastTimeStamp = evt.Timestamp;
            Superview.BringSubviewToFront(this);
            lastLocation = Center;
        }
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            if(evt.Timestamp - lastTimeStamp >= 0.5)
            {
                longPress = true;
            }
            base.TouchesMoved(touches, evt);
        }
        bool IsViewOverlapping(UIView view)
        {
            return Frame.IntersectsWith(view.Frame);
        }
    }
}
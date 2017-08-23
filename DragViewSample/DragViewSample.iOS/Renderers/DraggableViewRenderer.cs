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
        bool firstTime = true;
        double lastTimeStamp = 0f;
        UIPanGestureRecognizer panGesture;
        CGPoint lastLocation;
        CGPoint originalPosition;
        UIGestureRecognizer.Token panGestureToken;
        void DetectPan()
        {
            var dragView = Element as DraggableView;
            if (longPress|| dragView.DragMode == DragMode.Touch)
            {
                if (panGesture.State == UIGestureRecognizerState.Began)
                {
                    dragView.DragStarted();
                    if(firstTime)
                    {
                        originalPosition = Center;
                        firstTime = false;
                    }
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
                var dragView = Element as DraggableView;
                panGesture = new UIPanGestureRecognizer();
                panGestureToken =panGesture.AddTarget(DetectPan);
                AddGestureRecognizer(panGesture);


                dragView.RestorePositionCommand = new Command(() =>
                {
                    if(!firstTime)
                    {

                        Center = originalPosition;
                    }
                });

            }
          
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var dragView = Element as DraggableView;
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
       
    }
}
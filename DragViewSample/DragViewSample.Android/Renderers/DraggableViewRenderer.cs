using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AView = Android.Views;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DragViewSample.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DragViewSample;
using System.ComponentModel;
using Android.Graphics;
using Android.Views.Animations;

[assembly: ExportRenderer(typeof(DraggableView), typeof(DraggableViewRenderer))]
namespace DragViewSample.Droid.Renderers
{
    public class DraggableViewRenderer : VisualElementRenderer<Xamarin.Forms.View> 
    {
        float dX;
        float dY;
        bool touchedDown = false;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

          
            if(e.OldElement!=null)
            {
                LongClick -= HandleLongClick;
            }
            if (e.NewElement != null)
            {
                LongClick += HandleLongClick;
                
            }
        
        }

        private void HandleLongClick(object sender, LongClickEventArgs e)
        {
            var dragView = Element as DraggableView;

            dragView.DragStarted();
            
            touchedDown = true;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            float x = e.RawX;
            float y = e.RawY;
            var dragView = Element as DraggableView;
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    if(dragView.DragMode == DragMode.Touch)
                    { 
                        if(!touchedDown)
                        {
                            dragView.DragStarted();
                        }
                      
                        touchedDown = true;
                    }
                    dX = x - this.GetX();
                    dY = y - this.GetY();
                    break;
                case MotionEventActions.Move:
                    if (touchedDown)
                    {
                       if(dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Horizontal)
                       {
                            SetX(x - dX);
                       }

                       if (dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Vertical)
                       {
                            SetY(y - dY);
                       }
                       
                    }
                    break;
                case MotionEventActions.Up:
                    touchedDown = false;
                    dragView.DragEnded();
                    break;
                case MotionEventActions.Cancel:
                    touchedDown = false;
                    break;
            }
            return base.OnTouchEvent(e);
        }

        public override bool OnInterceptTouchEvent(MotionEvent e)
        {

            BringToFront();
            return true;
        }
        bool IsViewOverlapping(AView.View view)
        {
            
            int[] location = new int[2];
            this.GetLocationInWindow(location);
            Rect rect1 = new Rect(location[0], location[1], location[0] + this.Width, location[1] + this.Height);

            view.GetLocationInWindow(location);
            Rect rect2 = new Rect(location[0], location[1], location[0] + view.Width, location[1] + view.Height);

           return rect1.Intersect(rect2);

        }
    }
    

}
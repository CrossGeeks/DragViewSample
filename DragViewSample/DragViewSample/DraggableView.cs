using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragViewSample
{
    public partial class DraggableView : ContentView
    {
        public event EventHandler DragStart= delegate { };
        public event EventHandler DragEnd = delegate { };

        public static readonly BindableProperty DragDirectionProperty = BindableProperty.Create(
            propertyName: "DragDirection",
            returnType: typeof(DragDirectionType),
            declaringType: typeof(DraggableView),
            defaultValue: DragDirectionType.All,
            defaultBindingMode: BindingMode.TwoWay);

        public DragDirectionType DragDirection
        {
            get { return (DragDirectionType)GetValue(DragDirectionProperty); }
            set { SetValue(DragDirectionProperty, value); }
        }


        public static readonly BindableProperty DragModeProperty = BindableProperty.Create(
           propertyName: "DragMode",
           returnType: typeof(DragMode),
           declaringType: typeof(DraggableView),
           defaultValue: DragMode.LongPress,
           defaultBindingMode: BindingMode.TwoWay);

        public DragMode DragMode
        {
            get { return (DragMode)GetValue(DragModeProperty); }
            set { SetValue(DragModeProperty, value); }
        }

        public static readonly BindableProperty IsDraggingProperty = BindableProperty.Create(
          propertyName: "IsDragging",
          returnType: typeof(bool),
          declaringType: typeof(DraggableView),
          defaultValue: false,
          defaultBindingMode: BindingMode.TwoWay);

        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }

        public static readonly BindableProperty RestorePositionCommandProperty = BindableProperty.Create(nameof(RestorePositionCommand), typeof(ICommand), typeof(DraggableView), default(ICommand), BindingMode.TwoWay, null, OnRestorePositionCommandPropertyChanged);

        static void OnRestorePositionCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var source = bindable as DraggableView;
            if (source == null)
            {
                return;
            }
            source.OnRestorePositionCommandChanged();
        }

        private void OnRestorePositionCommandChanged()
        {
            OnPropertyChanged("RestorePositionCommand");
        }

        public ICommand RestorePositionCommand
        {
            get
            {
                return (ICommand)GetValue(RestorePositionCommandProperty);
            }
            set
            {
                SetValue(RestorePositionCommandProperty, value);
            }
        }

        public void DragStarted()
        {
            DragStart(this, default(EventArgs));
            IsDragging = true;
        }

        public void DragEnded()
        {
            IsDragging = false;
            DragEnd(this, default(EventArgs));
        }

    }
}
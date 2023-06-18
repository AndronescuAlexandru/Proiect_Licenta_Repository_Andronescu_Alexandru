using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Proiect_Licenta
{
    public class CustomStepper : StackLayout
    {
        ImageButton PlusBtn;
        ImageButton MinusBtn;

        public static readonly BindableProperty ValueProperty =
          BindableProperty.Create(
             propertyName: "Value",
              returnType: typeof(int),
              declaringType: typeof(CustomStepper),
              defaultValue: 0,
              defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MaxValueProperty =
          BindableProperty.Create(
             propertyName: "Max Value",
              returnType: typeof(int),
              declaringType: typeof(CustomStepper),
              defaultValue: 100,
              defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MinValueProperty =
          BindableProperty.Create(
             propertyName: "Min Value",
              returnType: typeof(int),
              declaringType: typeof(CustomStepper),
              defaultValue: 0,
              defaultBindingMode: BindingMode.TwoWay);

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public int MaxValue
        {   get { return (int)GetValue(MaxValueProperty); } 
            set { SetValue(MaxValueProperty,value); } 
        }
        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty,value); }
        }
        public CustomStepper()
        {
            MinusBtn = new ImageButton { Source = "minus_icon.png" , WidthRequest = 40, BackgroundColor = Color.Transparent };
            PlusBtn = new ImageButton { Source = "plus_icon.png", WidthRequest = 40, BackgroundColor = Color.Transparent };
            switch (Device.RuntimePlatform)
            {

                case Device.UWP:
                case Device.Android:
                    {
                        PlusBtn.BackgroundColor = Color.Transparent;
                        MinusBtn.BackgroundColor = Color.Transparent;
                        break;
                    }
                case Device.iOS:
                    {
                        PlusBtn.BackgroundColor = Color.DarkGray;
                        MinusBtn.BackgroundColor = Color.DarkGray;
                        break;
                    }
            }

            Orientation = StackOrientation.Horizontal;
            PlusBtn.Clicked += PlusBtn_Clicked;
            MinusBtn.Clicked += MinusBtn_Clicked;

            Children.Add(MinusBtn);
            Children.Add(PlusBtn);
        }

        private void MinusBtn_Clicked(object sender, EventArgs e)
        {
            if (Value > MinValue)
                Value--;
        }

        private void PlusBtn_Clicked(object sender, EventArgs e)
        {
            if(Value < MaxValue)
                Value++;
        }

    }
}

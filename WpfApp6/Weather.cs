using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace WpfApp6
{
    class Weather : DependencyObject
    {
        public static readonly DependencyProperty TempProperty;
        private string windDirection;
        private int windSpeed;
        private int presencePrecipitation;
        private string skyPrecipitation;
        public int Temp
        {
            get => (int)GetValue(TempProperty);
            set => SetValue(TempProperty, value);
        }
        public string WindDirection
        {
            get => windDirection;
            set => windDirection = value;
        }
        public int WindSpeed
        {
            get => windSpeed;
            set => windSpeed = value;
        }
        public int Precipitation
        {
            get => presencePrecipitation;

            set
            {
                if (value >= 0 && value <= 3)
                    presencePrecipitation = value;
                else
                    presencePrecipitation = 0;
            }
        }
        public Weather(int temp, string windDirection, int windSpeed, int presencePrecipitation)
        {
            this.Temp = temp;
            this.WindDirection = windDirection;
            this.WindSpeed = windSpeed;
            this.Precipitation = presencePrecipitation;
        }
        static Weather()
        {
            TempProperty = DependencyProperty.Register(
                nameof(Temp),
                typeof(int),
                typeof(Weather),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    new CoerceValueCallback(CoerceTemp)),
                new ValidateValueCallback(ValidateTemp));
        }
        private static bool ValidateTemp(object value)
        {
            int v = (int)value;
            if (v >= -50 && v <= 50)
                return true;
            else
                return false;
        }
        private static object CoerceTemp(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v >= -50)
                return v;
            else
                return 0;
        }
        public string Print()
        {
            switch (presencePrecipitation)
            {
                case 0:
                    {
                        skyPrecipitation = "Cолнечно";
                        break;
                    }
                case 1:
                    {
                        skyPrecipitation = "Облачно";
                        break;
                    }
                case 2:
                    {
                        skyPrecipitation = "Дождь";
                        break;
                    }
                case 3:
                    {
                        skyPrecipitation = "Снег";
                        break;
                    }
                default:
                    {
                        skyPrecipitation = "нет данных";
                        break;
                    }
            }
                return $"{Temp}, {WindDirection}, {WindSpeed}, {skyPrecipitation}";
        }
    }
}

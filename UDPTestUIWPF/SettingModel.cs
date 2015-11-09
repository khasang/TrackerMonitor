using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UDPTestUIWPF
{
    public class SettingModel : DependencyObject
    {
        public static DependencyProperty SendReceiveProperty;
        public static DependencyProperty CycleProperty;
        public static DependencyProperty WriteToDBProperty;
        public static DependencyProperty StateButtonProperty;
        public static DependencyProperty StatusProperty;

        static SettingModel()
        {
            SendReceiveProperty = DependencyProperty.Register("SendReceive",
                                                                typeof(bool),
                                                                typeof(SettingModel),
                                                                new PropertyMetadata(true));

            CycleProperty = DependencyProperty.Register("Cycle",
                                                                typeof(bool),
                                                                typeof(SettingModel),
                                                                new PropertyMetadata(false));

            WriteToDBProperty = DependencyProperty.Register("WriteToDB",
                                                                typeof(bool),
                                                                typeof(SettingModel),
                                                                new PropertyMetadata(false));

            StateButtonProperty = DependencyProperty.Register("StateButton",
                                                                typeof(string),
                                                                typeof(SettingModel),
                                                                new PropertyMetadata("Start"));

            StatusProperty = DependencyProperty.Register("Status",
                                                                typeof(string),
                                                                typeof(SettingModel),
                                                                new PropertyMetadata("Сonnection status"));
        }

        public bool SendReceive
        {
            set { SetValue(SendReceiveProperty, value); }
            get { return (bool)GetValue(SendReceiveProperty); }
        }

        public bool Cycle
        {
            set { SetValue(CycleProperty, value); }
            get { return (bool)GetValue(CycleProperty); }
        }

        public bool WriteToDB
        {
            set { SetValue(WriteToDBProperty, value); }
            get { return (bool)GetValue(WriteToDBProperty); }
        }

        public string StateButton
        {
            set { SetValue(StateButtonProperty, value); }
            get { return (string)GetValue(StateButtonProperty); }
        }

        public string Status
        {
            set { SetValue(StatusProperty, value); }
            get { return (string)GetValue(StatusProperty); }
        }
    }
}

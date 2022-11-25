﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.CustomControls
{
    /// <summary>
    /// Interaction logic for BindableConPasswordBox.xaml
    /// </summary>
    public partial class BindableConPasswordBox : UserControl
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindableConPasswordBox));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set => SetValue(PasswordProperty, value);
        }
        public BindableConPasswordBox()
        {
            InitializeComponent();
            txtConPassword.PasswordChanged += OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = txtConPassword.Password;
        }
    }
}
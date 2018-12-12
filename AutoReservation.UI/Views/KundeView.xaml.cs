﻿using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using AutoReservation.UI.ViewModels;

namespace AutoReservation.UI.Views
{
    /// <summary>
    /// Interaction logic for KundeView.xaml
    /// </summary>
    public partial class KundeView : Window
    {

        public KundeViewModel KundeVm { get; set; }
       

        public KundeView()
        {
            InitializeComponent();

            KundeVm = new KundeViewModel();
            DataContext = KundeVm;
            ListOfKunde.SelectionChanged += KundeVm.SelectedIndexChanged;
        }
    }
}
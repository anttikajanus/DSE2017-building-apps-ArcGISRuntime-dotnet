﻿using Esri.ArcGISRuntime.Mapping;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Windows.Input;
using BuildingAppsDemo.ArcGIS.ViewServices;
using System.Diagnostics;

namespace BuildingAppsDemo
{
    public class BaseViewModel : ViewModelBase
    {
        public BaseViewModel()
        {
            _resetViewPointCommand = new DelegateCommand(ResetViewPoint);
            _zoomInCommand = new DelegateCommand(ZoomIn);
            _zoomOutCommand = new DelegateCommand(ZoomOut);
        }

        private async void ResetViewPoint()
        {
            try
            {
                if (Map.InitialViewpoint != null)
                    await MapViewService.SetViewpointAsync(Map.InitialViewpoint);
            }
            catch (Exception ex)
            {
                // Production level error handling...
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        private DelegateCommand _resetViewPointCommand;
        public ICommand ResetViewPointCommand => _resetViewPointCommand;

        private DelegateCommand _zoomInCommand;
        public ICommand ZoomInCommand => _zoomInCommand;

        private async void ZoomIn()
        {
            try
            {
                var currentScale = MapViewService.MapScale;
                await MapViewService.SetViewpointScaleAsync(currentScale * 0.5);
            }
            catch (Exception ex)
            {
            }
        }

        private DelegateCommand _zoomOutCommand;
        public ICommand ZoomOutCommand => _zoomOutCommand;

        private async void ZoomOut()
        {
            try
            {
                var currentScale = MapViewService.MapScale;
                await MapViewService.SetViewpointScaleAsync(currentScale * 1.5);
            }
            catch (Exception ex)
            {
                // Production level error handling...
                Debug.WriteLine(ex.ToString());
            }
        }

        private MapViewService _mapViewService;
        /// <summary>
        /// Gets the view service that provides access to the MapView
        /// </summary>
        public MapViewService MapViewService
        {
            get
            {
                if (_mapViewService == null)
                    _mapViewService = new MapViewService();

                return _mapViewService;
            }
        }

        private Map _map;
        public Map Map
        {
            get { return _map; }
            set { SetProperty(ref _map, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private string _isBusyText;
        public string IsBusyText
        {
            get { return _isBusyText; }
            set { SetProperty(ref _isBusyText, value); }
        }

        private string _progressPercentage;
        public string ProgressPercentage
        {
            get { return _progressPercentage; }
            set { SetProperty(ref _progressPercentage, value); }
        }
    }
}

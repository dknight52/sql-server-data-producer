﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SQLRepeater.Entities.ExecutionOrderEntities;

namespace SQLRepeater.ViewModels
{
    public class ExecutionDetailsViewModel : ViewModelBase
    {

        SQLRepeater.Model.ApplicationModel _model;
        public SQLRepeater.Model.ApplicationModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged("Model");
                }
            }
        }


        public ExecutionDetailsViewModel(SQLRepeater.Model.ApplicationModel model, ExecutionItem execItem)
        {
            Model = model;
        }
    }
}
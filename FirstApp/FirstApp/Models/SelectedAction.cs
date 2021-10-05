using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Models
{
    public enum Actions
    {
        ShowAuthorization,
        ShowRecords,
        ShowProfile,
        Play,
        MainTab
    }
    public static class SelectedAction
    {

        private static Actions _currentSelectedAction;
        public static Actions CurrentSelectedAction
        {
            get => _currentSelectedAction;
            set
            {
                _currentSelectedAction = value;
            }
        }
    }
}

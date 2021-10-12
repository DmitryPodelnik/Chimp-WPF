using First_App.Models.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace First_App.ViewModels
{
    class ScoreTableViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ScoreTableViewModel()
        {
            _numbersMessage = Counter.Score.ToString();
            _strikesMessage = $"{Counter.Strikes} of {Counter.MAX_STRIKES}";
        }

        private string _numbersMessage;
        public string NumbersMessage
        {
            get => _numbersMessage;
            set
            {
                _numbersMessage = value;
            }
        }
        private string _strikesMessage;
        public string StrikesMessage
        {
            get => _strikesMessage;
            set
            {
                _strikesMessage = value;
            }
        }

    }
}

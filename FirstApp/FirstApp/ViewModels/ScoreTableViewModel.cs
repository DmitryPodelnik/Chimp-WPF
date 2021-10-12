using First_App.Models.Commands;
using First_App.Models.Game;
using First_App.Navigation;
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


        /// <summary>
        ///     Command after clicking continue button.
        /// </summary>
        private RelayCommand _continueGameCommand;
        public RelayCommand ContinueGameCommand
        {
            get
            {
                return _continueGameCommand =
                (_continueGameCommand = new RelayCommand(obj =>
                {
                    // increase current strike
                    Counter.Strikes++;
                    // change to play field interface
                    Navigator.Create().CurrentViewModel = new PlayFieldViewModel();
                }));
            }
        }

    }
}

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shikkhanobish
{
    public class RegisterAsTeacherViewModel
    {
        private bool _check;

        public RegisterAsTeacherViewModel()
        {
            checkIsChecked();
            Check = true;
        }

        public void checkIsChecked()
        {
        }

        public bool Check
        {
            get
            {
                return _check;
            }
            set
            {
                _check = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
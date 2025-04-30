using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Laundrygest_desktop.ViewModel.Dialogs
{
    public class PaymentDialogViewModel : INotifyPropertyChanged
    {
        private decimal _totalPrice;
        private string _paymentMode;
        private decimal _paymentAmount;
        private decimal _remainingAmount;
        private decimal _returnAmount;
        private bool _isCard;
        public Action<string, decimal> PaymentModeReturn { get; set; }
        public PaymentDialogViewModel(decimal totalPrice,decimal dueTotal)
        {            
            ReturnAmount = 0.0m;
            _totalPrice = totalPrice;
            _remainingAmount = dueTotal;
            PaymentAmount = totalPrice-dueTotal;
            CardPayCommand = new DelegateCommand(PayWithCard);
            CashPayCommand = new DelegateCommand(PayWithCash);
            GetTotalCommand = new DelegateCommand(GetTotalAmount);
        }

        public string PaymentMode
        {
            get { return _paymentMode; }
            set { _paymentMode = value; OnPropertyChanged(); }
        }

        public decimal PaymentAmount
        {
            get { return _paymentAmount; }
            set {
                _paymentAmount = value; 
                OnPropertyChanged();
                if (_totalPrice >= _paymentAmount)
                {
                    RemainingAmount = _totalPrice - _paymentAmount;
                    ReturnAmount = 0.0m;
                }else
                {
                    RemainingAmount = 0.0m;
                    ReturnAmount = _paymentAmount - _totalPrice;
                }
            }
        }

        public decimal RemainingAmount
        {
            get
            {
                return _remainingAmount;
            }
            set {
                if (value >= 0.0m)
                {
                    _remainingAmount = value;
                    OnPropertyChanged();
                }
            }
        }
        public decimal ReturnAmount
        {
            get
            {
                return _returnAmount;
            }
            set
            {
                if (value >= 0.0m)
                {
                    _returnAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand CardPayCommand { get; }
        public ICommand CashPayCommand { get; }
        public ICommand GetTotalCommand { get; }

        public void PayWithCard()
        {
            _isCard = true;
            PaymentSelected(null, null);
        }

        public void PayWithCash()
        {
            _isCard = false;
            PaymentSelected(null, null);
        }

        public void GetTotalAmount()
        {
            PaymentAmount = _totalPrice;
        }

        public void PaymentSelected(object paymentMode, object remaining)
        {
            string payment = _isCard ? "Card" : "Cash";
            paymentMode = payment;
            remaining = RemainingAmount;
            PaymentModeReturn?.Invoke((string)paymentMode, (decimal)remaining);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

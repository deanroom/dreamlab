using System;

namespace Delegate
{
    public class NumberEventArgs
    {
        public int  NumberValue { get; set; }
    }

    public class DelegateServer
    {
        public event EventHandler<NumberEventArgs> AddNumber;
        private readonly NumberEventArgs _number ;

        public DelegateServer ()
        {
            _number = new NumberEventArgs();
        }

        public NumberEventArgs Number => _number;
        public void Add()
        {
            _number.NumberValue++;
            AddNumber?.Invoke(this,_number);
        }
    }
}
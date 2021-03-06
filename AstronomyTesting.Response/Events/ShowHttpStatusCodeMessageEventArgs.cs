using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Events
{
    public class ShowHttpStatusCodeMessageEventArgs : EventArgs
    {
        public string ErrorMessage { get; set; }

        public ShowHttpStatusCodeMessageEventArgs(string errorMessage) : base()
        {
            ErrorMessage = errorMessage;
        }
    }
}

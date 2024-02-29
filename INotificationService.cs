using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge
{
    internal interface INotificationService
    {
        void Send(string type, string userId, string message);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge
{
    public class Gateway
    {
        public void Send(string userId, string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Sending {message} to user: {userId}");
            Console.ResetColor();
        }

    }
}

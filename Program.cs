// See https://aka.ms/new-console-template for more information
using CodeChallenge;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("\t\bWelcome to Modak Code Challenge");
        Console.WriteLine("Rate-Limited Notification Service");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("by Julian Ariel Martinez\n");
        Console.ResetColor();


        Console.WriteLine("###################################################");

        NotificationService service = new(new Gateway());

        // Test sending notifications for "News" type
        service.Send("news", "user4", "news 1"); // OK
        service.Send("news", "user4", "news 2"); // Rate limited (exceeds 1 per day limit)
        service.Send("news", "user5", "news 3"); // OK

        // Test sending notifications for "Status" type
        service.Send("status", "user5", "status 1"); // OK
        service.Send("status", "user4", "status 1"); // OK
        service.Send("status", "user5", "status 2"); // OK
        service.Send("status", "user4", "status 2"); // OK
        service.Send("status", "user5", "status 3"); // Rate limited (exceeds 2 per minute limit)

        // Test sending notifications for "Marketing" type
        service.Send("marketing", "user6", "marketing 1"); // OK
        service.Send("marketing", "user4", "marketing 2"); // OK
        service.Send("marketing", "user4", "marketing 2"); // OK
        service.Send("marketing", "user6", "marketing 2"); // OK
        service.Send("marketing", "user6", "marketing 3"); // OK
        service.Send("marketing", "user6", "marketing 4"); // Rate limited (exceeds 3 per hour limit)
        service.Send("marketing", "user4", "marketing 2"); // OK
        service.Send("marketing", "user4", "marketing 4"); // Rate limited (exceeds 3 per hour limit)



        Console.WriteLine("###################################################");

    }
}




// See https://aka.ms/new-console-template for more information

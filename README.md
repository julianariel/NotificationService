# Rate-Limited Notification Service - Code Challenge

## Overview
The Rate-Limited Notification Service is designed to send out email notifications of various types while enforcing rate limits to prevent recipients from receiving too many notifications within specified time limits. This repository contains the implementation of the rate-limited notification service in C# using the .NET framework.

## Code Explanation
The codebase consists of several components:

- **NotificationService**: This is the core component responsible for sending notifications. It enforces rate limits for different notification types based on predefined rules.

- **NotificationType**: Represents a type of notification (e.g., status update, news, marketing) and includes rate limit rules defining how many notifications of this type can be sent within a certain time span.

- **RateLimitRule**: Defines a rate limit rule specifying the maximum number of notifications allowed within a given time span.

- **Gateway**: Represents the external service responsible for sending notifications (e.g., email service).

- **NotificationLog**: Stores a log of notifications sent to each user along with the timestamp.

## Rate Limit Enforcement
The `NotificationService` class includes a method `IsRateLimitExceeded` that checks if the rate limit is exceeded for a given notification type and user ID. It calculates the count of notifications sent within the specified time span and compares it with the maximum allowed count defined in the rate limit rules.

## Test Cases
The repository includes this set of tests:

```c#
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
```


## Execution
The result of executing the service generates this output:

![image](https://github.com/julianariel/NotificationService/assets/11839151/60cc5199-c477-4006-9a26-f5fa636d4e5e)



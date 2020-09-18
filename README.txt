#Azure Service Bus Topic
Azure Ser Bus Topic => Azure Function (need Azure Storage Account) => SendGrid Email API


{
  "ToEmail": "fname.lname@hotmail.com",
  "FromEmail": "fname.lname@hotmail.com",
  "Subject": "AZURE : ServiceBusTopicTriggerEmailSendFunction from ASBT-Trigger-subscription-2 !",
  "Content": "CONTENT : Azure Service Bus Topic, Azure Function & SendGrid Email API"
}

#Azure Service Bus Topic Filter



{
  "ToEmail": "fname.lname@hotmail.com",
  "FromEmail": "fname.lname@hotmail.com",
  "Subject": "LOCAL : ServiceBusTopicTriggerEmailSendFunction from ASBT-Trigger-subscription-1 !",
  "Content": "CONTENT : Azure Service Bus Topic, Azure Function & SendGrid Email API"
}

TriggerFunction=local
TriggerFunction=azure
---
title: Two-factor authentication with SMS | Microsoft Docs
author: rick-anderson
description: Shows how to set up two-factor authentication (2FA) with ASP.NET Core
keywords: ASP.NET Core, SMS, authentication, 2FA, two-factor authentication, two factor authentication 
ms.author: riande
manager: wpickett
ms.date: 04/01/2017
ms.topic: article
ms.assetid: ff1c22d1-d1f2-4616-84dd-94ba61ec299a
ms.technology: aspnet
ms.prod: asp.net-core
uid: security/authentication/2fa
---
# Two-factor authentication with SMS

By [Rick Anderson](https://twitter.com/RickAndMSFT)

This tutorial will show you how to set up two-factor authentication (2FA) using SMS. [twilio](https://www.twilio.com/) is used, but you can use any other SMS provider. We recommend you complete [Account Confirmation and Password Recovery](accconfirm.md) before starting this tutorial.

## Create a new ASP.NET Core project

Create a new ASP.NET Core web app with individual user accounts. Follow the instructions in [Enforcing SSL in an ASP.NET Core app](xref:security/enforcing-ssl) to set up and require SSL.

### Create an SMS account

Create an SMS account, for example, from [twilio](https://www.twilio.com/try-twilio). Record the authentication credentials.

### Provide for the SMS tokens

We'll use the [Options pattern](xref:fundamentals/configuration#options-config-objects) to access the user account and key settings. 

   * Create a class to fetch the secure SMS key. For this sample, the `SMSoptions` class is created in the *Services/SMSoptions.cs* file.

[!code-csharp[Main](2fa/sample/Web2FA/Services/SMSoptions.cs)]

Set the `accountSid` and `authToken` with the [secret-manager tool](xref:security/app-secrets). For example:

```none
C:/Web2FA/src/WebApp1>dotnet user-secrets set accountSid 12345
info: Successfully saved accountSid = 12345 to the secret store.
```

* Add the twilio NuGet package. From the Package Manager Console (PMC) run

`Install-Package Twilio`

* Add code in the *Services/MessageServices.cs* file to enable SMS.

[!code-csharp[Main](2fa/sample/Web2FA/Services/MessageServices.cs)]

### Configure startup to use `SMSoptions`

Add `SMSoptions` to the service container in the `ConfigureServices` method in the *Startup.cs*:

[!code-csharp[Main](2fa/sample/Web2FA/Startup.cs?name=snippet1&highlight=4)]

### Enable two-factor authentication

Open the *Views/Manage/Index.cshtml* Razor view file and remove the comment characters (so no markup is commnted out).

## Log in with two-factor authentication

* Run the app and register a new user

![Web application Register view open in Microsoft Edge](2fa/_static/login2fa1.png)

* Tap on your user name, which activates the `Index` action method in Manage controller. Then tap the phone number **Add** link.

![Manage view](2fa/_static/login2fa2.png)

* Add a phone number that will receive the verification code, and tap **Send verification code**.

![Add Phone Number page](2fa/_static/login2fa3.png)

* You will get a text message with the verification code. Enter it and tap **Submit**

![Verify Phone Number page](2fa/_static/login2fa4.png)

If you don't get a text message, see twilio log page.

* The Manage view shows your phone number was added successfully.

![Manage view](2fa/_static/login2fa5.png)

* Tap **Enable** to enable two-factor authentication.

![Manage view](2fa/_static/login2fa6.png)

### Test two-factor authentication

* Log off.

* Log in.

* The user account has enabled two-factor authentication, so you have to provide the second factor of authentication . In this tutorial you have enabled phone verification. The built in templates also allow you to set up email as the second factor. You can set up additional second factors for authentication such as QR codes. Tap **Submit**.

![Send Verification Code view](2fa/_static/login2fa7.png)

* Enter the code you get in the SMS message.

* Clicking on the **Remember this browser** check box will exempt you from needing to use 2FA to log on when using the same device and browser. Enabling 2FA and clicking on **Remember this browser** will provide you with strong 2FA protection from malicious users trying to access your account, as long as they don't have access to your device. You can do this on any private device you regularly use. By setting  **Remember this browser**, you get the added security of 2FA from devices you don't regularly use, and you get the convenience on not having to go through 2FA on your own devices.

![Verify view](2fa/_static/login2fa8.png)

## Account lockout for protecting against brute force attacks

We recommend you use account lockout with 2FA. Once a user logs in (through a local account or social account), each failed attempt at 2FA is stored, and if the maximum attempts (default is 5) is reached, the user is locked out for five minutes (you can set the lock out time with `DefaultAccountLockoutTimeSpan`). The following configures Account to be locked out for 10 minutes after 10 failed attempts.

[!code-csharp[Main](2fa/sample/Web2FA/Startup.cs?name=snippet2&highlight=13-17)] 

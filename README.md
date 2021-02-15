# AutoGit

This is a library designed around GitHub Apps allowing you to automate events, jobs, release notes, etc... This utilizes [Octokit.net](https://github.com/octokit/octokit.net) to perform operations from the GitHub API.  If you use the Jobs package it leverages the [Hangfire](https://github.com/HangfireIO/Hangfire) package to schedule jobs.  

This is also inspired by the following work, which unfortunately seems deprecated, and is the reason why I have decided to work on this.
- https://github.com/mirsaeedi/octokit.net.extensions
- https://github.com/mirsaeedi/octokit.net.bot

## Development

- [GitHub API Docs](https://docs.github.com/en/rest)
- [Octokit.net Docs](https://octokitnet.readthedocs.io/en/latest/)

### Smee

In order to received GitHub events we need to forward them to our local machine.  The approach from the [GitHub Docs](https://docs.github.com/en/developers/apps/setting-up-your-development-environment-to-create-a-github-app#step-1-start-a-new-smee-channel) is to create a smee channel.

1. In order to get smee to work with our application on local we need to disable HTTPS in your develop environment.  Go to `Project Properties` -> `Debug` and uncheck `Enable SSL` Also note down the Port here for the future. 
1. Install smee
   ```txt
   npm install --global smee-client
   ```
1. Navigate to [https://smee.io/](https://smee.io/)
1. Click `Start a new channel`
1. Copy the smee Webhook Proxy URL
1. Start the smee client.
    - `Url` is the URL copied from above
    - `Path` is the endpoint in your application
    - `Port` is the port your application is running on
    ```txt
    smee --url https://smee.io/qrfeVRbFbffd6vD --path /github/hooks --port 3100
    ```
   You should see something like the following:
   ```txt
   Forwarding https://smee.io/qrfeVRbFbffd6vD to http://127.0.0.1:3000/event_handler
   Connected https://smee.io/qrfeVRbFbffd6vD
   ```

### GitHub App

Follow the [GitHub Docs](https://developer.github.com/apps/quickstart-guides/setting-up-your-development-environment/#step-2-register-a-new-github-app) for how to register a new application.

### Configuration

Some settings from the GitHub app need to be supplied.  Below is a sample of the `appsettings.json` but it is recommended to place sensitive data into your environment secrets.

```json
{
  "ConnectionStrings": {
    "AutoGit": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AutoGit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  },
  "GitHub": {
    "AppIdentifier": 123,
    "AppName": "test-bot",
    "PrivateKey": "Base64 text from the pem cert downloaded when creating your GitHub App"
  }
}
```

You can configure some or all of the features depending on the AutoGit packages loaded in.  Below is an example with everything available loaded in.

```csharp
public void ConfigureServices(IServiceCollection services)
{
   services.AddControllers();
   
   var autoGitOptions = Configuration.GetSection("GitHub").Get<AutoGitOptions>();
   
   var webHookSecret = Configuration.GetValue<string>("GitHub:WebHookSecret");
   
   services.AddGitHubBot(options =>
       {
           options.AppIdentifier = autoGitOptions.AppIdentifier;
           options.AppName = autoGitOptions.AppName;
           options.PrivateKey = autoGitOptions.PrivateKey;
       })
       .AddWebHookHandlers(options =>
       {
           options.WebHookSecret = webHookSecret;
           options.AddHandler<IssueCommentHandler>();
       })
       .AddJobs(options =>
       {
           options.ConnectionString = Configuration.GetConnectionString("AutoGit");
           options.AddRecurringJob<SimpleJob>();
       })
       .AddReleaseNoteGenerator(options =>
       {
           options.VersionTagPrefix = "v";
           options.ManageReleaseNotes = true;
           options.ManageChangeLog = true;
       });
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
   app.UseAutoGitScheduler();
}
```
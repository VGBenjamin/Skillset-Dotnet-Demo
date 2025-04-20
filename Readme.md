# Skillset Example

## Description

This code sample demonstrates building a Copilot Extension using the skillsets in .net core approach rather than a traditional agent. This extension is designed to generate random test and example data for a number of development purposes, by calling publicly available APIs.

### Architectural Model
- **Skillsets**: Define up to 5 API endpoints that Copilot can call directly. Copilot handles all AI interactions, prompt engineering, and response formatting.
- **Agents**: Provide full control over the interaction flow, including custom prompt crafting and specific LLM model selection.

![Architectural comparison between Skillsets and Agents](https://github.com/user-attachments/assets/9c5d6489-afb5-47c2-be73-2561d89dfde3)


### When to Choose Skillsets
Skillsets are ideal when you want to:
- Quickly integrate existing APIs or services without managing AI logic
- Focus purely on your service's core functionality
- Maintain consistent Copilot-style interactions without extensive development
- Get started with minimal infrastructure and setup

Use agents instead if you need:
- Complex custom interaction flows
- Specific LLM model control (using LLMs that aren't provided by the Copilot API)
- Custom prompt crafting
- Advanced state management

## Example Implementation

This extension showcases the skillset approach by providing three simple endpoints that generate random development data:
- Random commit messages
- Lorem ipsum text generation
- Random user data

## Getting Started
1. Using your web browser, log into your GitHub account <b>with your personal account</b>. It could be easier to use a private session to avoid mixing it with your professional account.

2. Logout your professional account and login with your personal one also in VSCode. (you could use VSCode insiders if you prefer but it isn't required)

3. Navigate to your profile’s home page ([https://github.com/yourhandle](https://github.com/yourhandle)).

4. Clone the git repository

```
git clone git@github.com:VGBenjamin/github-copilot-dlw-workshop-5.git
```

5. Once the repository is cloned swith to the develop branch (the main branch contains the full solution).

```
git checkout develop
```

## Build the application

To simplify the workshop the applicaiton is already setup as mutch as possible so we will just complete the missing parts.

1. First we will build an api to get some info from a service. This will simulate an business logic and a call to the database.
To do, edit the ```ExternalApiService.cs``` file and add an impelmentation for the 3 methods.

```
public async Task<string> GetRandomCommitMessage()
{
   var response = await _httpClient.GetStringAsync("https://whatthecommit.com/index.txt");
   return response.Trim();
}

public async Task<string> GetLoremIpsum(LoremIpsumRequest request)
{
   var url = $"https://baconipsum.com/api/?type=all-meat&paras={request.Paragraphs}";
   var response = await _httpClient.GetStringAsync(url);
   return response.Trim();
}

public async Task<string> GetRandomUser()
{
   var response = await _httpClient.GetStringAsync("https://randomuser.me/api/");
   return response;
}
```

2. Please check if ```IExternalApiService``` is injected in the ```Program.cs ``` like this:

```
builder.Services.AddScoped<IExternalApiService, ExternalApiService>();
```

3. Now, lets buils the controller to access to this APi. Edit the ```SkillsetController.cs``` and add the 3 methods:

```
[HttpPost("commit-message")]
public async Task<IActionResult> GetRandomCommitMessage()
{
   var message = await _externalApiService.GetRandomCommitMessage();
   return Ok(message);
}

[HttpPost("lorem-ipsum")]
public async Task<IActionResult> GetLoremIpsum([FromBody] LoremIpsumRequest request)
{
   var text = await _externalApiService.GetLoremIpsum(request);
   return Ok(text);
}

[HttpPost("random-user")]
public async Task<IActionResult> GetRandomUser()
{
   var userData = await _externalApiService.GetRandomUser();
   return Ok(userData);
}
```

4. Please note that those are using a Post and not a GET as the Gihub Copilot skillset only support the POST verb for the moment.

5. Now let's run the application to see if it work. Run it with visual studio or VSCode and check the url http://yourhostname/swagger and test the methods. Please notice that we are using swagger here but this isn't probably the best one anymore as scalar become more popular with .net 9.


## Usage

1. Option 1: Use the dev tunnels in Visual Studio:

Open the devtunnels in Viauls Studio, create a new one with the options public and Permanant. do not use any whitespace or dashes in the name to avoid issues.

2. Option 2: Use ngrok:</br>
2.1 Start up ngrok with the port provided or use the dev tunnels in Visual Studio:

```
ngrok http http://localhost:8080
```

  2.2. Set the environment variables (use the ngrok generated url for the `FDQN`)<br/>


## Accessing the Extension in Chat:

1. In the `Copilot` tab of your Application settings (`https://github.com/settings/apps/<app_name>/agent`)
- Set the app type to "Skillset"
- Specify the following skills
```
Name: random_commit_message
Inference description: Generates a random commit message
URL: https://<your ngrok domain>/Skillset/random-commit-message
Parameters: { "type": "object" }
Return type: String
---
Name: random_lorem_ipsum 
Inference description: Generates a random Lorem Ipsum text.  Responses should have html tags present.
URL: https://<your ngrok domain>/Skillset/lorem-ipsum
Parameters: 
{
   "type": "object",
   "properties": {
      "number_of_paragraphs": {
         "type": "number",
         "description": "The number of paragraphs to be generated.  Must be between 1 and 10 inclusive"
      },
      "paragraph_length": {
         "type": "string",
         "description": "The length of each paragraph.  Must be one of \"short\", \"medium\", \"long\", or \"verylong\""
      }
   }
}
Return type: String
---
Name: random_user
Inference description: Generates data for a random user
URL: https://<your ngrok domain>/Skillset/random-user
Parameters: { "type": "object" }
Return type: String
```

2. In the `General` tab of your application settings (`https://github.com/settings/apps/<app_name>`)
- Set the `Callback URL` to anything (`https://github.com` works well for testing, in a real environment, this would be a URL you control)
- Set the `Homepage URL` to anything as above
3. Ensure your permissions are enabled in `Permissions & events` > 
- `Account Permissions` > `Copilot Chat` > `Access: Read Only`
4. Ensure you install your application at (`https://github.com/apps/<app_name>`)
5. Now if you go to `https://github.com/copilot` you can `@` your skillset extension using the name of your app.

## What can the bot do?

Here's some example things:

* `@skillset-example please create a random commit message`
* `@skillset-example generate a lorem ipsum`
* `@skillset-example generate a short lorem ipsum with 3 paragraphs`
* `@skillset-example generate random user data`

## Implementation

This bot provides a passthrough to a couple of other APIs:

* For commit messages, https://whatthecommit.com/
* For Lorem Ipsum, https://loripsum.net/
* For user data, https://randomuser.me/

## Documentation
- [Using Copilot Extensions](https://docs.github.com/en/copilot/using-github-copilot/using-extensions-to-integrate-external-tools-with-copilot-chat)
- [About skillsets](https://docs.github.com/en/copilot/building-copilot-extensions/building-a-copilot-skillset-for-your-copilot-extension/about-copilot-skillsets)
- [About building Copilot Extensions](https://docs.github.com/en/copilot/building-copilot-extensions/about-building-copilot-extensions)
- [Set up process](https://docs.github.com/en/copilot/building-copilot-extensions/setting-up-copilot-extensions)
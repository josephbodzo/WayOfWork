# WayOfWork

- Sample project showing how we use Mediatr, Structuremap and other libraries.
- Note this project is built using ASP.NET Core, using the full .NET framework, so you will need that [installed](https://www.asp.net/core)
- Note that this is not a good RESTFul API. We just wanted a simple API so we could demonstrate various concepts
-
## How to use

- I have broken this down into various sections, each section is a GIT tag. Check out each tag to see the progression.
- Each section below corresponds to a GIT tag. That section will document what was changed in the project and the commits between the tags will show the steps to fully implement the specific feature.

## Tag : Baseline

- Simple WebAPi project that allowed you to things with books
    - Deliberately kept simple. The project end result will be overkill for something this simple but want to focus on concepts
- Database context is contrived as wanted to keep an actual DB out of this demo.
    - Not how we would normally do this
- Controllers do a lot of work and we will want to a lot of this going forward.
    - We want to isolate units of work
    - We want to move out validation
    - We 'new up' objects instead of injecting them
    - We want to add logging, security etc without having to rely on WebAPI attributes or middleware.
        - This is important as a lot of time we find that we share logic between API's, Back-end Services, etc
    - We having to set HTTP results manually i.e. if we have validation errors then we return BadRequest in multiple places.
- Note to test the various functions you need to use a tool like [Postman](https://www.getpostman.com/).
    - Remember to set you Accept header
    - We will be implementing SwashBuckle in the next tag to have a self documenting API

## Tag : API Documentation
- We add SwashBuckle to generate a self-documenting site for our API.
- See the GitHub project [Page](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) for more details
    - If you are not using ASP.NET Core then see [Older Version](https://github.com/domaindrivendev/Swashbuckle)
    - Note that the Core version of SwashBuckle was still in beta at the time of writing this so it might change
- We have change the startup of the site to now automatically launch the new UI.
    - We added a index.html file to redirect to the swagger interface
    - We added the static file middle-ware (see startup.cs)
- You can try out the calls from the UI.
    - For calls that need a body you can see the expected model on the right hand side of the call. Click on it and it will add the default model to the parameters text field.
    - You can then modify any values and click 'Try it out' to invoke the method
- We are going to add various attributes, classes and comments to control the output of swagger over the next few commits.

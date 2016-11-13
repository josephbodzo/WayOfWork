# WayOfWork

- Sample project showing how we use Mediatr, Structuremap and other libraries.
- Note this project is built using ASP.NET Core, using the full .NET framework, so you will need that [installed](https://www.asp.net/core)

## How to use

- I have broken this down into various sections, each section is a GIT tag. Check out each tag to see the progression.
- Each section below corresponds to a GIT tag. That section will document what was changed in the project

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
    - We having to set Http results manually i.e. if we have validation errors then we return BadRequest in multiple places.
- Note to test the various functions you need to use a tool like [Postman](https://www.getpostman.com/).
    - Remember to set you Accept header
    - We will be implementing SwashBuckle in the next tag to have a self documenting API


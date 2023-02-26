# Game Design Project

Project for Game Design class.

## Installation and Running

This project [lives on github](https://github.com/scott-parkhill/game_design_project). Simplest thing to do is clone the repo and run it.

### System Requirements

This project requires the [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).

### How to Run

To run the project, `cd` into the directory that contains the `Chaos.csproj` file. If you cloned from git, this should be the `game_design_project` directory.

Next, type in `dotnet run`. This will build the project and run it on a local development server. If you control-click the address (`http://localhost:5004`), it should open in your browser and be good to go.

The database is also local because I didn't want to pay for a proper database, so it is an sqlite database. No credentials needed for querying or accessing.

## Development

Here are instructions related to development on this project.

### Pulling from Git

```sh
git checkout main
git pull
git checkout NolanDev
git merge main
```

To make sure you're developing on your dev branch, type `git branch` to see your active branch.

### Compiling the SCSS

Install the SCSS compiler with `npm`:

`npm install -g sass postcss autoprefixer`

To run the compiler, execute:

`sass ./wwwroot/css/scss/:./wwwroot/css/ --no-source-map --style=expanded`

Run the compiler in watch mode in the background when working with SCSS files.

`sass ./wwwroot/css/scss/:./wwwroot/css/ --no-source-map --style=expanded --watch &`

To end the process, type `jobs` to get the job id (likely `1`), then use `kill %job_id` to stop it, i.e. `kill %1`.

Once you are satisified with the output, and before making a PR, execute:

`postcss ./wwwroot/main.css --no-map --use autoprefixer --output ./wwwroot/main.css'`.

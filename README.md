# Game Design Project

Project for Game Design class.

## Compiling the SCSS

Install the SCSS compiler with `npm`:

`npm install -g sass`

Run the compiler in watch mode in the background when working wish SCSS files.

`sass ./wwwroot/css/scss/:./wwwroot/css/ --no-source-map --style=expanded --watch &`

To end the process, type `jobs` to get the job id (likely `1`), then use `kill %job_id` to stop it, i.e. `kill %1`.

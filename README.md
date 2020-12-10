# FCC Request Header Parser Microservice

The `master` branch of this repository represents a fully usable reference ASP.NET Core application that fulfills the [FreeCodeCamp Request Header Parser Microservice](https://www.freecodecamp.org/learn/apis-and-microservices/apis-and-microservices-projects/request-header-parser-microservice) requirements.

The code in this repository is **not** meant to be used by unscrupulous developers as a shortcut to completing the microservice challenge. Instead, this is a companion to a course I wrote (available [on my website](https://www.christianlevesque.io/blog/fcc/header-parser/)) teaching developers with C# experience how to write backends in ASP.NET Core. This repository is just a reference for those following the course, and it also contains some helpful ancillary material for users, such as deployment scripts that users can modify for their own applications.

## Deployment scripts

The only parts of this code meant to be used by others are the deployment scripts. There is one deployment script that works for BASH (macOS and most Linux distros support BASH scripts), and another deployment script that works for PowerShell (technically, PowerShell exists for all major platforms, but it's mainly only for Windows users).

Both scripts have been documented with comments and divided into clear sections. The top section is where you can make changes to specific variables used in the script, which will alter the values used by the script. You may need to modify other sections of the script to suit your needs.

### BASH script (`deploy.sh`)

The BASH script has been tested on Ubuntu, but it should also work on macOS. To make the script work, you need to modify the `CREDENTIAL` variable on `line 20`, but other than that, the script can work as-is.

#### Variables

- `BUILD_DIR` This variable represents the output directory of the `dotnet publish` command. This also represents the name of the directory in which the application will be uploaded to your server, relative to your SSH root directory (if you SSH into `~`, the application will be uploaded to `~/$BUILD_DIR`). If your app is in version control, whatever value `BUILD_DIR` has should be added to your `.gitignore`. You do not need to set this value for the build script to work. Default is `publish`.
- `BUILD_TARGET` This variable represents the target runtime for the executable produced by `dotnet publish`. A list of available runtimes can be found [in the .NET Core documentation](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog#using-rids). You do not need to set this value for the build script to work. Default is `linux-x64`.
- `FILENAME` This variable represents the name of the archive to create. Whatever name you give here should be added to your `.gitignore` file if you use version control. You do not need to set this value for the build script to work. Default is `headerparser.tar`.
- `CREDENTIAL` This variable represents the SSH credential for your server in `username@server` format. If you have `~/.ssh/config` set up on your machine, you can also use the host identifier given to a particular SSH configuration. **NOTE:** you will have to change this value in order for the script to work because the default value is set to an environment variable from my development machine.

### PowerShell script (`deploy.ps1`)

The PowerShell script has been tested and works. However, there is a bug in the Linux version of PowerShell that prevents file permissions from being archived properly with the `Compress-Archive` commandlet - and there's no way to fix it because after the archive is extracted, trying to change the permissions corrupts the files. Long story short, if you're on a non-Windows machine, either use the BASH script or use the native `zip` utility to compress the archive instead. You can also modify the script to use `tar` if you prefer.

#### Variables

- `$build_dir` This variable represents the output directory of the `dotnet publish` command. This also represents the name of the directory in which the application will be uploaded to your server, relative to your SSH root directory (if you SSH into `~`, the application will be uploaded to `~/$build_dir`). If your app is in version control, whatever value `$build_dir` has should be added to your `.gitignore`. You do not need to set this value for the build script to work. Default is `publish`.
- `$build_target` This variable represents the target runtime for the executable produced by `dotnet publish`. A list of available runtimes can be found [in the .NET Core documentation](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog#using-rids). You do not need to set this value for the build script to work. Default is `linux-x64`.
- `$filename` This variable represents the name of the archive to create. Whatever name you give here should be added to your `.gitignore` file if you use version control. You do not need to set this value for the build script to work. Default is `headerparser.tar`.
- `$credential` This variable represents the SSH credential for your server in `username@server` format. **NOTE:** you will have to change this value in order for the script to work because the default value is set to an environment variable from my development machine.
- `$remote_deploy_path` This variable represents the SCP destination path for copying the archive to your server. This variable should not be modified. This variable only exists because PowerShell doesn't support string interpolation the way BASH does, so the SCP destination (the `$credential` variable followed by a colon) has to be constructed from a formatted string ahead of time.
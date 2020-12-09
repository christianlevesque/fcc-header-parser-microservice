$remote_deploy_path = "{0}:" -f $env:CL_BLOG_CREDENTIAL

Write-Host "Removing previous build..."
Remove-Item publish -Recurse
Remove-Item headerparser.zip
Write-Host "...done"

Write-Host "Creating executable..."
dotnet publish -c release -o publish -r linux-x64 --self-contained false
Write-Host "...done"

Write-Host "Packaging for deployment..."

# Note: Compress-Archive is bugged on Linux systems
# Files are archived with 000 permissions, and
# running `sudo chmod -R 644 publish` will
# just corrupt the files after extraction
Compress-Archive -Path publish -DestinationPath headerparser.zip
Write-Host "...done"

Write-Host "Clearing old deployment from server..."
ssh $env:CL_BLOG_CREDENTIAL "rm -r publish"
Write-Host "...done"

Write-Host "Uploading to server..."
scp headerparser.zip $remote_deploy_path
Write-Host "...done"

Write-Host "Extracting on server..."
ssh $env:CL_BLOG_CREDENTIAL "unzip headerparser.zip"
Write-Host "...done"

Write-Host "Removing deployment archive from server..."
ssh $env:CL_BLOG_CREDENTIAL "rm headerparser.zip"
Write-Host "...done"

Write-Host "To run Header Parser, log into remote server and execute ~/publish/header-parser"
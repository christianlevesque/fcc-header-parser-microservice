#!/usr/bin/env bash

printf "Removing previous build...\n"
rm -r publish
rm headerparser.tar
printf "...done\n"

printf "Creating executable...\n"
dotnet publish -c release -o publish -r linux-x64 --self-contained false
printf "...done\n"

printf "Packaging for deployment...\n"
tar -rf headerparser.tar publish
printf "...done\n"

printf "Clearing old deployment from server...\n"
ssh $CL_BLOG_CREDENTIAL "rm -r publish"
printf "...done\n"

printf "Uploading to server...\n"
scp headerparser.tar $CL_BLOG_CREDENTIAL:
printf "...done\n"

printf "Extracting on server...\n"
ssh $CL_BLOG_CREDENTIAL "tar -xf headerparser.tar"
printf "...done\n"

printf "Removing deployment archive from server...\n"
ssh $CL_BLOG_CREDENTIAL "rm headerparser.tar"
printf "...done\n"

echo "To run Header Parser, log into remote server and execute ~/publish/header-parser"
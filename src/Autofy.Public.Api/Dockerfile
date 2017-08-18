FROM microsoft/dotnet:onbuild

# Configure the listening port to 80
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80

# Copy the app

# Set the Working Directory
WORKDIR /dotnetapp

# Start the app
ENTRYPOINT dotnet run

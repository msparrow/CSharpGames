# Use the official .NET SDK 8.0 image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY ["CSharpGames.csproj", "./"]
RUN dotnet restore

# Copy the rest of the project files
COPY . ./

# Build and publish the app
RUN dotnet publish -c Release -o out

# Build runtime image
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Copy the published output from the build stage
COPY --from=build /app/out/wwwroot .

# Copy nginx configuration
COPY nginx.conf /etc/nginx/nginx.conf

# Expose port 80
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]
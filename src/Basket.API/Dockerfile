FROM microsoft/aspnetcore:2.2
# Create directory for the app source code
RUN mkdir -p /usr/src/basketapi
WORKDIR /usr/src/basketapi
# Copy the source and restore dependencies
COPY . /usr/src/basketapi
RUN dotnet restore

# Expose the port and start the app
EXPOSE 5000
CMD [ "dotnet", "run" ]
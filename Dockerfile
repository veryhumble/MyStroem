
############################################################
# Build Container
############################################################
FROM microsoft/dotnet:2.1-sdk as build
LABEL maintainer="fivenine GmbH"

WORKDIR /app

# copy and build everything
COPY . ./
RUN dotnet publish -r linux-arm -c Release -o out

############################################################
# Application Container
############################################################

FROM microsoft/dotnet:2.1-runtime
LABEL maintainer="fivenine GmbH"

WORKDIR /app

COPY --from=build ./app/MyStroem/out/ .

CMD ["./MyStroem"]
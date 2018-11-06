
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

FROM microsoft/dotnet:2.1.5-runtime-stretch-slim-arm32v7
LABEL maintainer="fivenine GmbH"

WORKDIR /app

COPY --from=build ./app/MyStroem/out/ .

CMD ["./MyStroem"]
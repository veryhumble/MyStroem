
############################################################
# Build Container
############################################################
FROM microsoft/dotnet:2.2-sdk AS build
LABEL maintainer="fivenine GmbH"

WORKDIR /app

# copy and build app and libraries
WORKDIR /app/
COPY . ./
# add IL Linker package
WORKDIR /app/MyStroem/
# RUN dotnet add package ILLink.Tasks -v 0.1.5-preview-1841731 -s https://dotnet.myget.org/F/dotnet-core/api/v3/index.json
RUN dotnet publish -c Release -o out /p:ShowLinkerSizeComparison=true

############################################################
# Application Container
############################################################

FROM microsoft/dotnet:2.2-runtime AS runtime
LABEL maintainer="fivenine GmbH"

WORKDIR /app

COPY --from=build ./app/MyStroem/out/ .

ENTRYPOINT ["dotnet", "./MyStroem.dll"]
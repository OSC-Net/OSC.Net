[![NuGet Package](https://img.shields.io/nuget/v/OSC.Net)](https://www.nuget.org/packages/OSC.Net/)

# OSC.Net
.NET Standard Library for Open Spherical Camera API 2.0

## Example
```csharp
var cameraClient = new CameraClient();
cameraClient.TakePicture("test.jpg");
```

## Client

The library exposes `CameraClient` class which implements an `ICameraClient` interface, which has a set of extension methods on it i.e. `TakePicture()`.

### Create client default IP (192.168.42.1) and Port (80)
```csharp
var cameraClient = new CameraClient();
```

### Create client supply IP and Port
```csharp
var cameraClient = new CameraClient(new IPEndPoint(IPAddress.Parse("192.168.42.1"), 80));
```

### Create client supply Uri
```csharp
var cameraClient = new CameraClient(new Uri("http://192.168.42.1"));
```

### HttpClientFactoryHandler

All `CameraClient` constructors takes an optional `HttpClientFactoryHandler` `createClient`parameter which allows you to override how the internally used HttpClient is created.

Example usage with `System.Net` `IHttpClientFactory` 
```csharp
var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

var cameraClient = new CameraClient(createClient: httpClientFactory.CreateClient);
```

### CreateFileHandler

All `CameraClient` constructors takes an optional `CreateFileHandler` `createFile`parameter which allows you to override how local files are created.

```csharp
var cameraClient = new CameraClient(
    createFile: path => File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None));
```

## Take Picture

### Take picture and get uri to image

```csharp
var pictureUri = await client.TakePicture();
```

### Take picture and download image to supplied stream

```csharp
var imageStream = new MemoryStream();
await client.TakePicture(imageStream);
```

### Take picture and save to supplied local path
```csharp
await client.TakePicture("test.jpg");
```

### useLocalFileUri

All `TakePicture` methods takes an optional `useLocalFileUri` bool parameter, by default it's false. If set to true it'll use `ICameraClient.EndPoint` for construction an absolute uri to images. This is useful if using camera through proxy or firewall.

## Supported cameras

This library has been tested with
* [Insta360 One X](https://www.insta360.com/product/insta360-onex/)
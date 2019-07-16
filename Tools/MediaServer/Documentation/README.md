Media Server
-------------------------------

The media server was made to work with the Cooper platform. At the moment, the media server supports uploading and receiving images.

# Table of contents

- [Starter Guide](#starter-guide)
  - [Uploading](#uploading)
    - [Uploading images](#uploading-images)
  - [Receiving](#receiving)
    - [Receiving images](#receiving-images)

# Starter Guide

## Uploading
The media server currently supports uploading these types of files:
- [images](#uploading-images);

### Uploading images
The media server supports downloading these types of images: *.png, *.jpg, *.jpeg.
Image upload to server is performed using the following URL: hostName + /api/image/upload
All image formats are eventually resized to 1000 pixels on the larger side (if they were originally more than 1000 pixels) and converted to *.jpg format.

The transfer is carried out in bytes, for this the downloaded file must be translated into bytes. To do this, you can use the following method:

```csharp
private byte[] GetBytes(IFormFile file)
        {
            byte[] fileBytes = new byte[0];

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
            }

            return fileBytes;
        }
```

This method can be found in [this library](https://github.com/vanmxpx/ISDPlatform/tree/master/Tools/MediaServer/Utility), which is used by the media server.

The media server, if the image is successfully added, will return a Json object with the image name. If the media server for some reason could not add an image (incorrect file format or too large size), then it will return the Json object with an error. To upload an image to a media server, you can use the following code.
**ATTENTION: In my case the host is localhost: 52879, in your case it may be different.**

```csharp 
public async Task<string> AddImageAsync(IFormFile file)
{
    byte[] bytes = GetBytes(file);
    string filePath = null;

    using (var client = new HttpClient())
    {
        using (var content = new MultipartFormDataContent())
        {
            content.Add(new StreamContent(new MemoryStream(bytes)), file.FileName, file.FileName);

            using (HttpResponseMessage message = await client.PostAsync("http://localhost:52879/api/image/upload", content))
            {
                string input = await message.Content.ReadAsStringAsync();
                JObject parsedString = JObject.Parse(input);
                JToken fileNameToken = parsedString.SelectToken("fileName");

                if (fileNameToken != null)
                {
                    filePath = receiptURL + fileNameToken.ToString();
                }
                else
                {
                    // You can get an error message with this code:
                    // string errorMessage = parsedString.SelectToken("errorMessage");
                }
            }
        }
    }

    return filePath;
}
```

## Receiving
The media server currently supports the receiving file types:
- [images](#receiving-images);

### Receiving images
The media server supports receiving these types of images: *.jpg
Image receive from server is performed using the following URL: hostName + /api/image/{filename}
**ATTENTION: In my case the host is localhost: 52879, in your case it may be different.**

Before you just return the finished link in the format host + route + fileName. I advise you to check the success of this link, as this image may simply not be on the media server and it will return the code 404. For this, I recommend using the following code:

```csharp
public async Task<string> GetImageAsync(string filename)
{
    string filePath = "http://localhost:52879/api/image/" + filename;

    using (var client = new HttpClient())
    {
        using (HttpResponseMessage response = await client.GetAsync(filePath))
        {
            if (response.IsSuccessStatusCode)
            {
                return filePath;
            }
            else
            {
                return null;
            }
        }         
    }
}
```

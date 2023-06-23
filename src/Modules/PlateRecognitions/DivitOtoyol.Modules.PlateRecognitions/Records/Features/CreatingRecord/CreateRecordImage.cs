using System.Drawing;
using System.Drawing.Imaging;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Requests;
using SharpCompress.Archives;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord;

public class CreateRecordImage
{
    private readonly CreateRecordImageRequest recordImage;

    public CreateRecordImage(CreateRecordImageRequest recordImage)
    {
        this.recordImage = recordImage;
    }

    private static void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path + '\\'))
        {
            Directory.CreateDirectory(path + "\\");
        }
    }

    private static ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }
        return null;
    }

    private void SaveSmallImage(string path, Image image, ImageCodecInfo encoder, EncoderParameters eps, string smallImageSize)
    {
        try
        {
            Size size = new Size();
            int smallWidth = int.Parse(smallImageSize);
            int smallHeight = (int)((double)image.Height * (double.Parse(smallImageSize) / (double)image.Width));
            if (smallWidth > 0 && smallHeight > 0)
                size = new Size(smallWidth, smallHeight);
            else
                size = new Size(image.Width, image.Height);
            var smallImage = ResizeImage(image, size);
            smallImage.Save(path, encoder, eps);
        }
        catch (Exception e)
        {
            // Consider throwing an exception or returning the error message here.
        }
    }

    public static Image ResizeImage(Image imgToResize, Size size)
    {
        return (Image)new Bitmap(imgToResize, size);
    }

    public string SaveImage()
    {
        MemoryStream ms = null;

        try
        {
            EnsureDirectoryExists(recordImage.BasePath);

            var imageDataBytes = Convert.FromBase64String(recordImage.ImageData);
            ms = new MemoryStream(imageDataBytes);
            var ss = Image.FromStream(ms);

            var path = NewRecordImagePath();
            var smallImagePath = NewRecordSmallImagePath();

            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            System.Drawing.Imaging.Encoder myencoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameter ep = new EncoderParameter(myencoder, 70L);
            var eps = new EncoderParameters(1);
            eps.Param[0] = ep;

            WriteImageData(ss, path);

            SaveSmallImage(smallImagePath, ss, jgpEncoder, eps, recordImage.SmallImageSize);

            return path;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
        finally
        {
            ms?.Close();
        }
    }

    private bool TryImageSave(Image image, string path)
    {
        try
        {
            image.Save(path, ImageFormat.Jpeg);
            return true;
        }
        catch (Exception ex)
        {
            File.AppendAllText(@"E:\Divit\Logs\WriteImageData.log", $"Error Type:1 {Environment.NewLine}Message:{ex.Message} {Environment.NewLine}Path: {path} {Environment.NewLine} {Environment.NewLine}");
            return false;
        }
    }

    public void WriteImageData(Image image, string path)
    {
        int tryCount = 0;
        int maxTryCount = 5;
        while (!TryImageSave(image, path))
        {
            tryCount++;
            if (tryCount >= maxTryCount)
            {
                File.AppendAllText(@"E:\Divit\Logs\WriteImageData.log", $"Error Type:2 {Environment.NewLine}Message:{maxTryCount} exceeded. Failed. {Environment.NewLine}Path: {path} {Environment.NewLine} {Environment.NewLine}");
                break;
            }
        }
    }

    private string CreateDirectoryPath()
    {
        var directory = Path.Combine(
            recordImage.BasePath,
            recordImage.LprDate.ToLocalTime().Year.ToString(),
            recordImage.LprDate.ToLocalTime().Month.ToString(),
            recordImage.LprDate.ToLocalTime().Day.ToString(),
            recordImage.CameraName,
            recordImage.LprDate.ToLocalTime().Hour.ToString());

        Directory.CreateDirectory(directory);

        if (!directory.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            // Add the separator
            directory += Path.DirectorySeparatorChar;
        }

        return directory;
    }

    private string CreateFilename(bool isSmallImage)
    {
        string uniqueSuffix = Guid.NewGuid().ToString();

        string filename = recordImage.Plate + "-" + recordImage.PlateCountry + "_" + recordImage.CameraBiosName + "_" + recordImage.LprDate.ToLocalTime().ToString("yyyy-MM-dd_HH-mm-ss-fff") + "_";
        filename += AppendProperty(recordImage.TypeName, "0");
        filename += AppendProperty(recordImage.MakeName, "0");
        filename += AppendProperty(recordImage.ModelName, "0");
        filename += AppendProperty(recordImage.ColorName, "0");
        filename += AppendProperty(recordImage.Speed.ToString(), "0");
        filename += AppendProperty(recordImage.PlatePos, "0"); // platepos aracı yakaladığı dikdörtgen koordinatı
        filename += recordImage.NRead.ToString() + "_" + recordImage.Score.ToString(); // nread aracın kaç kere geçtiği (sunucu bazlı), score başarı yüzdesi
        if (!isSmallImage)
        {
            filename += recordImage.IsNight ? "_0_0_0_0_BW.jpg" : "_0_0_0_0_CL.jpg";
        } else
        {
            filename += recordImage.IsNight ? "_[BW][]_SMALL_.jpg" : "_[CL][]_SMALL_.jpg";
        }


        filename = Path.ChangeExtension(filename, uniqueSuffix + Path.GetExtension(filename)); // Benzersiz ismi dosya uzantısıyla birleştir

        return filename;
    }

    private static string AppendProperty(string property, string defaultVal)
    {
        return (!string.IsNullOrEmpty(property) && !property.Equals("YOK")) ? property + "_" : defaultVal + "_";
    }

    public string NewRecordImagePath()
    {
        if (recordImage.CameraName != null)
        {
            var directory = CreateDirectoryPath();
            var filename = CreateFilename(false);

            return directory + filename;
        }
        else
        {
            return string.Empty;
        }
    }
    public string NewRecordSmallImagePath()
    {
        if (recordImage.CameraName != null)
        {
            var directory = CreateDirectoryPath();
            var filename = CreateFilename(true);

            return directory + filename;
        }
        else
        {
            return string.Empty;
        }
    }
}

using CliWrap;

namespace JanusWeb.Data;

public class VideoMergerService
{
    private readonly string _ffmpegPath;

    public VideoMergerService(string ffmpegPath)
    {
        _ffmpegPath = ffmpegPath;
    }

    public async Task MergeAdsToSingleFile(IEnumerable<string> videoPaths, string newFilePath)
    {
        var convertedVideoPaths = await ConvertVideosToSingleFormat(videoPaths);
        var file = await CreateFileWithSegmentsPaths(Path.GetTempPath(), convertedVideoPaths);
        try
        {
            var cmd = Cli.Wrap(_ffmpegPath)
                .WithArguments($"-f concat -safe 0 -i \"{file}\" -c:v h264 \"{newFilePath}\"");
            await cmd.ExecuteAsync();
        }
        catch (Exception e)
        {
            Console.Write(e);
        }
        finally
        {
            File.Delete(file);
        }
    }

    private async Task<string> CreateFileWithSegmentsPaths(string directoryPath, IEnumerable<string> filePaths)
    {
        var textFileWithFilePaths = Path.Combine(directoryPath, $"{Guid.NewGuid()}.txt");
        await using var file = new StreamWriter(textFileWithFilePaths);
        foreach (var filePath in filePaths)
        {
            if (string.IsNullOrEmpty(filePath)) continue;
            await file.WriteLineAsync($"file '{filePath}'");
        }

        return textFileWithFilePaths;
    }

    private async Task<List<string>> ConvertVideosToSingleFormat(IEnumerable<string> videoPaths)
    {
        var result = new List<string>();
        foreach (var videoPath in videoPaths)
        {
            var newPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.mp4");
            if (!await CovertVideoToH264(videoPath, newPath)) continue;
            result.Add(newPath);
        }
        return result;
    }

    private async Task<bool> CovertVideoToH264(string videoPath, string newPath)
    {
        try
        {
            var cmd = Cli.Wrap(_ffmpegPath)
                .WithArguments($"-i \"{videoPath}\" -c:v h264 -crf 23 -preset medium -c:a copy \"{newPath}\"");
            await cmd.ExecuteAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}
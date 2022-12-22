﻿using System.Drawing.Imaging;
using TagsCloudContainer.TextReaders;
using TagsCloudContainer.Visualisators;
using TagsCloudContainer.WorkWithWords;

namespace TagsCloudContainer.Application
{
    public class ConsoleApp : IApp
    {
        private TextReaderGenerator _readerGenerator;
        private Settings _settings;
        private WordHandler _handler;
        private readonly IVisualisator _visualisator;

        public ConsoleApp(TextReaderGenerator readerGenerator, Settings settings,
            WordHandler handler, IVisualisator visualisator)
        {
            _readerGenerator = readerGenerator;
            _settings = settings;
            _handler = handler;
            _visualisator = visualisator;
        }

        public void Run()
        {
            var text = GetText(_settings.FileName);
            var boringText = GetText(_settings.BoringWordsFileName);
            var words = _handler.ProcessWords(text, boringText);
            var bitmap = _visualisator.Paint(words);

            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            bitmap.Save(projectDirectory + "\\Results", "Rectangles", ImageFormat.Png);
        }

        private Result<string> GetText(string fileName)
        {
            if (!File.Exists(fileName))
                return Result.Fail<string>($"File is not exists: {fileName}");
            var result = _readerGenerator
                .GetReader(fileName)
                .Then(x => x.GetTextFromFile(fileName))
                .RefineError("Can't get text from file");
            return result;
        }
    }
}
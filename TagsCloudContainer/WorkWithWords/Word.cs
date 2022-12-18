﻿namespace TagsCloudContainer.WorkWithWords
{
    public class Word
    {
        public string Value { get; }
        public int Count { get; set; }
        public float Size { get; set; }

        public Word(string value)
        {
            Value = value;
            Count = 1;
        }

        public void GenerateSize(Settings settings, int wordsCount)
        {
            Size = Count / (float)wordsCount * 100 * settings.WordFontSize;
        }
    }
}
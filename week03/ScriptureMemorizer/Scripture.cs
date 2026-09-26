using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference reference;
    private List<Word> words;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = new List<Word>();
        foreach (string word in text.Split(" "))
        {
            words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int count)
    {
        Random rand = new Random();
        for (int i = 0; i < count; i++)
        {
            int index = rand.Next(words.Count);
            words[index].Hide();
        }
    }

    public string GetDisplayText()
    {
        string scriptureText = string.Join(" ", words.ConvertAll(w => w.GetDisplayText()));
        return $"{reference.GetDisplayText()} - {scriptureText}";
    }

    public bool IsCompletelyHidden()
    {
        return words.TrueForAll(w => w.IsHidden());
    }
}

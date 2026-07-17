using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        _random = new Random();

        string[] parts = text.Split(' ');

        foreach (string part in parts)
        {
            _words.Add(new Word(part));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        int hidden = 0;

        while (hidden < numberToHide && !IsCompletelyHidden())
        {
            int index = _random.Next(_words.Count);

            if (!_words[index].IsHidden())
            {
                _words[index].Hide();
                hidden++;
            }
        }
    }

    public string GetDisplayText()
    {
        string text = _reference.GetDisplayText() + " ";

        foreach (Word word in _words)
        {
            text += word.GetDisplayText() + " ";
        }

        return text;
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }

        return true;
    }
}

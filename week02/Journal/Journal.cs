using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayJournal()
    {
        foreach (Entry e in _entries)
        {
            e.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry e in _entries)
            {
                outputFile.WriteLine($"{e._date}|{e._prompt}|{e._response}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        _entries.Clear();

        // ✅ Declare 'lines' properly here
        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3) // safety check
            {
                Entry e = new Entry();
                e._date = parts[0];
                e._prompt = parts[1];
                e._response = parts[2];
                _entries.Add(e);
            }
        }
    }
}

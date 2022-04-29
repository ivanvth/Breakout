using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Breakout.LevelData
{
    public class LevelManager
    {
        private StreamReader streamReader;

        private string[,] map;
        private Dictionary<string, string> metaData = new Dictionary<string, string>();
        private Dictionary<char, string> legend = new Dictionary<char, string>();

        private List<Level> levels;

        private string[] filenames;
        public Level CurrentLevel {get; private set; }

        public LevelManager(string[] filenames)
        {
            this.filenames = filenames;
            levels = new();
            AddLevels();
        }

        public void AddLevels() {
            
            foreach (string filename in filenames) {
                metaData = new Dictionary<string, string>();
                legend = new Dictionary<char, string>();
                try {
                    streamReader = new StreamReader(filename);
                    ReadData();
                    CreateLevel();
                } catch (FileNotFoundException e) {
                    System.Console.WriteLine("no such filename: " + e.Message);
                }
            }
        }

        private void CreateLevel() {
            string name = metaData.ContainsKey("Name") ? metaData["Name"] : "level with no name";
            Level newLevel = new Level(name, map, metaData);
            levels.Add(newLevel);
            CurrentLevel = CurrentLevel ?? newLevel;
            
            if (name.Equals("LEVEL 1")) {
                CurrentLevel = newLevel;
            }
        }
        
        private void ReadData() {
            List<string> rows = ReadMap();
            ReadMetaData();
            ReadLegend();

            int rowLength = rows[0].Length;
            map = new string[rows.Count, rowLength];
            for (int row=0; row<rows.Count; row++) {
                for (int col=0; col<rowLength; col++) {
                    string currentRow = rows[row];
                    string input = legend.ContainsKey(currentRow[col]) ? legend[currentRow[col]] : "none";
                    map[row, col] = input;
                }
            }
            
        }

        private List<string> ReadMap() {
            List<string> rows = new List<string>();
            while (true) {
                string line = streamReader.ReadLine();
                if (line.Equals("Map/")) {
                    break;
                }
                if (line.Equals("Map:") || line.Equals("")) {
                    continue;
                }
                else {
                    rows.Add(line);
                }
            }
            return rows;            
        }

        private void ReadMetaData() {
            while (true) {
                string line = streamReader.ReadLine();
                if (line.Equals("Meta/")) {
                    break;
                }
                if (line.Equals("Meta:") || line.Equals("")) {
                    continue;
                }
                else {
                    string[] splitLine = line.Split(": ");
                    metaData.Add(splitLine[0], splitLine[1]);
                }
            }
        }

        private void ReadLegend() {
            while (true) {
                string line = streamReader.ReadLine();
                if (line.Equals("Legend/")) {
                    break;
                }
                if (line.Equals("Legend:") || line.Equals("")) {
                    continue;
                }
                else {
                    char symbol = line[0];
                    string filename = line[3..];
                    legend.Add(symbol, filename);
                }
            }
        }
    }
}
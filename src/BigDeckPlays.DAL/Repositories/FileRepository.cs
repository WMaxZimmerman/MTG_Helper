using System.Collections.Generic;
using System.IO;

namespace BigDeckPlays.DAL.Repositories
{
    public interface IFileRepository
    {
        void WriteLines(IEnumerable<string> lines, string path);
        IEnumerable<string> ReadLines(string path);
    }
    
    public class FileRepository : IFileRepository
    {
        public void WriteLines(IEnumerable<string> lines, string path)
        {
            using (var file = new StreamWriter(path))
            {
                foreach (var line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }

        public IEnumerable<string> ReadLines(string path)
        {
            using (var file = new StreamReader(path))
            {
                var s = "";

                while ((s = file.ReadLine()) != null)
                {
                    yield return s;
                }
            }
        }
    }
}

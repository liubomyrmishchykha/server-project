using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.IO;
using System.Linq;

namespace SqlScriptCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @args[0];
            string createDbPath = path + @"DatabaseScripts\CreateDatabase";
            string createTablesPath = path + @"DatabaseScripts\Tables";
            string createStoredProcedures = path + @"DatabaseScripts\StoredProcedures";
            string ouputFilePath = @args[1];
            ouputFilePath += "CreateDb.sql";

            if (File.Exists(ouputFilePath))
                File.Delete(ouputFilePath);
            string[] database = Directory.GetFiles(createDbPath, "*.sql", SearchOption.TopDirectoryOnly);
            string[] tables = Directory.GetFiles(createTablesPath, "*.sql", SearchOption.TopDirectoryOnly);
            string[] storedProcedures = Directory.GetFiles(createStoredProcedures, "*.sql", SearchOption.TopDirectoryOnly);

            List<string> listOfFiles = new List<string>();
            listOfFiles.AddRange(database.ToList());
            listOfFiles.AddRange(tables.ToList());
            listOfFiles.AddRange(storedProcedures.ToList());
            foreach (string file in listOfFiles)
            {
                using (StreamWriter sw = File.AppendText(ouputFilePath))
                {
                    StreamReader sr = new StreamReader(file);
                    sw.WriteLine(sr.ReadToEnd());
                }
            }
        }
    }
}

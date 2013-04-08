namespace Proctology.Interview.CSharp.LikeStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;

    using Parsers;

    public partial class MainWindow : Window
    {
        private IEnumerable<string> GetFileList()
        {
            return FileSystem.GetFiles(
                        this.GetDatabasePath(),
                        this.GetSelectedSourceType());
        } // GetFileList()

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                var filesToProcess = this.GetFileList();
                var sourceType = this.GetSelectedSourceType();

                var persons = new List<Person>();

                // Fetch all persons from every files
                foreach (var file in filesToProcess)
                {
                    switch (sourceType)
                    {
                        case "bin":
                            {
                                var parser = new BinaryParser();
                                parser.Open(file);

                                while (!parser.HasReachedEnd)
                                {
                                    persons.Add(parser.GetPerson());
                                } // while

                                parser.Close();
                            } // case "bin"
                            break;

                        case "xml":
                            {
                                var parser = new XmlParser(file);
                                parser.StartParse();

                                Person parsedPerson;
                                while ((parsedPerson = parser.GetNextPerson()) != null)
                                {
                                    persons.Add(parsedPerson);
                                } // while

                                parser.FinishParse();
                            } // case "xml"
                            break;
                    } // switch
                } // foreach

                // Calculate like counts
                var counters = new List<PersonNameWithLikeCount>();

                foreach (var person in persons)
                {
                    var counter = 0;

                    foreach (var candidate in persons)
                    {
                        if (candidate.LikedPersons.Contains(person.Name))
                        {
                            counter++;
                        } // if
                    } // foreach

                    counters.Add(
                        new PersonNameWithLikeCount()
                        { 
                            Name = person.Name, 
                            LikeCount = counter 
                        });
                } // foreach

                // Show top 10 liked
                var firstTen = (from p in counters
                                orderby p.LikeCount descending
                                select p).Take(10);

                UpdateScreen(firstTen, sw.ElapsedMilliseconds);
            }
            catch (Exception)
            {
            } // catch
        } // Button_Click()    
    } // class MainWindow
} // namespace Proctology.Interview.CSharp.LikeStatistics

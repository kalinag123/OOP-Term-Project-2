/*
 * Author: Kalina Gavrilova
 * Project: P5
 */

namespace CPSC_3200_P3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Company accountingFirm = new Company("Accounting Firm");

            List<IOrgChange> instructions = new List<IOrgChange>();

            var path = args[0];
            var input = File.OpenText(path);

            for(string? line; (line = input.ReadLine()) != null;)
            {
                string[] words = line.Split(' ');

                switch(words[0])
                {
                    case "h":
                        instructions.Add(new HireOperation(words[1]));
                        break;

                    case "c":
                        instructions.Add(new CreateTeamOperation(words[1]));
                        break;

                    case "t":
                        instructions.Add(new TransferOperation(words[1], (words.Length == 3) ? words[2] : null));
                        break;
                }
            }

            foreach(var item in instructions)
            {
                item.Apply(accountingFirm);

                Console.WriteLine(item.ToString());

                foreach (Team team in accountingFirm.CompanyTeams)
                {
                    foreach (Task task in AllTasks.Items)
                    {
                        try
                        {
                            Console.WriteLine("Team " + team.Name + " will " + task.Name
                                + " in " + team.timeEstimate(task) + " minutes.");
                        }
                        catch(InvalidOperationException)
                        {
                            Console.WriteLine("Team " + team.Name + " has no employees yet.");
                        }
                    }

                    Console.WriteLine();
                }
            }   
        }
    }
}

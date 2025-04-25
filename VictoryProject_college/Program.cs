using System.Text.Json;
using VictoryProject_college.Utils;

namespace VictoryProject_college;

class Program
{
    static void Main(string[] args)
    {
        Vistory v = new();
        
        v.LoadVictory();
        v.StartVictory();
    }
}

class Vistory
{
    public Quiz? quiz = null;
    public int score = 0;
    
    public void LoadVictory()
    {
        Console.WriteLine("Enter file name (without extension):");
        string fileName = Console.ReadLine();
        
        fileName = Path.Combine("..", "..", "..", "Questions", fileName + ".json");
        Console.WriteLine("Path: " + fileName);

        if (!File.Exists(fileName))
        {
            Console.WriteLine("File not found!");
            return;
        }

        var jsonData = File.ReadAllText(fileName);
        this.quiz = JsonSerializer.Deserialize<Quiz>(jsonData);
    }

    public void StartVictory()
    {
        if (quiz == null)
        {
            Console.WriteLine("You have to select a quiz!");
            return;
        }
        
        foreach (var question in quiz.questions)
        {
            Console.Clear();
            Console.WriteLine($"----------- Victory - {quiz.name} ------------");
            
            Console.WriteLine("\t" + question.text);

            var correct = question.answers[0]; // перша відповіть у файлі є вірною
            question.answers.Shuffle();
            
            for (int i = 0; i < question.answers.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {question.answers[i]}");
            }

            Console.Write("Enter answer: ");
            int choice = Convert.ToInt32(Console.ReadLine()) - 1;

            if (choice < 0 || choice >= question.answers.Length)
            {
                Console.WriteLine("Invalid answer!");
                continue;
            }

            if (question.answers[choice] == correct)
                ++score;
        }

        Console.WriteLine($"Result: {score}/{quiz.questions.Length}");
    }
}

class Quiz
{
    public string name { get; set; }
    public Question[] questions { get; set; }
}

class Question
{
    public string text { get; set; }
    public string[] answers { get; set; }
}
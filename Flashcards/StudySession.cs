﻿using Flashcards.Models;
using Flashcards.Tables;
using Spectre.Console;

namespace Flashcards;

public class StudySession
{
    public static void StartStudySession()
    {
        Console.WriteLine("Please enter the name of the stack that you would like to study: ");
        StacksUI.DisplayStacks();

        var stackName = Console.ReadLine().Trim();
        var stackID = Stacks.ReturnStackID(stackName);

        if (stackID == 0)
        {
            Console.WriteLine("Stack not found. Please try again.\n");
            StartStudySession();
            return;
        }

        List<Flashcard> flashcards = FlashcardsTable.GetFlashcardsFromStack(stackID);
        List<FlashcardDTO> flashcardDTOS = flashcards.Select(f => MapToDTO(f)).ToList();

        var studySession = new StudySessionDTO
        {
            StackID = stackID,
            Date = DateTime.Now,
            Score = 0
        };

        if (flashcardDTOS.Count == 0)
        {
            Console.WriteLine("No flashcards available in this stack. Please add flashcards first.");
            return;
        }

        foreach (var flashcard in flashcardDTOS)
        {
            Console.WriteLine($"Question: {flashcard.Question}\n");

            string userAnswer = "";

            while (string.IsNullOrWhiteSpace(userAnswer))
            {
                Console.WriteLine("Your answer: ");
                userAnswer = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(userAnswer))
                {
                    Console.WriteLine("Answer cannot be blank. Please try again.\n");
                }
            }
            
            if (string.Equals(userAnswer, flashcard.Answer, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Correct!\n");
                studySession.Score++;
            }
            else
            {
                Console.WriteLine($"Incorrect. The correct answer is: {flashcard.Answer}\n");
            }
        }

        Console.WriteLine($"You have completed all cards for this stack! Your score: {studySession.Score}/{flashcardDTOS.Count}");

        StudySessions.SaveStudySession(studySession);
    }

    public static FlashcardDTO MapToDTO(Flashcard flashcard)
    {
        return new FlashcardDTO
        {
            Question = flashcard.Question,
            Answer = flashcard.Answer
        };
    }

    public static void DisplayStudySessions()
    {
        var studySessions = StudySessions.GetStudySessions();

        AnsiConsole.Write(new Markup("[bold underline]Study Sessions[/]\n").Centered());

        var table = new Table();

        table.AddColumn(new TableColumn("[bold]Stack[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Date[/]")).Centered();
        table.AddColumn(new TableColumn("[bold]Score[/]")).Centered();

        table.Border(TableBorder.Ascii);
        table.ShowHeaders = true;
        table.BorderColor(Color.Grey);
        table.ShowFooters = false;

        foreach (var session in studySessions)
        {
            table.AddRow(
                session.StackName,
                session.Date.ToString("yyyy-MM-dd HH:mm"),
                session.Score.ToString()
            );
        }

        AnsiConsole.Write(table);

        Console.WriteLine("\nPress any key to return to the main menu\n");
        Console.ReadLine();
    }
}

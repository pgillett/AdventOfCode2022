using System;
using System.Linq;

namespace Advent;

public class Day02
{
    public int Score(string input) => ScoreCalc(input, -1);

    public int ScoreForceOutcome(string input) => ScoreCalc(input,  1);

    private int ScoreCalc(string input,  int subtract) =>
        input.Split(Environment.NewLine)
            .Select(l => (l[0] - 'A', l[2] - 'X'))
            .Sum(game => ((game.Item2 + subtract * (game.Item1 - 1) + 3) % 3) * (2 - subtract) + game.Item2 * (2 + subtract) + 1);

    // Human readable versions
    
    public int ScoreReadable(string input)
    {
        var games = input.Split(Environment.NewLine)
            .Select(l => ((PlayType)(l[0]-'A'), (PlayType)(l[2]-'X')))
            .ToArray();

        int score = 0;

        foreach (var game in games)
        {
            var outcome = game switch
            {
                (PlayType.Rock, PlayType.Paper) => 6,
                (PlayType.Rock, PlayType.Scissors) => 0,
                (PlayType.Rock, PlayType.Rock) => 3,

                (PlayType.Paper, PlayType.Rock) => 0,
                (PlayType.Paper, PlayType.Scissors) => 6,
                (PlayType.Paper, PlayType.Paper) => 3,

                (PlayType.Scissors, PlayType.Rock) => 6,
                (PlayType.Scissors, PlayType.Paper) => 0,
                (PlayType.Scissors, PlayType.Scissors) => 3
            };

            score = score + outcome + ChoiceScore(game.Item2);
        }

        return score;
    }
    
    public int ScoreForceOutcomeReadable(string input)
    {
        var games = input.Split(Environment.NewLine)
            .Select(l => ((PlayType)(l[0]-'A'), (Outcome)(l[2]-'X')))
            .ToArray();

        int score = 0;

        foreach (var game in games)
        {
            var play = game switch
            {
                (PlayType.Rock, Outcome.Lose) => PlayType.Scissors,
                (PlayType.Rock, Outcome.Draw) => PlayType.Rock,
                (PlayType.Rock, Outcome.Win) => PlayType.Paper,

                (PlayType.Paper, Outcome.Lose) => PlayType.Rock,
                (PlayType.Paper, Outcome.Draw) => PlayType.Paper,
                (PlayType.Paper, Outcome.Win) => PlayType.Scissors,

                (PlayType.Scissors, Outcome.Lose) => PlayType.Paper,
                (PlayType.Scissors, Outcome.Draw) => PlayType.Scissors,
                (PlayType.Scissors, Outcome.Win) => PlayType.Rock
            };

            score = score + 3 * ((int)game.Item2) + ChoiceScore(play);
        }

        return score;
    }

    public enum PlayType
    {
        Rock, Paper, Scissors
    }

    public enum Outcome
    {
        Lose, Draw, Win
    }

    public int ChoiceScore(PlayType type) => ((int)type) + 1;
}
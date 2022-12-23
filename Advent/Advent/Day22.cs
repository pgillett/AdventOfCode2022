using System;
using System.Linq;

namespace Advent;

public class Day22
{
    public int Password(string input)
    {
        var split = input.Split(Environment.NewLine + Environment.NewLine);

        var lines = split[0].Split(Environment.NewLine);
        MaxX = lines.Max(l => l.Length);
        MaxY = lines.Length;
        Map = new char[MaxX, MaxY];
        for(var y = 0; y< MaxY; y++)
        for (var x = 0; x < lines[y].Length; x++)
            Map[x, y] = lines[y][x];

        (int x, int y) pos = (0, 0);
        for(var i = 0; i<MaxX;i++)
            if (Map[i, 0] == '.')
            {
                pos = (i, 0);
                break;
            }

        var direction = 0;

        var feed = new Feed(split[1]);

        while (!feed.IsEnd)
        {
            if (feed.IsTurn)
            {
                direction = (direction + (feed.GetChar() == 'R' ? 1 : -1) + 4) % 4;
            }
            else
            {
                var move = feed.GetNumber();

                for (var m = 0; m < move; m++)
                {
                    (int x, int y) newPos = Move(pos, Directions[direction]);
                    if (Map[newPos.x, newPos.y] == '#')
                        break;
                    pos = newPos;
                }
            }
        }

        return (pos.y + 1) * 1000 + (pos.x + 1) * 4 + direction;
    }

    public (int x, int y) Move((int x, int y) pos, (int dx, int dy) delta)
    {
        (int x, int y) newPos = ((pos.x + delta.dx + MaxX) % MaxX, (pos.y + delta.dy + MaxY) % MaxY);
        var at = Map[newPos.x, newPos.y];
        if (at != '.' && at != '#') return Move(newPos, delta);
        return newPos;
    }

    public (int dx, int dy)[] Directions = {(1, 0), (0, 1), (-1, 0), (0, -1)};

    public char[,] Map;
    public int MaxX;
    public int MaxY;

    public int Size;
    
    public int Password3d(string input)
    {
        var split = input.Split(Environment.NewLine + Environment.NewLine);
        var lines = split[0].Split(Environment.NewLine);

        MaxX = lines.Max(l => l.Length);
        MaxY = lines.Length;

        Size = MaxX > MaxY ? MaxX / 4 : MaxX / 3;

        Map = new char[MaxX, MaxY];
        for(var y = 0; y< MaxY; y++)
        for (var x = 0; x < lines[y].Length; x++)
            Map[x, y] = lines[y][x];

        (int x, int y) pos = (0, 0);
        for(var i = 0; i<MaxX;i++)
            if (Map[i, 0] == '.')
            {
                pos = (i, 0);
                break;
            }

        var direction = 0;

        var feed = new Feed(split[1]);

        while (!feed.IsEnd)
        {
            if (feed.IsTurn)
            {
                direction = (direction + (feed.GetChar() == 'R' ? 1 : -1) + 4) % 4;
            }
            else
            {
                var move = feed.GetNumber();

                for (var m = 0; m < move; m++)
                {
                    ((int x, int y) newPos, int newDirection) = Move3d(pos, direction);
                    if (Map[newPos.x, newPos.y] == '#')
                        break;
                    pos = newPos;
                    direction = newDirection;
                }
            }
        }
        
        return (pos.y + 1) * 1000 + (pos.x + 1) * 4 + direction;
    }

    public class Feed
    {
        public string Input;
        public int Pos;

        public Feed(string input)
        {
            Input = input;
        }

        public bool IsEnd => Pos == Input.Length;

        public bool IsTurn => char.IsLetter(Input[Pos]);

        public char GetChar() => Input[Pos++];

        public int GetNumber()
        {
            var number = Input[Pos++] - '0';
            while (Pos < Input.Length)
            {
                var c = Input[Pos];
                if (char.IsLetter(c))
                    break;
                number = number * 10 + (c - '0');
                Pos++;
            }

            return number;
        }
    }

    public ((int x, int y), int direction) Move3d((int x, int y) pos, int direction)
    {
        var delta = Directions[direction];
        (int x, int y) newPos = (pos.x + delta.dx, pos.y + delta.dy);
        if (newPos.x >= 0 && newPos.x < MaxX && newPos.y >= 0 && newPos.y < MaxY
            && (Map[newPos.x, newPos.y] == '.' || Map[newPos.x, newPos.y] == '#'))
            return (newPos, direction);

        if (IsOk(newPos, -4 * Directions[direction].dx, -4 * Directions[direction].dy))
        {
            return (Translate(newPos, (-4 * Directions[direction].dx, -4 * Directions[direction].dy), ""), direction);
        }

        // if (IsOk(newPos, Directions[direction].dy, Directions[direction].dx))
        //     return (Translate(newPos, (Directions[direction].dy, Directions[direction].dx), "CCCC"[direction].ToString()), (direction + 1) % 4);

        switch (direction)
        {
            case 0:
                if (IsOk(newPos, 0, 1)) return (Translate(newPos, (0, 1), "C"), 1);
                if (IsOk(newPos, 0, -1)) return (Translate(newPos, (0, -1), "A"), 3);
                if (IsOk(newPos, 0, -2)) return (Translate(newPos, (0, -2), "AA"), 2);
                if (IsOk(newPos, -2, 2)) return (Translate(newPos, (-2, 2), "CC"), 2);
                break;
            case 1:
                if (IsOk(newPos, -2, -2)) return (Translate(newPos, (-2, -2), "AA"), 3);
                if (IsOk(newPos, -1, 0)) return (Translate(newPos, (-1, 0), "C"), 2);
                if (IsOk(newPos, 2, -4)) return (Translate(newPos, (2, -4), ""), 1);
                break;
            case 2:
                if (IsOk(newPos, 0, 1)) return (Translate(newPos, (0, 1), "A"), 1);
                if (IsOk(newPos, 0, 2)) return (Translate(newPos, (0, 2), "AA"), 0);
                if (IsOk(newPos, 2, -3)) return (Translate(newPos, (2, -3), "A"), 1);
                if (IsOk(newPos, 2, -2)) return (Translate(newPos, (2, -2), "CC"), 0);
                break;
            case 3:
                if (IsOk(newPos, 1, 0)) return (Translate(newPos, (1, 0), "C"), 0);
                if (IsOk(newPos, -1, 4)) return (Translate(newPos, (-1, 4), "C"), 0);
                if (IsOk(newPos, -2, 4)) return (Translate(newPos, (-2, 4), ""), 3);
                break;
        }

        throw new Exception();
        
    }

    public (int x, int y) Translate((int x, int y) pos, (int x, int y) delta, string moves)
    {
        pos = (pos.x + delta.x * Size, pos.y + delta.y * Size);
        
        (int x, int y) basePos = (pos.x / Size, pos.y / Size);
        basePos = (basePos.x * Size, basePos.y * Size);
        
        (int x, int y) offSet = (pos.x % Size, pos.y % Size);

        foreach (var move in moves)
        {
            offSet = move switch
            {
                'C' => (Size - 1 - offSet.y, offSet.x),
                'A' => (offSet.y, Size - 1 - offSet.x)
            };
        }

        pos = (basePos.x + offSet.x, basePos.y + offSet.y);
        
        return pos;
    }

    public bool IsOk((int x, int y) pos, int dx, int dy)
    {
        var x = pos.x + dx * Size;
        var y = pos.y + dy * Size;
        if (x < 0 || x >= MaxX || y < 0 || y >= MaxY) return false;
        var at = Map[x, y];
        return at is '.' or '#';
    }
}
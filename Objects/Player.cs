using SeaBattle.Extensions;
using SeaBattle.Objects.Boards;
using SeaBattle.Objects.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using SeaBattle.Enums;

namespace SeaBattle.Objects;

public class Player(string name)
{
    public string Name { get; } = name;
    private GameBoard GameBoard { get; } = new();
    private FiringBoard FiringBoard { get; } = new();
    public int NumberOfWins { get; set; }
    public int Rating { get; set; }
    private List<Ship> Ships { get; } =
    [
        new Destroyer(),
        new Submarine(),
        new Cruiser(),
        new Battleship(),
        new AircraftCarrier()
    ];

    public bool HasLost { get { return Ships.All(x => x.IsSunk); } }
    
    public void OutputBoards()
    {
        Console.WriteLine();
        Console.WriteLine(Name);
        Console.WriteLine("Own Board:                          Firing Board:");
        for (int row = 1; row <= 10; row++)
        {
            for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
            {
                Console.Write(GameBoard.Cells.At(row, ownColumn).Status + " ");
            }
            Console.Write("                ");
            for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
            {
                Console.Write(FiringBoard.Cells.At(row, firingColumn).Status + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void PlaceShips()
    {
        Random rand = new Random(Guid.NewGuid().GetHashCode());
        foreach (Ship ship in Ships)
        {
            //Select a random row/column combination, then select a random orientation.
            //If none of the proposed panels are occupied, place the ship
            //Do this for all ships
            bool isOpen = true;
            while (isOpen)
            {
                int startColumn = rand.Next(1, 11);
                int startRow = rand.Next(1, 11);
                int endRow = startRow, endColumn = startColumn;
                int orientation = rand.Next(1, 101) % 2; //0 for Horizontal

                if (orientation == 0)
                {
                    for (int i = 1; i < ship.Width; i++)
                    {
                        endRow++;
                    }
                }
                else
                {
                    for (int i = 1; i < ship.Width; i++)
                    {
                        endColumn++;
                    }
                }

                //We cannot place ships beyond the boundaries of the board
                if (endRow > 10 || endColumn > 10)
                {
                    continue;
                }

                //Check if specified cells are occupied
                List<Cell> affectedCells = GameBoard.Cells.Range(startRow, startColumn, endRow, endColumn);
                if (affectedCells.Any(x => x.IsOccupied))
                {
                    continue;
                }

                foreach (Cell cell in affectedCells)
                {
                    cell.OccupationType = ship.OccupationType;
                }
                
                isOpen = false;
            }
        }
    }
    
    public Coordinates FireShot()
    {
        //If there are hits on the board with neighbors which don't have shots, we should fire at those first.
        List<Coordinates> hitNeighbors = FiringBoard.GetHitNeighbors();
        Coordinates coords = hitNeighbors.Any() ? SearchingShot() : RandomShot();
        Console.WriteLine(Name + " says: \"Firing shot at " + coords.Row + ", " + coords.Column + "\"");
        return coords;
    }

    private Coordinates RandomShot()
    {
        List<Coordinates> availableCells = FiringBoard.GetOpenRandomCells();
        Random rand = new Random(Guid.NewGuid().GetHashCode());
        int panelId = rand.Next(availableCells.Count);
        return availableCells[panelId];
    }

    private Coordinates SearchingShot()
    {
        Random rand = new Random(Guid.NewGuid().GetHashCode());
        List<Coordinates> hitNeighbors = FiringBoard.GetHitNeighbors();
        int neighborId = rand.Next(hitNeighbors.Count);
        return hitNeighbors[neighborId];
    }

    public ShotResult ProcessShot(Coordinates coords)
    {
        Cell cell = GameBoard.Cells.At(coords.Row, coords.Column);
        if (!cell.IsOccupied)
        {
            Console.WriteLine(Name + " says: \"Miss!\"");
            return ShotResult.Miss;
        }

        Ship ship = Ships.First(x => x.OccupationType == cell.OccupationType);
        ship.Hits++;
        Console.WriteLine(Name + " says: \"Hit!\"");
        
        if (ship.IsSunk)
        {
            Console.WriteLine(Name + " says: \"You sunk my " + ship.Name + "!\"");
        }

        return ShotResult.Hit;
    }

    public void ProcessShotResult(Coordinates coords, ShotResult result)
    {
        Cell cell = FiringBoard.Cells.At(coords.Row, coords.Column);
        cell.OccupationType = result switch
        {
            ShotResult.Hit => OccupationType.Hit,
            _ => OccupationType.Miss
        };
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Conf;
using UnityEngine;
using View;

namespace Core
{
    public class Battle : ICore, IBattle
    {
        private readonly MapGrid<Unit> _grid = new MapGrid<Unit>();
        private readonly List<Unit> _units = new List<Unit>();
        private readonly Transform _container;
    
        public Battle(Transform container)
        {
            _container = container;
        }

        public IUnit GetNearestEnemy(IUnit unit)
        {
            var enemies = unit.Team == TeamFlag.Red ? GetBlueTeam() : GetRedTeam();
            return GetNearestAtTeam(unit, enemies);
        }

        public IUnit GetNearestFriend(IUnit unit)
        {
            var friends = unit.Team == TeamFlag.Red ? GetRedTeam() : GetBlueTeam();
            return GetNearestAtTeam(unit, friends);
        }
        
        public float GetDistance(int x1, int y1, int x2, int y2)
        {
            return Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2));
        }

        public float GetDistance(IUnit first, IUnit second)
        {
            return GetDistance(first.X, first.Y, second.X, second.Y);
        }
        
        public void Start(BattleInfo info)
        {
            foreach (var entry in info.Units)
            {
                SpawnUnit(entry.Flag, entry.Info, entry.SpawnX, entry.SpawnY);
            }
        }

        public void Tick()
        {
            LogicTick();
        }
        
        public void Finish()
        {
            while (_units.Any())
            {
                DeleteUnit(_units[0]);
            }
            _grid.Clear();
        }
        
        public bool IsFinished()
        {
            return !GetRedTeam().Any() || !GetBlueTeam().Any();
        }

        public void AskMoveUnitTo(Unit unit, int toX, int toY)
        {
            var distance = GetDistance(unit.X, unit.Y, toX, toY);
            var step = Math.Min((int)Math.Round(distance), unit.Speed);
            var path = PathFinding.FindNextPoint(_grid, unit.X, unit.Y, toX, toY, step);
            if (path == Vector2.negativeInfinity)
            {
                return;
            }

            MoveUnit(unit, (int)path.x, (int)path.y);
        }
        
        private void LogicTick()
        {
            var dead = new List<Unit>();
            foreach (var unit in _units)
            {
                if (unit.IsAlive())
                {
                    unit.Tick();
                }
                else
                {
                    dead.Add(unit);
                }
            }
            dead.ForEach(DeleteUnit);
            dead.Clear();
        }

        private bool IsCellTaken(Vector2 position)
        {
            return _grid[(int) position.x, (int) position.y] != null;
        }

        private IEnumerable<IUnit> GetRedTeam()
        {
            return _units.Where(u => u.Team == TeamFlag.Red && u.IsAlive());
        }

        private IEnumerable<IUnit> GetBlueTeam()
        {
            return _units.Where(u => u.Team == TeamFlag.Blue && u.IsAlive());
        }
        
        private IUnit GetNearestAtTeam(IUnit unit, IEnumerable<IUnit> team)
        {
            var min = 0.0f;
            IUnit nearest = null;
            foreach (var current in team)
            {
                if (current == unit || !current.IsAlive())
                {
                    continue;
                }
                var distance = GetDistance(current, unit);
                if (nearest == null || distance < min)
                {
                    min = distance;
                    nearest = current;
                }
            }
            return nearest;
        }

        private void SpawnUnit(TeamFlag flag, UnitInfo info, int x, int y)
        {
            if (_grid[x, y] != null)
            {
                throw new ArgumentException($"Supplied coords ({x},{y}) already taken");
            }
            var view = UnitView.Create(flag, _container);
            var unit = new Unit(flag, info, view, this);
            PlaceUnit(unit, x, y);
            _units.Add(unit);
        }

        private void MoveUnit(Unit unit, int x, int y)
        {
            _grid[unit.X, unit.Y] = null;
            PlaceUnit(unit, x, y);
        }
        
        private void PlaceUnit(Unit unit, int x, int y)
        {
            _grid[x, y] = unit;
            unit.X = x;
            unit.Y = y;
            unit.View.SetPosition(x, y);
        }
        
        private void DeleteUnit(Unit unit)
        {
            _grid[unit.X, unit.Y] = null;
            _units.Remove(unit);
            unit.View.Destroy();
        }
    }
}
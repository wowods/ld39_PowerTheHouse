using System.Collections.Generic;
using ld39.Entities;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Microsoft.Xna.Framework;
using ld39.Entities.Behaviour;

namespace ld39
{
    static class EntityManager
    {
        static List<Entity> entities = new List<Entity>();
        static List<IElectrified> electrified = new List<IElectrified>();
        static List<IMoveable> movables = new List<IMoveable>();
        static List<INeedPower> needPowers = new List<INeedPower>();
        static List<IOverlap> overlaps = new List<IOverlap>();
        static List<IBlockElectricity> blocks = new List<IBlockElectricity>();

        static bool isUpdating;
        static List<Entity> pendingEntities = new List<Entity>();
        static IMoveable selectedEntity = null;

        public static List<IElectrified> ListElectrified { get { return electrified; } }

        static List<Indicator> indicatorPool = new List<Indicator>();
        static List<Indicator> indicatorAlive = new List<Indicator>();
        public static void InitializeIndicator()
        {
            int totalPool = 100;
            for (int i = 0; i < totalPool; i++)
            {
                Indicator temp = new Indicator(0, 0);
                temp.Kill();
                indicatorPool.Add(temp);
            }
        }

        static void GetIndicator(int x, int y)
        {
            Indicator indicator = indicatorPool.FirstOrDefault(item => item.IsExpired);
            if (indicator != null)
            {
                indicator.Reset(x, y);
                indicatorAlive.Add(indicator);
            }
        }

        public static void Add(Entity entity)
        {
            if (!isUpdating)
                AddEntity(entity);
            else
                pendingEntities.Add(entity);
        }

        // Adding to specific list
        private static void AddEntity(Entity entity)
        {
            entities.Add(entity);

            if (entity is IElectrified || entity.GetType() == typeof(IElectrified))
                electrified.Add((IElectrified)entity);
            if (entity is IMoveable || entity.GetType() == typeof(IMoveable))
                movables.Add((IMoveable)entity);
            if (entity is INeedPower || entity.GetType() == typeof(INeedPower))
                needPowers.Add((INeedPower)entity);
            if (entity is IOverlap || entity.GetType() == typeof(IOverlap))
                overlaps.Add((IOverlap)entity);
            if (entity is IBlockElectricity || entity.GetType() == typeof(IBlockElectricity))
                blocks.Add((IBlockElectricity)entity);
        }

        public static void Update(float dt)
        {
            if (selectedEntity == null)
                CalculateElectrifiedAndNeedPower();

            isUpdating = true;
            foreach (var entity in entities)
                entity.Update(dt);

            // Moving pendingEntities to entities list
            isUpdating = false;
            foreach (var entity in pendingEntities)
                entities.Add(entity);
            pendingEntities.Clear();

            // Moving with cursor
            if (selectedEntity != null)
                selectedEntity.Moving();

            // Updating Indicator
            foreach (Indicator ind in indicatorAlive)
                ind.Update(dt);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Draw overlap-able entity first
            foreach (Entity entity in overlaps)
                entity.Draw(spriteBatch);

            // Draw others entities
            foreach (var entity in entities)
                if (!(entity is IOverlap || entity is IMoveable))
                    entity.Draw(spriteBatch);

            // Draw moveable entity last
            foreach (Entity entity in movables)
                entity.Draw(spriteBatch);

            // Draw indicator
            foreach (Indicator ind in indicatorAlive)
                ind.Draw(spriteBatch);
        }

        public static void ClearAll()
        {
            selectedEntity = null;
            isUpdating = false;

            entities.Clear();
            electrified.Clear();
            movables.Clear();
            needPowers.Clear();
            overlaps.Clear();
            blocks.Clear();

            foreach (Indicator ind in indicatorAlive)
                ind.Kill();
            indicatorAlive.Clear();

            pendingEntities.Clear();
        }

        public static bool CheckSelectedEntity(int x, int y)
        {
            Point position = new Point(x, y);
            Entity entity = FindMovableByPosition(position);
            if (entity != null)
            {
                // Set IMovable from that position to moviing with cursor
                if (selectedEntity == null)
                {
                    selectedEntity = (IMoveable)entity;
                    LevelManager.ChangeLevelData(entity.CODE_ENTITY, x, y);
                    Art.Pick.Play(0.5f, 0f, 0f);
                }
                // Entity already been selected, that means time to "release" it from cursor
                else
                {
                    // Check if position is can be filled
                    List<Entity> checkList = FindEntityByPosition(position);
                    if (checkList.Count == 2)
                    {
                        foreach (Entity checkedEntity in checkList)
                        {
                            // Skip for selectedEntity
                            if (checkedEntity.Equals(selectedEntity)) continue;
                            if (checkedEntity is IOverlap && checkList.Count == 2) // Count 2 : 1 IOverlap + 1 selectedEntity, more than that is false
                            {
                                LevelManager.ChangeLevelData(entity.CODE_ENTITY, x, y);
                                entity.Position = position;
                                ((Entity)selectedEntity).Color = Art.NoTint; // remove SelectedTint
                                selectedEntity = null;
                                CalculateAfterClick();
                                Art.Drop.Play(0.5f, 0f, 0f);
                            }
                            else
                                return false;
                        }
                    }
                    else
                        return false;
                }
                return true;
            }
            return false;
        }

        private static List<Entity> FindEntityByPosition(Point position)
        {
            List<Entity> result = new List<Entity>();
            foreach (Entity entity in entities)
                if (entity.Position.Equals(position))
                    result.Add(entity);

            return result;
        }

        private static Entity FindMovableByPosition(Point position)
        {
            foreach (Entity entity in movables)
                if (entity.Position.Equals(position))
                    return entity;

            return null;
        }

        private static void SetHighlight()
        {
            // TODO implement set highlight
        }

        private static void CalculateAfterClick()
        {
            // only calculate this after drop a selected IMovable, i think it's to costly to calculate at update

            // Clear some variables
            LevelManager.ElectrifiedLevel = new int[10, 10];
            foreach (IElectrified electr in electrified)
                electr.IsElectrified = false;
            foreach (Indicator ind in indicatorAlive)
                ind.Kill();
            indicatorAlive.Clear();

            CalculateElectrifiedAndNeedPower();
        }

        private static void CalculateElectrifiedAndNeedPower()
        {
            // Start calculate
            foreach (Entity entity in electrified)
            {
                if (((IElectrified)entity).IsElectrified)
                {
                    ((IElectrified)entity).IsElectrified = false;
                    int x = entity.Position.X;
                    int y = entity.Position.Y;
                    int range = ((IElectrified)entity).RangeElectrified;
                    List<Point> coord = new List<Point>();

                    for (int j = 0; j < LevelManager.CurrentLevel.Length; j++)
                    {
                        for (int i = 0; i < LevelManager.CurrentLevel[j].Length; i++)
                        {
                            // This is the looping data with CurrentLevel[j][i]
                            if (entity.Position.GetRange(new Point(j, i)) <= range)
                                coord.Add(new Point(j, i));
                        }
                    }

                    // Clearing the blocked path
                    var duplicateCoord = new List<Point>(coord);
                    foreach (Point point in duplicateCoord)
                    {
                        foreach (Entity blocker in blocks)
                        {
                            if (blocker.Position.Equals(point))
                            {
                                if (point.X == x)
                                {
                                    if (point.Y < y)
                                        coord.RemoveAll(p => (p.X == point.X && p.Y <= point.Y));
                                    else if (point.Y > y)
                                        coord.RemoveAll(p => (p.X == point.X && p.Y >= point.Y));
                                }
                                else if (point.X < x)
                                    coord.RemoveAll(p => (p.X <= point.X && p.Y == point.Y));
                                else if (point.X > x)
                                    coord.RemoveAll(p => (p.X >= point.X && p.Y == point.Y));
                            }
                        }
                    }

                    // After it clear, put it into Electrified Array (plus set Indicator)
                    foreach (Point point in coord)
                        LevelManager.ElectrifiedLevel[point.X, point.Y] = 1;
                }
            }

            for (int j = 0; j < LevelManager.ElectrifiedLevel.GetLength(0); j++)
            {
                for (int i = 0; i < LevelManager.ElectrifiedLevel.GetLength(1); i++)
                {
                    if (LevelManager.ElectrifiedLevel[j, i] == 1)
                        GetIndicator(j, i);
                }
            }

            // TODO set INeedPower is their needs already satisfied or not
            foreach (Entity needPower in needPowers)
            {
                if (LevelManager.ElectrifiedLevel[needPower.Position.X, needPower.Position.Y] == 1)
                    ((INeedPower)needPower).IsElectrified = true;
                else
                    ((INeedPower)needPower).IsElectrified = false;
            }

            // Win condition
            if (needPowers.Where(o => o.IsElectrified).Count() == needPowers.Count())
                GameMain.State = GameMain.GAMESTATE.Winning;
        }
    }
}
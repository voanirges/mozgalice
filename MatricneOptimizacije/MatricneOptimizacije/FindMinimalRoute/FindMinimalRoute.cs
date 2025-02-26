
using static MatricneOptimizacije.FindMinimalRoute.Models;
using Path = MatricneOptimizacije.FindMinimalRoute.Models.Path;

namespace MatricneOptimizacije.FindMinimalRoute
{
    /// <summary>
    /// For the given matrix M(a,b) find steps from point P(0,0) to P(a-1,b-1) that has minimal summary of matrix value on the path.
    /// 
    /// For this , simple example, moving to matrix can be only -> and ↓ ,to increasing index of current point.
    /// </summary>
    public class FindMinimalRoute
    {

        

        /// <summary>
        /// 
        ///
        ///
        /// </summary>
        public RouteWithSum GetMinimalRouteJagged(int[][] matrix , Direction[] AllowedDirections)
        {
            try
            {
                List<RouteWithSum> startPoint = new() { new RouteWithSum() { Path = new Path() { Points = [new Point() { X = 0, Y = 0 }] } , Sum = matrix[0][0] } };

                var Paths = CalculateRoutesJagged(matrix, startPoint, AllowedDirections);

                return Paths.OrderBy(path => path.Sum).First();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return new RouteWithSum()
            {
                Path = new Path() { Points = [new Point() { X = -1, Y = -1 }] },
                Sum = -1
            };
        }

        public RouteWithSum GetMinimalRouteMultiDimensional(int[,] matrix, Direction[] AllowedDirections)
        {
            try
            {
                List<RouteWithSum> startPoint = new() { new RouteWithSum() { Path = new Path() { Points = [new Point() { X = 0, Y = 0 }] }, Sum = matrix[0, 0] } };

                var Paths = CalculateRoutesMultiDimensional(matrix, startPoint, AllowedDirections);

                return Paths.OrderBy(path => path.Sum).First();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return new RouteWithSum() { Path = new Path() { Points = [new Point() { X = -1, Y = -1 }] }, Sum = -1 };
        }

        private List<RouteWithSum> CalculateRoutesJagged(int[][] matrix, List<RouteWithSum> CalculatedPaths , Direction[] AllowedDirections)
        {

            do
            {
                CalculatedPaths = CalculateNewPossibleRoutesJagged(CalculatedPaths, AllowedDirections, matrix);

            } while (CalculatedPaths.Any(path=> path.Path.Points.Last().X < matrix.Length - 1 || path.Path.Points.Last().Y < matrix.First().Length - 1 ));
            

            return CalculatedPaths;
        }

        private List<RouteWithSum> CalculateRoutesMultiDimensional(int[,] matrix, List<RouteWithSum> CalculatedPaths, Direction[] AllowedDirections)
        {

            do
            {
                CalculatedPaths = CalculateNewPossibleRoutesMultiDimensional(CalculatedPaths, AllowedDirections, matrix);

            } while (CalculatedPaths.Any(path => path.Path.Points.Last().X < matrix.GetLength(0) - 1 || path.Path.Points.Last().Y < matrix.GetLength(1) - 1));


            return CalculatedPaths;
        }


        private List<RouteWithSum> CalculateNewPossibleRoutesJagged(List<RouteWithSum> Paths, Direction[] AllowedDirections , int[][] matrix)
        {
            List<RouteWithSum> NewPaths = new();

            foreach (Direction direction in AllowedDirections)
            {
                foreach (RouteWithSum result in Paths)
                {
                    if (result.Path.Points.Last().X == matrix.Length - 1 && result.Path.Points.Last().Y == matrix.First().Length - 1)
                    {
                        continue;
                    }
                    else
                    {
                        //TODO add check if this point is allowed in the path !!!
                        RouteWithSum optimizationResult = new() { Path = new Path() { Points = result.Path.Points }, Sum = result.Sum };
                        Point? newPoint = CalculateNewPoint(direction , result, optimizationResult);

                        //check if point is within matrix, not outside
                        if (newPoint is null || newPoint.X == matrix.Length || newPoint.Y == matrix.First().Length)
                        {
                            continue;
                        }

                        optimizationResult.Path.Points = [.. optimizationResult.Path.Points, newPoint];
                        optimizationResult.Sum += matrix[newPoint.X][newPoint.Y];

                        NewPaths.Add(optimizationResult);
                    }
                }
            }

            return NewPaths;
        }

        private Point? CalculateNewPoint(Direction direction, RouteWithSum result , RouteWithSum optimizationResult)
        {
           
           
            switch (direction)
            {
                case Direction.Right:
                    return new Point() { X = optimizationResult.Path.Points.Last().X + 1, Y = optimizationResult.Path.Points.Last().Y };

                case Direction.Down:
                    return new Point() { X = optimizationResult.Path.Points.Last().X, Y = optimizationResult.Path.Points.Last().Y + 1 };

                case Direction.Left:
                    return new Point() { X = optimizationResult.Path.Points.Last().X - 1, Y = optimizationResult.Path.Points.Last().Y };

                case Direction.Up:
                    return new Point() { X = optimizationResult.Path.Points.Last().X, Y = optimizationResult.Path.Points.Last().Y - 1 };

            }

            return null;
        }


        private List<RouteWithSum> CalculateNewPossibleRoutesMultiDimensional(List<RouteWithSum> Paths, Direction[] AllowedDirections, int[,] matrix)
        {
            List<RouteWithSum> NewPaths = new();

            foreach (Direction direction in AllowedDirections)
            {
                foreach (RouteWithSum result in Paths)
                {
                    if (result.Path.Points.Last().X == matrix.GetLength(0) - 1 && result.Path.Points.Last().Y == matrix.GetLength(1) - 1)
                    {
                        continue;
                    }
                    else
                    {
                        //TODO add check if this point is allowed in the path !!!
                        RouteWithSum optimizationResult = new() { Path = new Path() { Points = result.Path.Points }, Sum = result.Sum };
                        Point? newPoint = CalculateNewPoint(direction , result, optimizationResult);

                        //check if point is within matrix, not outside
                        if (newPoint is null || newPoint.X == matrix.GetLength(0) || newPoint.Y == matrix.GetLength(1))
                        {
                            continue;
                        }

                        optimizationResult.Path.Points = [.. optimizationResult.Path.Points, newPoint];
                        optimizationResult.Sum += matrix[newPoint.X , newPoint.Y];

                        NewPaths.Add(optimizationResult);
                    }
                }
            }

            return NewPaths;
        }


        #region CalculateRoutesMultiDimensionalMatrix
        public List<RouteWithSum> CalculateRoutesMultiDimensionalMatrix(int M, int N,Direction[] AllowedDirections)
        {
            List<RouteWithSum> CalculatedPaths = new() { new RouteWithSum() { Path = new Path() { Points = [new Point() { X = 0, Y = 0 }] } } };
            do
            {
                CalculatedPaths = CalculateRoutesMultiDimensionalMatrixCyclic(CalculatedPaths, AllowedDirections,   M,  N);

            } while (CalculatedPaths.Any(path => path.Path.Points.Last().X < M - 1 || path.Path.Points.Last().Y < N - 1));


            return CalculatedPaths;
        }

        private List<RouteWithSum> CalculateRoutesMultiDimensionalMatrixCyclic(List<RouteWithSum> Paths, Direction[] AllowedDirections, int M, int N)
        {

            List<RouteWithSum> NewPaths = new();

            foreach (Direction direction in AllowedDirections)
            {
                foreach (RouteWithSum result in Paths)
                {
                    if (result.Path.Points.Last().X == M - 1 && result.Path.Points.Last().Y == N - 1)
                    {
                        continue;
                    }
                    else
                    {
                        //TODO add check if this point is allowed in the path !!!
                        RouteWithSum optimizationResult = new() { Path = new Path() { Points = result.Path.Points }, Sum = result.Sum };
                        Point? newPoint = CalculateNewPoint(direction, result, optimizationResult);

                        //check if point is within matrix, not outside
                        if (newPoint is null || newPoint.X == M || newPoint.Y == N)
                        {
                            continue;
                        }

                        optimizationResult.Path.Points = [.. optimizationResult.Path.Points, newPoint];

                        NewPaths.Add(optimizationResult);
                    }
                }
            }

            return NewPaths;
        }

        #endregion
    }
}